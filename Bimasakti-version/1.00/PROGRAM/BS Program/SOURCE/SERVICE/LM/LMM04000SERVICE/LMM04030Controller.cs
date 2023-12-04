using LMM04000BACK;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04030;
using LMM04000COMMON.Loggers;
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

namespace LMM04000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM04030Controller : ControllerBase, ILMM04030
    {
        private LoggerLMM04030 _logger;
        public LMM04030Controller(ILogger<LMM04030Controller> logger)
        {
            LoggerLMM04030.R_InitializeLogger(logger);
            _logger = LoggerLMM04030.R_GetInstanceLogger();
        }
        [HttpPost]
        public IAsyncEnumerable<GetBankCodeDTO> GetBankCodeList()
        {
            _logger.LogInfo("Start || GetBankCodeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetBankCodeDTO> loRtn = null;
            GetBankCodeParameterDTO loParam = new GetBankCodeParameterDTO();
            LMM04030Cls loCls = new LMM04030Cls();
            List<GetBankCodeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetBankCodeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run GetBankCodeList(Cls) || GetBankCodeList(Controller)");
                loTempRtn = loCls.GetBankCodeList(loParam);

                _logger.LogInfo("Run GetBankCodeStream(Controller) || GetBankCodeList(Controller)");
                loRtn = GetBankCodeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBankCodeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetBankCodeDTO> GetBankCodeStream(List<GetBankCodeDTO> poParameter)
        {
            foreach (GetBankCodeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM04030DTO> GetBankInfoList()
        {
            _logger.LogInfo("Start || GetBankInfoList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM04030DTO> loRtn = null;
            GetBankInfoParameterDTO loParam = new GetBankInfoParameterDTO();
            LMM04030Cls loCls = new LMM04030Cls();
            List<LMM04030DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetBankInfoList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04030_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04030_TENANT_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetBankInfoList(Cls) || GetBankInfoList(Controller)");
                loTempRtn = loCls.GetBankInfoList(loParam);

                _logger.LogInfo("Run GetBankInfoStream(Controller) || GetBankInfoList(Controller)");
                loRtn = GetBankInfoStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBankInfoList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<LMM04030DTO> GetBankInfoStream(List<LMM04030DTO> poParameter)
        {
            foreach (LMM04030DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TenantResultDTO GetTenant(TenantParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetTenant(Controller)");
            R_Exception loException = new R_Exception();
            TenantResultDTO loRtn = new TenantResultDTO();
            LMM04030Cls loCls = new LMM04030Cls();
            TenantDTO loTempRtn = null;

            try
            {
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_TENANT_ID_CONTEXT);

                _logger.LogInfo("Set Parameter || GetTenant(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenant(Cls) || GetTenant(Controller)");
                loRtn.Data = loCls.GetTenant(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenant(Controller)");
            return loRtn;
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM04030ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            LMM04030Cls loCls = new LMM04030Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_TENANT_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM04030ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM04030ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM04030ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<LMM04030ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                LMM04030Cls loCls = new LMM04030Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_TENANT_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceSaveResultDTO<LMM04030ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM04030ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM04030ParameterDTO> loRtn = new R_ServiceSaveResultDTO<LMM04030ParameterDTO>();
            LMM04030Cls loCls = new LMM04030Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04030_TENANT_ID_CONTEXT);
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
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
