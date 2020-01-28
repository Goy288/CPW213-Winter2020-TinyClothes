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
        public static List<Clothing> GetAllClothing()
        {
            throw new NotImplementedException();
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
