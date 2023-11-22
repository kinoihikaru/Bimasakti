using System.Collections.Generic;

namespace GSM03000Common.DTOs
{
    public class GSM03000ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public List<GSM03000ResultDTO> Data { get; set; }
    }

    public class GSM03000ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GSM03000ResultPrintDTO OtherCharges { get; set; }
    }
}
