using System;
using System.Collections.Generic;

namespace APT00600COMMON
{
    public class APT00600InitTabListDTO
    {
        public APT00600PeriodYearRangeGSDTO VAR_GSM_PERIOD { get; set; }
        public List<APT00600PropertyDTO> VAR_PROPERTY_LIST { get; set; }
        public APT00600TodayDateDBDTO VAR_TODAY { get ; set; }
    }
    public class APT00600InitTabEntryDTO
    {
        public APT00600TodayDateDBDTO VAR_TODAY { get ; set; }
        public APT00600CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; }
        public APT00600TransCodeInfoGSDTO VAR_ADJUSTMENT_TRANS_CODE { get; set; } //VAR_TRANS_CODE
        public APT00600SystemParamGLDTO VAR_GL_SYSTEM_PARAM { get; set; }
        public APT00600PeriodDTInfoGSDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
        public List<APT00600PropertyDTO> VAR_PROPERTY_LIST { get; set; }
    }
    public class APT00600InitPopupAllocDTO
    {
        public APT00600CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; }
        public APT00600TransCodeInfoGSDTO VAR_ALLOCATION_TRANS_CODE { get; set; } 
        public List<APT00600AllocTrxTypeAPDTO> VAR_ALLOC_TRX_TYPE_LIST { get; set; }
    }
}
