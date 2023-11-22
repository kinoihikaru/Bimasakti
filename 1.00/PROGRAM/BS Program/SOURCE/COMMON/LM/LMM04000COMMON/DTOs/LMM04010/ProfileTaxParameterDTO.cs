using LMM04000COMMON.DTOs.LMM04020;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class ProfileTaxParameterDTO 
    {
        public LMM04010DTO Profile { get; set; } = new LMM04010DTO();
        public LMM04020DTO TaxInfo { get; set; } = new LMM04020DTO();
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_TENANT_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
