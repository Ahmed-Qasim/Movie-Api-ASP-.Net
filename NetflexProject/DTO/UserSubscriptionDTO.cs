using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetflexProject.DTO
{
    public class UserSubscriptionDTO
    {
       
        public string Fname { get; set; }
   
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int SubscriptionId { get; set; }
        

    }
}
