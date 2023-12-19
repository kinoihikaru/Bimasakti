using System;

namespace GSM05000COMMON_FMC
{
    public class GSM05000ValidateDTO
    {
        //Param
        public string CTRANS_CODE { get; set; }
        public GSM05000eTabName ETAB_INDEX { get; set; }

        //Result
        public int EXIST { get; set; } = 0;
    }
}