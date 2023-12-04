using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class SaveMultipleUserAssignmentParameterDTO
    {
        public List<string> CSELECTED_USER_ID_LIST { get; set; } = new List<string>();
        public string CAPPROVAL_CODE { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
