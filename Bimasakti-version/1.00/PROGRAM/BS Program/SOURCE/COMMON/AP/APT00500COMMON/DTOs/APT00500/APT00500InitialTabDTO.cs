using System;
using System.Collections.Generic;

namespace APT00500COMMON
{
    public class APT00500InitTabListDTO
    {
        public APT00500PeriodYearRangeGSDTO VAR_GSM_PERIOD { get; set; }
        public List<APT00500PropertyDTO> VAR_PROPERTY_LIST { get; set; }
        public APT00500TodayDateDBDTO VAR_TODAY { get ; set; }
    }
    public class APT00500InitTabEntryDTO
    {
        public APT00500TodayDateDBDTO VAR_TODAY { get ; set; }
        public APT00500CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; }
        public APT00500TransCodeInfoGSDTO VAR_ADJUSTMENT_TRANS_CODE { get; set; } //VAR_TRANS_CODE
        public APT00500SystemParamGLDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public APT00500PeriodDTInfoGSDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
        public List<APT00500PropertyDTO> VAR_PROPERTY_LIST { get; set; }
    }
    public class APT00500InitPopupAllocDTO
    {
        public APT00500CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; }
        public APT00500TransCodeInfoGSDTO VAR_ALLOCATION_TRANS_CODE { get; set; } 
        public List<APT00500AllocTrxTypeAPDTO> VAR_ALLOC_TRX_TYPE_LIST { get; set; }
    }
}
