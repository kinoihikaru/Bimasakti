using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs.GSM01500Print
{
    public class GSM01500PrintCenterHeaderDTO
    {
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public List<GSM01500PrintCenterDetailDTO> DetailData { get; set; }
    }
}
