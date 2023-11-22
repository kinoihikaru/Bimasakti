using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02531;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadUnitUtility
    {
        IAsyncEnumerable<UploadUnitUtilityDTO> GetUploadUnitUtilityList();
        IAsyncEnumerable<UploadUnitUtilityErrorDTO> GetErrorProcessList();
        IAsyncEnumerable<GetAllUnitUtilityDTO> GetAllUnitUtilities();
    }
}
