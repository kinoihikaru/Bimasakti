using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs.GSM01500Print
{
    public class GSM01500PrintCenterResultDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public bool PrintDept { get; set; }
        public GSM01500PrintCenterColumnDTO Column { get; set; }
        public List<GSM01500PrintCenterHeaderDTO> Data { get; set; }
    }

    public class GSM01500PrintCenterResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GSM01500PrintCenterResultDTO CenterData { get; set; }
    }
}
