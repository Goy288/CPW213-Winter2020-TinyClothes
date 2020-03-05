using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Results = new List<Clothing>();
        }

        [StringLength(32)]
        public string Title { get; set; }
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing.
        /// </summary>
        public string Type { get; set; }

        [Range(0.0, 1000.0)]
        public double? MinPrice { get; set; }

        [Display(Name = "Maximum Price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Max price must be positive")]
        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }
    }
}
