namespace Web_ProjectName.ViewModels
{
    public class VM_SiteConfig
    {
        public List<MenuConfig> menuConfig { get; set; }
        public HomeConfig homeConfig { get; set; }
        public CatalogueHeaderConfig catalogueHeaderConfig { get; set; }
        public class MenuConfig
        {
            public string name { get; set; }
            public string page { get; set; }
            public int status { get; set; }
            public int sort { get; set; }
        }
        public class HomeConfig
        {
            public int layoutId { get; set; }
        }
        public class CatalogueHeaderConfig
        {
            public int status { get; set; }
            public string url { get; set; }
            public string target { get; set; }
            public string name { get; set; }
        }
    }
}
