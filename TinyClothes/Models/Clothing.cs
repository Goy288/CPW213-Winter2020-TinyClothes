using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class Clothing
    {
        /// <summary>
        /// The unique ID for the clothing item
        /// </summary>
        [Key]
        public int ItemID { get; set; }

        /// <summary>
        /// The clothing's size,
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing.
        /// </summary>
        public string Type { get; set; }

        // The clothing's color.
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The clothing's display title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Describes the clothing.
        /// </summary>
        public string Description { get; set; }

    }
}
