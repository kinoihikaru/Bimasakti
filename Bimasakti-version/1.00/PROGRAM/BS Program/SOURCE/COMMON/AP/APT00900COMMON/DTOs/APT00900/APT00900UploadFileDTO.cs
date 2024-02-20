using R_APICommonDTO;
using System;

namespace APT00900COMMON
{
    public class APT00900UploadFileDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
