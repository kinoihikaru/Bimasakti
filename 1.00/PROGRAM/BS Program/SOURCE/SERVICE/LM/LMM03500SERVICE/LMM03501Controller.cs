using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.Logging;
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

namespace LMM03500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03501Controller : ControllerBase, ILMM03501
    {
        private LoggerLMM03501 _logger;
        public LMM03501Controller(ILogger<LoggerLMM03501> logger)
        {
            LoggerLMM03501.R_InitializeLogger(logger);
            _logger = LoggerLMM03501.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03501DTO> GetTenantList()
        {
            _logger.LogInfo("Start || GetTenantList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM03501DTO> loRtn = null;
            LMM03501ParameterDTO loParam = new LMM03501ParameterDTO();
            LMM03501Cls loCls = new LMM03501Cls();
            List<LMM03501DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03501_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenantList(Cls) || GetTenantList(Controller)");
                loTempRtn = loCls.GetTenantList(loParam);

                _logger.LogInfo("Run GetTenantStream(Controller) || GetTenantList(Controller)");
                loRtn = GetTenantStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<LMM03501DTO> GetTenantStream(List<LMM03501DTO> poParameter)
        {
            foreach (LMM03501DTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public TemplateTenantDTO DownloadTemplateTenant()
        {
            _logger.LogInfo("Start || DownloadTemplateTenant(Controller)");
            R_Exception loException = new R_Exception();
            TemplateTenantDTO loRtn = new TemplateTenantDTO();

            try
            {
                var loAsm = Assembly.Load("BIMASAKTI_LM_API");
                var lcResourceFile = "BIMASAKTI_LM_API.Template.Tenant.xlsx";

                _logger.LogInfo("Read File || DownloadTemplateTenant(Controller)");
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

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || DownloadTemplateTenant(Controller)");
            return loRtn;
        }

    }
}
