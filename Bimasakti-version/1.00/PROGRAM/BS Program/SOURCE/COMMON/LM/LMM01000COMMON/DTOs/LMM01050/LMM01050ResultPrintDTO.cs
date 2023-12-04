using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01050ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01050DTO HeaderData { get; set; }
        public List<LMM01051DTO> DetailWKData { get; set; }
        public List<LMM01051DTO> DetailWDData { get; set; }
    }

    public class LMM01050ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01050ResultPrintDTO RateOT { get; set; }
    }
}
