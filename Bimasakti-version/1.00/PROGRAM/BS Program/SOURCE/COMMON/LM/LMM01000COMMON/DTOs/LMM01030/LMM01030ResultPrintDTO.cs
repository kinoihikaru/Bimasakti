namespace LMM01000COMMON
{
    public class LMM01030ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01030DTO HeaderData { get; set; }
    }

    public class LMM01030ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01030ResultPrintDTO RatePG { get; set; }
    }
}
