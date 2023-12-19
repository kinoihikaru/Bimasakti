using System;
using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00213DTO
    {
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public List<GLR00214DTO> CenterDetailData { get; set; }
    }
}
