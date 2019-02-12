using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaxLifts.Models
{
    public class SetsModel
    {
    }

    public class Set
    { 
        [Key]
        public int SetId { get; set; }
        public string User { get; set; }
        public string Exercise { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        [Display(Name = "Estimated Max")]
        public int CalculatedMax { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,
              DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date")]
        public DateTime SetDate { get; set; }
    }
    
}