using R_APICommonDTO;

namespace GLB00600COMMON
{
    public class GLB00600GSMTransactionCodeDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }

        // result
        public bool LINCREMENT_FLAG { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
    }


}
