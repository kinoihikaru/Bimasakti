using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class ApprovalListDTO : R_APIResultBaseDTO
    {
        public List<ApprovalDTO> Data { get; set; }
    }
}
