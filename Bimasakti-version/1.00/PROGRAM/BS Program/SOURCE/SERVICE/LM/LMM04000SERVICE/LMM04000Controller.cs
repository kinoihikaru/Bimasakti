using LMM04000BACK;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM04000Controller : ControllerBase, ILMM04000
    {
        private LoggerLMM04000 _logger;
        public LMM04000Controller(ILogger<LMM04000Controller> logger)
        {
            LoggerLMM04000.R_InitializeLogger(logger);
            _logger = LoggerLMM04000.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM04000DTO> GetContractorList()
        {
            _logger.LogInfo("Start || GetContractorList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM04000DTO> loRtn = null;
            LMM04000ParameterDTO loParam = new LMM04000ParameterDTO();
            LMM04000Cls loCls = new LMM04000Cls();
            List<LMM04000DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetContractorList(Controller)");
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04000_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetContractorList(Cls) || GetContractorList(Controller)");
                loTempRtn = loCls.GetContractorList(loParam);

                _logger.LogInfo("Run GetContractorStream(Controller) || GetContractorList(Controller)");
                loRtn = GetContractorStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetContractorList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<LMM04000DTO> GetContractorStream(List<LMM04000DTO> poParameter)
        {
            foreach (LMM04000DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateContractorDTO DownloadTemplateContractor()
        {
            _logger.LogInfo("Start || DownloadTemplateContractor(Controller)");
            R_Exception loException = new R_Exception();
            TemplateContractorDTO loRtn = new TemplateContractorDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateContractor(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_LM_API");
                var lcResourceFile = "BIMASAKTI_LM_API.Template.Contractor.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || DownloadTemplateContractor(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            GetPropertyListParameterDTO loParam = new GetPropertyListParameterDTO();
            LMM04000Cls loCls = new LMM04000Cls();
            List<GetPropertyListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParam);

                _logger.LogInfo("Run GetPropertyStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || GetPropertyList(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyListDTO> GetPropertyStream(List<GetPropertyListDTO> poParameter)
        {
            foreach (GetPropertyListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
