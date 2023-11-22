using GSM02500COMMON.DTOs.GSM02530;
using System.Collections.Generic;

namespace GSM02500COMMON
{
    public interface IUploadUnit
    {
        IAsyncEnumerable<UploadUnitDTO> GetUploadUnitList();
        IAsyncEnumerable<UploadUnitErrorDTO> GetErrorProcessList();
    }
}
