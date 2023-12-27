using System;

namespace APT00300COMMON
{
    public class APT00300PeriodYearRangeDTO
    {
        //Parameter
        public string CYEAR { get; set; } = "";
        public string CMODE { get; set; } = "";


        //Result
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }

    }
}
