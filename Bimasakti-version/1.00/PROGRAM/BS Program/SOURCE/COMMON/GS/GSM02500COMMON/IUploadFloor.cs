using GSM02500COMMON.DTOs.GSM02520;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadFloor
    {
        IAsyncEnumerable<UploadFloorDTO> GetUploadFloorList();
        IAsyncEnumerable<UploadFloorErrorDTO> GetErrorProcessList();
        CheckIsFloorUsedResultDTO CheckIsFloorUsed();
    }
}
