using System;

namespace GSM05000COMMON_FMC
{
    public class GSM05020ValidateDTO
    {
        //Param
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }

        //Result
        public int EXIST { get; set; } = 0;
    }
}