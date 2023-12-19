using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00212DTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CGLACCOUNT_TYPE { get; set; }
        public string CACCOUNT { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public List<GLR00213DTO> AccountDetailData { get; set; }

    }
}
