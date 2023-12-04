using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02540;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadUnitPromotionType
    {
        IAsyncEnumerable<UploadUnitPromotionTypeDTO> GetUploadUnitPromotionTypeList();
        IAsyncEnumerable<UploadUnitPromotionTypeErrorDTO> GetErrorProcessList();
    }
}
