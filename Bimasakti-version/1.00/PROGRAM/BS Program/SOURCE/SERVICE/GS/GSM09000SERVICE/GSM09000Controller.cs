using GSM09000BACK;
using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09000;
using GSM09000COMMON.Loggers;
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

namespace GSM09000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09000Controller : ControllerBase, IGSM09000
    {
        private LoggerGSM09000 _logger;

        public GSM09000Controller(ILogger<GSM09000Controller> logger)
        {
            LoggerGSM09000.R_InitializeLogger(logger);
            _logger = LoggerGSM09000.R_GetInstanceLogger();
        }
        [HttpPost]
        public IAsyncEnumerable<GSM09000DTO> GetTenantCategoryList()
        {
            _logger.LogInfo("Start || GetTenantCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09000DTO> loRtn = null;
            GetTenantCategoryListParameterDTO loParam = new GetTenantCategoryListParameterDTO();
            GSM09000Cls loCls = new GSM09000Cls();
            List<GSM09000DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09000_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenantCategoryList(Cls) || GetTenantCategoryList(Controller)");
                loTempRtn = loCls.GetTenantCategoryList(loParam);

                _logger.LogInfo("Run GetTenantCategoryStream(Controller) || GetTenantCategoryList(Controller)");
                loRtn = GetTenantCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM09000DTO> GetTenantCategoryStream(List<GSM09000DTO> poParameter)
        {
            foreach (GSM09000DTO item in poParameter)
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
            GSM09000Cls loCls = new GSM09000Cls();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM09000DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM09000ParameterDTO loParam = new GSM09000ParameterDTO();
            GSM09000Cls loCls = new GSM09000Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT);
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
        public R_ServiceGetRecordResultDTO<GSM09000DetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM09000DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM09000DetailDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM09000DetailDTO>();
            GSM09000ParameterDTO loParam = new GSM09000ParameterDTO();
            R_ServiceGetRecordResultDTO<GSM09000ParameterDTO> loTempRtn = new R_ServiceGetRecordResultDTO<GSM09000ParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM09000Cls loCls = new GSM09000Cls();

                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT);
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
        public R_ServiceSaveResultDTO<GSM09000DetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM09000DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM09000DetailDTO> loRtn = new R_ServiceSaveResultDTO<GSM09000DetailDTO>();
            R_ServiceSaveResultDTO<GSM09000ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM09000ParameterDTO>();
            GSM09000Cls loCls = new GSM09000Cls();
            GSM09000ParameterDTO loParam = new GSM09000ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    loParam.CACTION = "ADD";
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
        public GetParentPositionIfEmptyResultDTO GetParentPositionIfEmpty(GetParentPositionIfEmptyParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetParentPositionIfEmpty(Controller)");
            R_Exception loException = new R_Exception();
            GetParentPositionIfEmptyResultDTO loRtn = new GetParentPositionIfEmptyResultDTO();
            //GetParentPositionIfEmptyParameterDTO loParam = new GetParentPositionIfEmptyParameterDTO();
            GetParentPositionIfEmptyDTO loTempRtn = null;


            try
            {
                _logger.LogInfo("Set Parameter || GetParentPositionIfEmpty(Controller)");
                GSM09000Cls loCls = new GSM09000Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT);

                _logger.LogInfo("Run GetParentPositionIfEmpty(Cls) || GetParentPositionIfEmpty(Controller)");
                loTempRtn = loCls.GetParentPositionIfEmpty(poParam);

                loRtn.Data = loTempRtn;
            }
            catch (Exception ex)
            { 
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetParentPositionIfEmpty(Controller)");
            return loRtn;
        }

        [HttpPost]
        public InsertNewRootIfListEmptyResultDTO InsertNewRootIfListEmpty(InsertNewRootIfListEmptyParameterDTO poParam)
        {
            _logger.LogInfo("Start || InsertNewRootIfListEmpty(Controller)");
            R_Exception loException = new R_Exception();
            InsertNewRootIfListEmptyResultDTO loRtn = new InsertNewRootIfListEmptyResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || InsertNewRootIfListEmpty(Controller)");
                GSM09000Cls loCls = new GSM09000Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09000_PROPERTY_ID_CONTEXT);

                _logger.LogInfo("Run InsertNewRootIfListEmpty(Cls) || InsertNewRootIfListEmpty(Controller)");
                loCls.InsertNewRootIfListEmpty(poParam);
            }
            catch (Exception ex)
            { 
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || InsertNewRootIfListEmpty(Controller)");
            return loRtn;
        }
    }
}
