using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM06500COMMON
{
    public class LMM06502DTO : R_APIResultBaseDTO
    {
        // param
        public string CPROPERTY_ID { get; set; }

        public string COLD_SUPERVISOR_ID { get; set; } = "";
        public string COLD_SUPERVISOR_NAME { get; set; } = "";
        public string COLD_DEPT_CODE { get; set; } = "";
        public string COLD_DEPT_NAME { get; set; } = "";
        public string CNEW_SUPERVISOR_ID { get; set; } = "";
        public string CNEW_SUPERVISOR_NAME { get; set; } = "";
        public string CNEW_DEPT_CODE { get; set; } = "";
        public string CNEW_DEPT_NAME { get; set; } = "";
        public string CSTAFF_LIST { get; set; } = "";
    }


}
