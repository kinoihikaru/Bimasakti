using APM00100BACK;
using APM00100COMMON;
using APM00100COMMON.DTOs.APM00100;
using APM00100COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APM00100Controller : ControllerBase, IAPM00100
    {
        private LoggerAPM00100 _logger;
        public APM00100Controller(ILogger<APM00100Controller>logger) 
        {
            LoggerAPM00100.R_InitializeLogger(logger);
            _logger = LoggerAPM00100.R_GetInstanceLogger();
        }
        [HttpPost]
        public GetAPMSystemParamResultDTO GetAPMSystemParam()
        {
            _logger.LogInfo("Start || GetAPMSystemParam(Controller)");
            R_Exception loException = new R_Exception();
            GetAPMSystemParamResultDTO loRtn = new GetAPMSystemParamResultDTO();
            GetAPMSystemParamParameterDTO loParam = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetAPMSystemParam(Controller)");
                APM00100Cls loCls = new APM00100Cls();

                loParam = new GetAPMSystemParamParameterDTO()
                {
                    CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID
                };

                _logger.LogInfo("Run GetAPMSystemParam(Cls) || GetAPMSystemParam(Controller)");
                loRtn.Data = loCls.GetAPMSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAPMSystemParam(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APM00100ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APM00100ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APM00100ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APM00100ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APM00100ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                APM00100Cls loCls = new APM00100Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APM00100ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APM00100ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APM00100ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APM00100ParameterDTO>();
            APM00100Cls loCls = new APM00100Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "NEW";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");

            return loRtn;
        }
    }
}
