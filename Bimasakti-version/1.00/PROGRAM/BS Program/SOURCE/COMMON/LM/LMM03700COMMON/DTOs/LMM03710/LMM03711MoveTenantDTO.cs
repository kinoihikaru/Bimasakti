using System;

namespace LMM03700COMMON
{
    public class LMM03711MoveTenantDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_CLASSIFICATION_GROUP_ID { get; set; }
        public string CFROM_TENANT_CLASSIFICATION_ID { get; set; }
        public string CFROM_TENANT_CLASSIFICATION_NAME { get; set; }
        public string CTO_TENANT_CLASSIFICATION_ID { get; set; }
        public string CTO_TENANT_CLASSIFICATION_NAME { get; set; }
        public string CTENANT_LIST { get; set; }
    }
}
