namespace Web_ProjectName.ViewModels
{
    public class VM_CounterHome
    {
        public int status { get; set; }
        public string titleSmall { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<ChildrenObj> childrenObjs { get; set; }
        public class ChildrenObj
        {
            public string counter { get; set; }
            public string title { get; set; }
            public string imageUrl { get; set; }
        }
    }
}
