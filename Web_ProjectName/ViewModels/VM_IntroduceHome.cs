namespace Web_ProjectName.ViewModels
{
    public class VM_IntroduceHome
    {
        public string id { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public string titleSmall { get; set; }
        public string description { get; set; }
        public string descriptionSmall { get; set; }
        public string link { get; set; }
        public string linkTarget { get; set; }
        public string linkText { get; set; }
        public List<ChildrenObj> childrenObjs { get; set; }
        public class ChildrenObj
        {
            public string number { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string marginClass { get; set; }
        }
    }
}
