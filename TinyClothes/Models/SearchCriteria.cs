﻿using System;
using System.Collections.Generic;
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

        public string Title { get; set; }
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing.
        /// </summary>
        public string Type { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }
    }
}
