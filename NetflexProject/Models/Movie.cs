using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflexProject.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public double Rate { get; set; }

        [Required]
        public string MovieDate { get; set; }

        public string MovieImage { get; set; }
        public string MovieBackDrop { get; set; }

        public string MovieVideo { get; set; }

        [ForeignKey("subscription")]
        public int SubscriptionID { get; set; }
        public virtual Subscription subscription { get; set; }
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();


    }
}
