using R_BlazorFrontEnd.Controls.TreeView;

namespace GSM09100MODEL
{
    public class GSM09100TreeDTO : R_TreeViewItemBase
    {
        public string CCATEGORY_ID { get; set; }
        public string DisplayTree { get; set; }
        public string ParentName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TypeDesc { get; set; }
        public int Level { get; set; }
    }
}
