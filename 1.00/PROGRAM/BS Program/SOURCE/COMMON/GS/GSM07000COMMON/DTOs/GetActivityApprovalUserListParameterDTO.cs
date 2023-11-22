using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class GetActivityApprovalUserListParameterDTO
    {
        public string COMPANY_ID { get; set; } = "";
        public string SELECTED_APPROVAL_CODE { get; set; } = "";
        public string LOGIN_USER_ID { get; set; } = "";
    }
}
