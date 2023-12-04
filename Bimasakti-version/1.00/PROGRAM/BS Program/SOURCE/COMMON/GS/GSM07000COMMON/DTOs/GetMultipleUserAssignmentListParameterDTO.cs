using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class GetMultipleUserAssignmentListParameterDTO
    {
        public string LOGIN_COMPANY_ID { get; set; } = "";
        public string CAPPROVAL_CODE { get; set; } = "";
    }
}
