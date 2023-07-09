using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Extensions
{
    public static class IQueryablePaginateExtensions
    {
        public static List<T> ToPaginate<T>(this IQueryable<T> source, int page, int size)
        {
            List<T> items = source.Skip(page * size).Take(size).ToList();
            return items;
        }
    }
}
