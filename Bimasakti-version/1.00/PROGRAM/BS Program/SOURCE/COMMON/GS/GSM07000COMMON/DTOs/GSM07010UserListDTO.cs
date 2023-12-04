using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class GSM07010UserListDTO : R_APIResultBaseDTO
    {
        public List<GSM07010UserDTO> Data { get; set; } 
    }
}
