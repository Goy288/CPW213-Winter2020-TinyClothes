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
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing.
        /// </summary>
        [Required]
        public string Type { get; set; }

        // The clothing's color.
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the item
        /// </summary>
        [Required]
        [Range(0.00, 1000000000.00)]
        public double Price { get; set; }

        /// <summary>
        /// The clothing's display title.
        /// </summary>
        [Required]
        [StringLength(32)]
        // Sample Regex, great for validation.
        //[RegularExpression("^([A-Za-z0-9])+$",
        // ErrorMessage = "lmao error")]
        public string Title { get; set; }

        /// <summary>
        /// Describes the clothing.
        /// </summary>
        [StringLength(1024)]
        public string Description { get; set; }

    }
}
