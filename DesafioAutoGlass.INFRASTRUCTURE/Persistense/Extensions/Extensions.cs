using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAutoGlass.INFRASTRUCTURE.Persistense.Extensions
{
    public static class Extensions
    {
        public static async Task<PaginationResult<TEntity>> GetPagination<TEntity>(this IQueryable<TEntity> query,
            int page,
            int pageSize) where TEntity : class
        {
            PaginationResult<TEntity> paginationResult = new PaginationResult<TEntity>();

            paginationResult.ItemsCount = await query.CountAsync();
            paginationResult.Page = page;
            paginationResult.PageSize = pageSize;

            var pageCount = (double)paginationResult.ItemsCount / pageSize;
            paginationResult.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            paginationResult.Data = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return paginationResult;
        }
    }
}