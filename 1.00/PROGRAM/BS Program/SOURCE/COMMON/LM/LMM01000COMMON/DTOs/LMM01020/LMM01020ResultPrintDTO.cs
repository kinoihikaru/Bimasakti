namespace LMM01000COMMON
{
    public class LMM01020ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LMM01020DTO HeaderData { get; set; }
    }

    public class LMM01020ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LMM01020ResultPrintDTO RateWG { get; set; }
    }
}
