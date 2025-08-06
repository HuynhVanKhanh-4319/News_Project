namespace Web_ProjectName.Models.Common
{
    public class Config_Supplier
    {
        public int id { get; set; }
        public string refCode { get; set; }
        public string package { get; set; }
        public bool catalogueProduct { get; set; }
        public bool catalogueIntroduce { get; set; }
        public bool catalogueProject { get; set; }
        public bool catalogueService { get; set; }
        public bool catalogueSolution { get; set; }
    }
}
