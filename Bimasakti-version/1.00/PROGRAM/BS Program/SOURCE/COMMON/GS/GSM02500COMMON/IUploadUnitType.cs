using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02503;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadUnitType
    {
        IAsyncEnumerable<UploadUnitTypeDTO> GetUploadUnitTypeList();
        IAsyncEnumerable<UploadUnitTypeErrorDTO> GetErrorProcessList();
        CheckIsUnitTypeUsedResultDTO CheckIsUnitTypeUsed();
    }
}
