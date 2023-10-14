using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflexProject.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPass { get; set; }
        [ForeignKey("subscription")]
        
        public int SubscriptionID { get; set; }
        public virtual Subscription subscription { get; set; }
    }
}
