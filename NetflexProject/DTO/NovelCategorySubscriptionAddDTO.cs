namespace NetflexProject.DTO
{
    public class NovelCategorySubscriptionAddDTO
    {
        public string NovelName { get; set; }


        public string NovelDescription { get; set; }


        public double NovelRate { get; set; }


        public string NovelDate { get; set; }

        public string NovelImage { get; set; }

        public string NovelFile { get; set; }

        public int SubscriptionId { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
