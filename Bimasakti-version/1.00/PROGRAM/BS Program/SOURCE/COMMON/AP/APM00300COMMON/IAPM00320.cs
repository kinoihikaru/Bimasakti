using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APM00300COMMON
{
    public interface IAPM00320 
    {
        GLM00400SingleResult<APM00320DTO> SaveSupplierInfo(APM00320DTO poParam);
        GLM00400SingleResult<APM00320DTO> GetSupplierInfo(APM00320DTO poParam);
        IAsyncEnumerable<APM00321DTO> GetSupplierSeqList();
    }
}
