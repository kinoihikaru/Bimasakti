using System;

namespace CBM00100COMMON
{
    public interface ICBM00100
    {
        CBM00100SingleResult<CBM00100ValidateInitDTO> GetInitValidate();

        CBM00100SingleResult<CBM00100DTO> GetSystemParamCB();

        CBM00100SingleResult<CBM00100DTO> SaveSystemParamCB(CBM00100SaveParameterDTO poEntity);
    }
}
