using GSM01500BACK;
using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using GSM01500COMMON.Loggers;
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

namespace GSM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadCenterController : ControllerBase, IUploadCenter
    {
        private LoggerUploadCenter _logger;

        public UploadCenterController(ILogger<UploadCenterController> logger)
        {
            LoggerUploadCenter.R_InitializeLogger(logger);
            _logger = LoggerUploadCenter.R_GetInstanceLogger();
        }

        [HttpPost]
        public CompanyResultDTO GetCompany()
        {
            _logger.LogInfo("Start || GetCompany(Controller)");
            R_Exception loException = new R_Exception();
            CompanyResultDTO loRtn = new CompanyResultDTO();
            CompanyParameterDTO loParam = new CompanyParameterDTO();
            UploadCenterCls loCls = new UploadCenterCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetCompany(Controller)");
                loParam.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetCompany(Cls) || GetCompany(Controller)");
                loRtn.Data = loCls.GetCompany(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCompany(Controller)");
            return loRtn;
        }
    }
}
