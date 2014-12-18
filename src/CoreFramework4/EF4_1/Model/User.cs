using System;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFramework4.Model
{
    [Serializable]
    [Table("User")]
    public class User
    {
        public User() { }

        public int ID { get; set; }

        [Key]
        public Guid UID { get; set; }
        public Guid User_PKID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsGuest { get; set; }
        public string Email { get; set; }
        public string Name 
        {
            get { return LastName + ", " + FirstName; }
        }
        public string Password { get; set; }
    }
}