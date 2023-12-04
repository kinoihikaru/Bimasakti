using GSM01500COMMON.DTOs.GSM01500Print;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500SERVICE.DTOs
{
    public class GSM01500PrintLogKeyDTO
    {
        public GSM01500PrintCenterParameterDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
