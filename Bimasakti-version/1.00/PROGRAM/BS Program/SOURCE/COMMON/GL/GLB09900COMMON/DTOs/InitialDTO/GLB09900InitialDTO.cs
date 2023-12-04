using R_APICommonDTO;
using System;

namespace GLB09900COMMON
{
    public class GLB09900InitialDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public DateTime DTODAY { get; set; }
    }


}
