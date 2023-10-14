namespace NetflexProject.DTO
{
    public class MovieCategorySubscriptionDTO
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
        public string CategoryName { get; set; }
        public string MovieVedio { get; internal set; }
    }
}
