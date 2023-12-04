using LMM04000COMMON.DTOs.LMM04010;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON
{
    public interface IUploadContractor
    {
        IAsyncEnumerable<UploadContractorDTO> GetUploadContractorList();
        IAsyncEnumerable<UploadContractorErrorDTO> GetErrorProcessList();
    }
}
