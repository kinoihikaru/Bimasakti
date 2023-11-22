using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01000TopPrintDTO
    {
        public string CCHARGES_TYPE_DESCR { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public List<LMM01000HeaderPrintDTO> DataCharges { get; set; }
    }
}
