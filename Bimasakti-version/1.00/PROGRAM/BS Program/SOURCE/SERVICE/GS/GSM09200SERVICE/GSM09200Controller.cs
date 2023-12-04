using GSM09200BACK;
using GSM09200COMMON;
using GSM09200COMMON.DTOs.GSM09200;
using GSM09200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GSM09200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09200Controller : ControllerBase, IGSM09200
    {
        private LoggerGSM09200 _logger;
        public GSM09200Controller(ILogger<GSM09200Controller> logger)
        {
            LoggerGSM09200.R_InitializeLogger(logger);
            _logger = LoggerGSM09200.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09200DTO> GetExpenditureCategoryList()
        {
            _logger.LogInfo("Start || GetExpenditureCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09200DTO> loRtn = null;
            GetExpenditureCategoryListParameterDTO loParam = new GetExpenditureCategoryListParameterDTO();
            GSM09200Cls loCls = new GSM09200Cls();
            List<GSM09200DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetExpenditureCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09200_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetExpenditureCategoryList(Cls) || GetExpenditureCategoryList(Controller)");
                loTempRtn = loCls.GetExpenditureCategoryList(loParam);

                _logger.LogInfo("Run GetExpenditureCategoryStream(Controller) || GetExpenditureCategoryList(Controller)");
                loRtn = GetExpenditureCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("Emd || GetExpenditureCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM09200DTO> GetExpenditureCategoryStream(List<GSM09200DTO> poParameter)
        {
            foreach (GSM09200DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            GetPropertyListParameterDTO loParam = new GetPropertyListParameterDTO();
            GSM09200Cls loCls = new GSM09200Cls();
            List<GetPropertyListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParam);

                _logger.LogInfo("Run GetPropertyListStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyListDTO> GetPropertyListStream(List<GetPropertyListDTO> poParameter)
        {
            foreach (GetPropertyListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM09200DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM09200ParameterDTO loParam = new GSM09200ParameterDTO();
            GSM09200Cls loCls = new GSM09200Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
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
        public R_ServiceGetRecordResultDTO<GSM09200DetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM09200DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM09200DetailDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM09200DetailDTO>();
            GSM09200ParameterDTO loParam = new GSM09200ParameterDTO();
            R_ServiceGetRecordResultDTO<GSM09200ParameterDTO> loTempRtn = new R_ServiceGetRecordResultDTO<GSM09200ParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM09200Cls loCls = new GSM09200Cls();

                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loTempRtn.data = loCls.R_GetRecord(loParam);

                loRtn.data = loTempRtn.data.Data;
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
        public R_ServiceSaveResultDTO<GSM09200DetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM09200DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM09200DetailDTO> loRtn = new R_ServiceSaveResultDTO<GSM09200DetailDTO>();
            R_ServiceSaveResultDTO<GSM09200ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM09200ParameterDTO>();
            GSM09200Cls loCls = new GSM09200Cls();
            GSM09200ParameterDTO loParam = new GSM09200ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    loParam.CACTION = "NEW";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    loParam.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loTempRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
                loRtn.data = loTempRtn.data.Data;
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

        [HttpPost]
        public ValidateIsCategoryExistResultDTO ValidateIsCategoryExist(ValidateIsCategoryExistParameterDTO poParam)
        {
            _logger.LogInfo("Start || ValidateIsCategoryExist(Controller)");
            R_Exception loException = new R_Exception();
            ValidateIsCategoryExistResultDTO loRtn = new ValidateIsCategoryExistResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || ValidateIsCategoryExist(Controller)");
                GSM09200Cls loCls = new GSM09200Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run ValidateIsCategoryExist(Cls) || ValidateIsCategoryExist(Controller)");
                loCls.ValidateIsCategoryExist(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ValidateIsCategoryExist(Controller)");
            return loRtn;
        }
    }
}
