namespace NetflexProject.DTO
{
    public class MovieCategorySubscriptionGetDTO
    {

        public int MovieID { get; set; }


        public string MovieName { get; set; }


        public string MovieDescription { get; set; }


        public double Rate { get; set; }


        public string MovieDate { get; set; }

        public string MovieImage { get; set; }
        public string MovieBackDrop { get; set; }

        public string MovieVideo { get; set; }

        public string SubscriptionType { get; set; }
        public List<string> CategoryNames { get; set; }

    }
}
