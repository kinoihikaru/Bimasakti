namespace LMM01000COMMON
{
    public class LMM01040ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01040DTO HeaderData { get; set; }
    }

    public class LMM01040ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01040ResultPrintDTO RateGU { get; set; }
    }
}
