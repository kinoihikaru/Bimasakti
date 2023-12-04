using GSM03000Common.DTOs;
using R_CommonFrontBackAPI.Log;

namespace GSM03000SERVICE
{
    public class GSM03000PrintLogKeyDTO
    {
        public GSM03000PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
