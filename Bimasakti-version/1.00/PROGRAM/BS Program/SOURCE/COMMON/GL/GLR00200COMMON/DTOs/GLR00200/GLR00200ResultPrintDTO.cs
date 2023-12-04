using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00200ResultPrintDTO
    {
        public GLR00200DTO SummaryData { get; set; }
        public List<GLR00202DTO> DetailData { get; set; }
    }

    public class GLR00200ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00200ResultPrintDTO GLR { get; set; }
    }
}
