using R_APICommonDTO;

namespace GLB00600COMMON
{
    public class GLB00600SuspenseAmountDTO
    {
        // param
        public string CGLACCOUNT_NO { get; set; }
        public string CPERIOD { get; set; }

        // result
        public int NSUSPENSE { get; set; }
    }


}
