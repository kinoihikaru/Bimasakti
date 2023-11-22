using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM03000Common.DTOs
{
    public class GSM03000PropertyDTO
    {
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }

    public class GSM03000PropertyListDTO : R_APIResultBaseDTO
    {
        public List<GSM03000PropertyDTO> Data { get; set; }
    }
}
