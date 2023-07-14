using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public class LMM06500UploadFileDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }


}
