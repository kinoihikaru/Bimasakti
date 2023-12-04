using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01000ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01000ColumnPrintDTO Column { get; set; }
        public List<LMM01000TopPrintDTO> Data { get; set; }
    }

    public class LMM01000ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01000ResultPrintDTO UtilitiesData { get; set; }
    }
}
