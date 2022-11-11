IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [APIAuditLogs] (
    [Id] bigint NOT NULL IDENTITY,
    [AuditType] nvarchar(max) NULL,
    [Request] nvarchar(max) NULL,
    [Response] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    CONSTRAINT [PK_APIAuditLogs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AuditLogs] (
    [Id] uniqueidentifier NOT NULL,
    [AuditDateTime] datetime2 NOT NULL,
    [AuditType] nvarchar(max) NULL,
    [AuditUser] bigint NOT NULL,
    [TableName] nvarchar(max) NULL,
    [KeyValues] nvarchar(max) NULL,
    [OldValues] nvarchar(max) NULL,
    [NewValues] nvarchar(max) NULL,
    [ChangedColumns] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PermissionGroups] (
    [Id] bigint NOT NULL IDENTITY,
    [GroupNameEn] nvarchar(max) NULL,
    [GroupNameAr] nvarchar(max) NULL,
    CONSTRAINT [PK_PermissionGroups] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [FullName] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Permissions] (
    [Id] bigint NOT NULL IDENTITY,
    [PermissionKey] nvarchar(max) NULL,
    [PermissionNameEn] nvarchar(max) NULL,
    [PermissionNameAr] nvarchar(max) NULL,
    [PermissionGroupId] bigint NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Permissions_PermissionGroups_PermissionGroupId] FOREIGN KEY ([PermissionGroupId]) REFERENCES [PermissionGroups] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Roles] (
    [Id] bigint NOT NULL IDENTITY,
    [IsDeletable] bit NOT NULL,
    [UserId] bigint NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Roles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserPasswordHistories] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    CONSTRAINT [PK_UserPasswordHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPasswordHistories_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTokens] (
    [UserId] bigint NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([LoginProvider], [UserId]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] bigint NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RolePermissions] (
    [Id] bigint NOT NULL IDENTITY,
    [RoleId] bigint NOT NULL,
    [PermissionId] bigint NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [UserId] bigint NOT NULL,
    [RoleId] bigint NOT NULL,
    [RoleId1] bigint NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Roles_RoleId1] FOREIGN KEY ([RoleId1]) REFERENCES [Roles] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Permissions_PermissionGroupId] ON [Permissions] ([PermissionGroupId]);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
GO

CREATE INDEX [IX_RolePermissions_PermissionId] ON [RolePermissions] ([PermissionId]);
GO

CREATE INDEX [IX_RolePermissions_RoleId] ON [RolePermissions] ([RoleId]);
GO

CREATE INDEX [IX_Roles_UserId] ON [Roles] ([UserId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserPasswordHistories_UserId] ON [UserPasswordHistories] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [IX_UserRoles_RoleId1] ON [UserRoles] ([RoleId1]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_UserTokens_UserId] ON [UserTokens] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221110082403_initDb', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221110123313_product-table', N'6.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ProductCategories] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Barcode] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ProductCategoryId] bigint NOT NULL,
    [Status] int NOT NULL,
    [IsWeighted] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedBy] bigint NULL,
    [LastModifiedOn] datetime2 NULL,
    [LastModifiedBy] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [DeletedBy] bigint NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_ProductCategories_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Products_ProductCategoryId] ON [Products] ([ProductCategoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221110123555_products', N'6.0.6');
GO

COMMIT;
GO


DROP PROCEDURE IF EXISTS [GetProductCountByStatus];
GO
CREATE PROCEDURE [GetProductCountByStatus] 
AS
	BEGIN;
	SET NOCOUNT ON;
	SELECT
		SUM(CASE  WHEN  (p.Status  = 1 ) THEN  1 ELSE 0 END  ) as [Sold],
		SUM(CASE WHEN  (p.Status  = 2 ) THEN  1 ELSE 0 END  ) as [InStock],
		SUM( CASE  WHEN  (p.Status  = 3 ) THEN  1 ELSE 0 END  ) as [Damaged]
	FROM Products p
	SET NOCOUNT OFF;
END;

