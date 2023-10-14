using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflexProject.Models
{
    public class Novel
    {
        [Key]
        public int NovelID { get; set; }
        [Required]
        public string NovelName { get; set; }
        [Required]
        
        public string NovelDescription { get; set; }
        [Required]

        public double NovelRate { get; set; }
        [Required]

        public string NovelDate { get; set; }

        public string NovelImage { get; set; }

        public string NovelFile { get; set; }

        [ForeignKey("subscription")]

        public int SubscriptionID { get; set; }
        public virtual Subscription subscription { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

    }
}
