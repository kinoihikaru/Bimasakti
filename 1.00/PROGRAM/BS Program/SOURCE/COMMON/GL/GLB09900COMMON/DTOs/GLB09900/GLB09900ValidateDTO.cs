using R_APICommonDTO;

namespace GLB09900COMMON
{
    public class GLB09900ValidateDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPERIOD { get; set; }
        public int IERROR_COUNT { get; set; }
        public string CERROR_MESSAGE { get; set; }
    }


}
