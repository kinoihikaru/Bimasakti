using R_CommonFrontBackAPI;
using System;

namespace CBM00100COMMON
{
    public class CBM00100SaveParameterDTO
    {
        public CBM00100DTO Entity { get; set; }
        public eCRUDMode CRUDMode { get; set; }
    }
}
