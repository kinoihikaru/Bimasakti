﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class GetContractorCategoryParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
