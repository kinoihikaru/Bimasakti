using R_APICommonDTO;

namespace GLB00600COMMON
{
    public class GLB00600DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CMANAGER_NAME { get; set; }
    }

}
