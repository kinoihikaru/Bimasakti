using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class ActivityApprovalListDTO : R_APIResultBaseDTO
    {
        public List<GSM07000DTO> Data { get; set; }
    }
}
