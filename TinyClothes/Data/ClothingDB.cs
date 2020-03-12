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
        /// Returns the total number of Clothing items
        /// </summary>
        /// <returns></returns>
        public async static Task<int> GetNumClothing(StoreContext context)
        {
            return await context.Clothing.CountAsync();
        }

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
                             .Skip(pageSize * (pageNum - PageOffset))
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

        internal static Task<Clothing> GetClothingbyID(int? id, StoreContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a single clothing item if there
        /// is a match, null if otherwise
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <param name="context">DB Context</param>
        /// <returns>A single clothing item</returns>
        public static async Task<Clothing> GetClothingbyID(int id, StoreContext context)
        {
            Clothing c = await (
                from clothing in context.Clothing
                where clothing.ItemID == id
                select clothing
            ).SingleOrDefaultAsync();
            return c;
        }

        public static async Task<Clothing> Edit(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return c;
        }

        public static async Task Delete(int id, StoreContext context)
        {
            Clothing c = await GetClothingbyID(id, context);
                
            if (c != null)
            {
                await Delete(c, context);
            }
        }

        public async static Task Delete(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Deleted;
            await context.SaveChangesAsync();
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

        public static async Task BuildSearchQuery(SearchCriteria search, StoreContext context)
        {
            // Prepare query - SELECT * FROM Clothes
            // Does not get sent to DB
            IQueryable<Clothing> allClothes = from c in context.Clothing
                                              select c;

            // WHERE MinPrice < Price
            if (search.MinPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price >= search.MinPrice
                             select c;
            }

            // WHERE Price < MaxPrice
            if (search.MaxPrice.HasValue)
            {
                allClothes = from c in allClothes
                             where c.Price <= search.MaxPrice
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Size))
            {
                allClothes = from c in allClothes
                             where c.Size == search.Size
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allClothes = from c in allClothes
                             where c.Size == search.Type
                             select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allClothes = from c in allClothes
                             where c.Title.Contains(search.Title)
                             select c;
            }

            search.Results = await allClothes.ToListAsync();
        }
    }
}
