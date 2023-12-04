using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM01500COMMON.DTOs
{
    public class CopyFromProcessCompanyListDTO : R_APIResultBaseDTO
    {
        public List<CopyFromProcessCompanyDTO> Data { get; set; }
    }
}
