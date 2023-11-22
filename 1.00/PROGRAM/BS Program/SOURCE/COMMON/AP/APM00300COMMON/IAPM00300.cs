using System;
using System.Collections.Generic;

namespace APM00300COMMON
{
    public interface IAPM00300
    {
        GLM00400SingleResult<APM00300InitialDTO> GetInitial();

        IAsyncEnumerable<APM00300PropertyDTO> GetGSPropertyList();
        IAsyncEnumerable<APM00300LOBDTO> GetLOBList();
        IAsyncEnumerable<APM00300DTO> GetAPSearchSupplierList();
    }
}
