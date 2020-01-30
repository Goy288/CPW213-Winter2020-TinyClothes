using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains database helper methods for <see cref="Models.Clothing"">
    /// </summary>
    public static class ClothingDB
    {
        /// <summary>
        /// Gets a specific page of cloting items 
        /// sorted by ItemID in ascending order.
        /// </summary>
        /// <param name="pageNum">The page you want.</param>
        /// <param name="pageSize">The number of clothing items per page.</param>
        /// <returns></returns>
        public static async Task<List<Clothing>> GetClothingbyPage(
                            StoreContext context, int pageNum, byte pageSize)
        {
            // If you wanted page 1, we wouldn't skip
            // any rows, so we must offset by 1
            const int PageOffset = 1;

            //LINQ Method Syntax
            List<Clothing> clothes = 
                await context.Clothing
                             .Skip(pageSize * (pageNum - 1))
                             .Take(pageSize)
                             .OrderBy(c => c.ItemID)
                             .ToListAsync();

            return clothes;

            /* LINQ Query Syntax - Same as above
            List<Clothing> clothes2 = await
            (
                from c in context.Clothing
                orderby c.ItemID ascending
                select c;
            ).Skip(pageSize * (pageNum - 1))
             .Take(pageSize)
             .OrderBy(c => c.ItemID)
             .ToListAsync();
            */

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Add a clothing object to the database.
        /// </summary>
        /// <param name="c">The clothing object to be added</param>
        /// <returns>The clothing object with the ID populated</returns>
        public static async Task<Clothing> Add(Clothing c, StoreContext context)
        {
            await context.AddAsync(c); // Prepares the INSERT query
            await context.SaveChangesAsync(); // Executes the INSERT query

            return c;
        }
    }
}
