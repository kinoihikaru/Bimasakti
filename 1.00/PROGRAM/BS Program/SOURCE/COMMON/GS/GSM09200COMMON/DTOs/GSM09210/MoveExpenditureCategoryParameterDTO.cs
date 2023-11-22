using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09210
{
    public class MoveExpenditureCategoryParameterDTO
    {
        public string CEXPENDITURE_ID { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CFROM_EXPENDITURE_CATEGORY_ID { get; set; }
        public string CTO_EXPENDITURE_CATEGORY_ID { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
