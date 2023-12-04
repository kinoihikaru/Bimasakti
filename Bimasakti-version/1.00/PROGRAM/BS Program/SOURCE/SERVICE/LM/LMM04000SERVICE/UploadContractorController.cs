using LMM04000BACK;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04010;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadContractorController : ControllerBase, IUploadContractor
    {
        [HttpPost]
        public IAsyncEnumerable<UploadContractorDTO> GetUploadContractorList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<UploadContractorDTO> loRtn = null;
            List<UploadContractorDTO> loParam = new List<UploadContractorDTO>();
            UploadContractorCls loCls = new UploadContractorCls();
            List<UploadContractorDTO> loTempRtn = null;

            try
            {
                loParam = R_Utility.R_GetStreamingContext<List<UploadContractorDTO>>(ContextConstant.UPLOAD_CONTRACTOR_STREAMING_CONTEXT);

                loTempRtn = loCls.GetUploadContractorList(loParam);

                loRtn = GetUploadContractorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<UploadContractorDTO> GetUploadContractorStream(List<UploadContractorDTO> poParameter)
        {
            foreach (UploadContractorDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<UploadContractorErrorDTO> GetErrorProcessList()
        {
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<UploadContractorErrorDTO> loRtn = null;
            ValidateUploadContractorCls loCls = new ValidateUploadContractorCls();
            List<UploadContractorErrorDTO> loTempRtn = null;
            string lcKeyGuid;

            try
            {
                lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstant.UPLOAD_CONTRACTOR_ERROR_GUID_STREAMING_CONTEXT);

                loTempRtn = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                loRtn = GetErrorProcessStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<UploadContractorErrorDTO> GetErrorProcessStream(List<UploadContractorErrorDTO> poParameter)
        {
            foreach (UploadContractorErrorDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
