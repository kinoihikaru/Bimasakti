using R_APICommonDTO;
using System;

namespace GLB00600COMMON
{
    public class GLB00600InitialDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public string CYEAR { get; set; }
        public DateTime DTODAY { get; set; }
        public int INO_PERIOD { get; set; }
        public bool LPERIOD_MODE { get; set; }
        public bool LVALID { get; set; }
    }


}
