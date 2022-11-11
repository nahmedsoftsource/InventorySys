using System.Linq.Dynamic.Core;

namespace Domain.Extensions
{
    public static class LinqExtensions
    {

        public static IQueryable<T> ToPagedIQueryable<T>(this IQueryable<T> query,
                                             int page, int pageSize) where T : class
        {

            if (page == 1)
                return query.Take(pageSize);
            else
                return query.Skip((page - 1) * pageSize).Take(pageSize);

        }
        public static List<T> ToPagedList<T>(this IQueryable<T> query,
                                           int page, int pageSize) where T : class
        {
            if (page > 0 && pageSize > 0)
            {
                return query.ToPagedIQueryable(page, pageSize).ToList();
            }
            else
            {
                return query.ToList();
            }

        }
        public static List<T> ToOrderedPagedList<T>(this IQueryable<T> query,
                                           int page, int pageSize, string orderby = null, string sortDirection = "ASC") where T : class
        {
            //sortDirection = "DESC" or "ASC"
            if (!string.IsNullOrEmpty(orderby) && orderby != "undefined")
            {
                query = query.OrderBy(orderby + " " + sortDirection);
            }

            return query.ToPagedList(page, pageSize);
        }
    }
}
