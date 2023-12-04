using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class GetCenterFromExcelDTO
    {
        public string CenterCode { get; set; } = "";
        public string CenterName { get; set; } = "";    
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
    }
}
