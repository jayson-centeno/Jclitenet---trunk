using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Serializable]
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Column("Comment")]
        public string CommentText { get; set; }

        //public virtual User CommentBy { get; set; }
        //public virtual Comment ReplyToComment { get; set; }

        public string AnonymousName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public string Website { get; set; }

        public int? Tutorial_ID { get; set; }

        public int? Game_ID { get; set; }

        [ForeignKey("Tutorial_ID")]
        public virtual Tutorial Tutorial { get; set; }

        [ForeignKey("Game_ID")]
        public virtual Game Game { get; set; }

        public DateTime? DateCreated { get; set; }
        public bool? IsDeleted { get; set; }

        public string IpAddress { get; set; }
        public bool? IsValidWebSite { get; set; }
    }
}