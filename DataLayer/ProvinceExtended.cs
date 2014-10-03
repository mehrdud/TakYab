﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [MetadataTypeAttribute(typeof(ProvinceMetadata))]
    public partial class Province
    {
        public override string ToString()
        {
            return this.Name;
        }
    }


    class ProvinceMetadata
    {
        [Required]
        [Display(Name = "استان")]
        public string Name { get; set; }

        [Display(Name = "توضیحات بیشتر")]
        public string Description { get; set; }

        [Display(Name = "ترتیب")]
        public Nullable<int> SortOrder { get; set; }

         
        
    }
}
