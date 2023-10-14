namespace NetflexProject.DTO
{
    public class MovieCategorySubscriptionAddDTO
    {

        public string MovieName { get; set; }


        public string MovieDescription { get; set; }


        public double Rate { get; set; }


        public string MovieDate { get; set; }

        public string MovieImage { get; set; }
        public string MovieBackdrop { get; set; }

        public string MovieVideo { get; set; }

        public int SubscriptionId { get; set; }
        public List<int> CategoriesId { get; set; }

    }
}
