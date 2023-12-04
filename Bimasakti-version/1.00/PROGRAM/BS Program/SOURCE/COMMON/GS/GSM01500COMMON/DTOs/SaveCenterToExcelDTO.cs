using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class SaveCenterToExcelDTO
    {
        public int No { get; set; } = 0;
        public string CenterCode { get; set; } = "";
        public string CenterName { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
