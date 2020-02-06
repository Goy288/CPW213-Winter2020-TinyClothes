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
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        // The clothing's color.
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the item
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.00, 1000000000.00)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The clothing's display title.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [StringLength(32)]
        // Sample Regex, great for validation.
        //[RegularExpression("^([A-Za-z0-9])+$",
        // ErrorMessage = "lmao error")]
        public string Title { get; set; }

        /// <summary>
        /// Describes the clothing.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1024)]
        public string Description { get; set; }

    }
}
