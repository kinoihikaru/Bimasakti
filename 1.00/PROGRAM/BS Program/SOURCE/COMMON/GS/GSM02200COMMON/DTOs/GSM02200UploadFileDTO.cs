using R_APICommonDTO;
using System;

namespace GSM02200COMMON
{
    public class GSM02200UploadFileDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
