namespace LMM01000COMMON
{
    public class LMM01010ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01010DTO HeaderData { get; set; }
    }

    public class LMM01010ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01010ResultPrintDTO RateEC { get; set; }
    }
}
