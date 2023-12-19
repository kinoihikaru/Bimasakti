using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00210ResultPrintDTO
    {
        public GLR00210DTO HeaderData { get; set; }
        public List<GLR00210DTO> SummaryData { get; set; }
        public List<GLR00212DTO> DetailData { get; set; }
    }

    public class GLR00210ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00210ResultPrintDTO GLR { get; set; }
    }
}
