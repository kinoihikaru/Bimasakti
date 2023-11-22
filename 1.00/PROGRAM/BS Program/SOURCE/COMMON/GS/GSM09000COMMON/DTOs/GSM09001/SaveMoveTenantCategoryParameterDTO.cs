using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09001
{
    public class SaveMoveTenantCategoryParameterDTO
    {
        public string CTENANT_ID { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CFROM_TENANT_CATEGORY_ID { get; set; }
        public string CTO_TENANT_CATEGORY_ID { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
