using Domain.Misc;
using Domain.Models.Audit;
using Domain.Models.Products;
using Domain.Models.Users;
using GenericHelpers.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Misc.EnumsData;

namespace Data.Context
{
    public class SqlDbContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRoles, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public SqlDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<APIAuditLog> APIAuditLogs { get; set; }
        // public DbSet<PaymentGatewayLogs> PaymentGatewayLogs { get; set; }

       

       

        #region Users Module
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }


        #endregion Users Module

        #region Products Module
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion Products Module



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.ConfigureUserModelCreating(builder);
            // Seed data for developement 
            builder.Seed();
        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            Type type = inputObject.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);
            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;
            propertyVal = Convert.ChangeType(propertyVal, targetType);
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }

        void InitializeAudit()
        {
            var changeList = this.ChangeTracker.Entries().ToList();
            foreach (var entry in changeList)
            {
                var type = entry.Entity.GetType();
                switch (entry.State)
                {
                    case EntityState.Modified:
                        if (type.GetProperties().Where(x => x.Name == "LastModifiedOn").FirstOrDefault() != null)
                        {
                            DateTime? updatedOn = DateTime.Now;
                            SetValue(entry.Entity, "LastModifiedOn", updatedOn);
                            SetValue(entry.Entity, "LastModifiedBy", 1);
                            if (HttpContextHelper.GetUserIdFromClaim > 0)
                            {
                                SetValue(entry.Entity, "LastModifiedBy", HttpContextHelper.GetUserIdFromClaim);
                            }
                        }
                        break;
                    case EntityState.Added:

                        if (type.GetProperties().Where(x => x.Name == "CreatedOn").FirstOrDefault() != null)
                        {
                            SetValue(entry.Entity, "CreatedOn", DateTime.Now);
                            SetValue(entry.Entity, "CreatedBy", 1);
                            if (HttpContextHelper.GetUserIdFromClaim > 0)
                            {
                                SetValue(entry.Entity, "CreatedBy", HttpContextHelper.GetUserIdFromClaim);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public override int SaveChanges()
        {
            InitializeAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            InitializeAudit();

            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }


        private void ConfigureUserModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            //builder.Entity<IdentityUser<long>>(b =>
            //{
            //    b.ToTable("Users").HasKey(x=>x.Id);
            //});

            builder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("UserLogins");
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey }); ;
            });
            builder.Entity<UserRoles>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });


                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("UserTokens");
                b.HasKey(k => new { k.LoginProvider, k.UserId });
            });
            //builder.Entity<IdentityRole<long>>(b =>
            //{
            //    b.ToTable("Roles");
            //});

            builder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.ToTable("RoleClaims");
            });

        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                if (entry.Metadata.GetTableName() == "Attachments" || entry.Metadata.GetTableName() == "APIAuditLogs")
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntries.Add(auditEntry);
                // IEnumerable<string> modifiedProperties = entry.Metadata.get();
                foreach (var property in entry.Properties)
                {
                    /// string dbColumnName = property.Metadata.GetColumnName();
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (HttpContextHelper.GetUserIdFromClaim > 0)
                            {
                                auditEntry.AuditUser = HttpContextHelper.GetUserIdFromClaim;
                            }
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.AuditType = AuditType.Create;
                            break;

                        case EntityState.Deleted:
                            if (HttpContextHelper.GetUserIdFromClaim > 0)
                            {
                                auditEntry.AuditUser = HttpContextHelper.GetUserIdFromClaim;
                            }
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.AuditType = AuditType.Delete;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                //if (propertyName != "AccessFailedCount" && propertyName != "LockoutEnd" && propertyName!= "ConcurrencyStamp")
                                //{
                                var oldValue = originalValues[propertyName] != null ? originalValues[propertyName].ToString() : null;
                                var newValue = currentValues[propertyName] != null ? currentValues[propertyName].ToString() : null;

                                if (oldValue == newValue) { continue; };
                                if (HttpContextHelper.GetUserIdFromClaim > 0)
                                {
                                    auditEntry.AuditUser = HttpContextHelper.GetUserIdFromClaim;
                                }
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.AuditType = AuditType.Update;
                                //}
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AuditLogs.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }



    }

}
