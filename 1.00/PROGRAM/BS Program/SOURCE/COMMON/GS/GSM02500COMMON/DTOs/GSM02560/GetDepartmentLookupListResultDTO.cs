﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02560
{
    public class GetDepartmentLookupListResultDTO : R_APIResultBaseDTO
    {
        public List<GetDepartmentLookupListDTO> Data { get; set; }
    }
}
