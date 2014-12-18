using System.ComponentModel.DataAnnotations;

namespace jclitenet.Models
{
    public class BlogModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}