using GSM09300BACK;
using GSM09300COMMON;
using GSM09300COMMON.DTOs.GSM09300;
using GSM09300COMMON.Loggers;
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

namespace GSM09300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09300Controller : ControllerBase, IGSM09300
    {
        private LoggerGSM09300 _logger;
        public GSM09300Controller(ILogger<GSM09300Controller> logger)
        {
            LoggerGSM09300.R_InitializeLogger(logger);
            _logger = LoggerGSM09300.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09300DTO> GetSupplierCategoryList()
        {
            _logger.LogInfo("Start || GetSupplierCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09300DTO> loRtn = null;
            GetSupplierCategoryListParameterDTO loParam = new GetSupplierCategoryListParameterDTO();
            GSM09300Cls loCls = new GSM09300Cls();
            List<GSM09300DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetSupplierCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09300_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetSupplierCategoryList(Cls) || GetSupplierCategoryList(Controller)");
                loTempRtn = loCls.GetSupplierCategoryList(loParam);

                _logger.LogInfo("Run GetSupplierCategoryStream(Controller) || GetSupplierCategoryList(Controller)");
                loRtn = GetSupplierCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || GetSupplierCategoryList(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GSM09300DTO> GetSupplierCategoryStream(List<GSM09300DTO> poParameter)
        {
            foreach (GSM09300DTO item in poParameter)
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
            GSM09300Cls loCls = new GSM09300Cls();
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

            _logger.LogInfo("End || GetPropertyList(Controller)");
            loException.ThrowExceptionIfErrors();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM09300DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM09300ParameterDTO loParam = new GSM09300ParameterDTO();
            GSM09300Cls loCls = new GSM09300Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT);
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

            _logger.LogInfo("End || R_ServiceDelete(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM09300DetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM09300DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM09300DetailDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM09300DetailDTO>();
            GSM09300ParameterDTO loParam = new GSM09300ParameterDTO();
            R_ServiceGetRecordResultDTO<GSM09300ParameterDTO> loTempRtn = new R_ServiceGetRecordResultDTO<GSM09300ParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM09300Cls loCls = new GSM09300Cls();

                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT);
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

            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM09300DetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM09300DetailDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM09300DetailDTO> loRtn = new R_ServiceSaveResultDTO<GSM09300DetailDTO>();
            R_ServiceSaveResultDTO<GSM09300ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM09300ParameterDTO>();
            GSM09300Cls loCls = new GSM09300Cls();
            GSM09300ParameterDTO loParam = new GSM09300ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT);
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

            _logger.LogInfo("End || R_ServiceSave(Controller)");
            loException.ThrowExceptionIfErrors();

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
                GSM09300Cls loCls = new GSM09300Cls();

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

            _logger.LogInfo("End || ValidateIsCategoryExist(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
