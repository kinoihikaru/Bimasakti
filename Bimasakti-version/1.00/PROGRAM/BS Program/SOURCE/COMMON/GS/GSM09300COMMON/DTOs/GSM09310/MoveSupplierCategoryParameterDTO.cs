using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09310
{
    public class MoveSupplierCategoryParameterDTO
    {
        public string CSUPPLIER_ID { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CFROM_SUPPLIER_CATEGORY_ID { get; set; }
        public string CTO_SUPPLIER_CATEGORY_ID { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
