﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class GSM02541ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02541DTO> Data { get; set; }
    }
}
