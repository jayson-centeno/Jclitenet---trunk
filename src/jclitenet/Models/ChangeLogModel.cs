using System.ComponentModel.DataAnnotations;
using System;

namespace jclitenet.Models
{
    public class ChangeLogModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        public DateTime? DateCreated { get; set; }
    }
}