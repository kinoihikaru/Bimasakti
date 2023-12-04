using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class GSM01510DepartmentListDTO : R_APIResultBaseDTO
    {
        public List<GSM01510DepartmentDTO> Data { get; set; }
    }
}
