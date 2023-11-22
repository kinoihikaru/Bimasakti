using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02520;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadUnitTypeCategory
    {
        IAsyncEnumerable<UploadUnitTypeCategoryDTO> GetUploadUnitTypeCategoryList();
        CheckIsUnitTypeCategoryUsedResultDTO CheckIsUnitTypeCategoryUsed();
        IAsyncEnumerable<UploadUnitTypeCategoryErrorDTO> GetErrorProcessList();
    }
}
