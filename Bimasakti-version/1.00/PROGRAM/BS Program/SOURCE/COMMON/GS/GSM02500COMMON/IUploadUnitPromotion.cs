using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IUploadUnitPromotion
    {
        IAsyncEnumerable<UploadUnitPromotionDTO> GetUploadUnitPromotionList();
        IAsyncEnumerable<UploadUnitPromotionErrorDTO> GetErrorProcessList();
    }
}
