using GSM09000BACK;
using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09001;
using GSM09000COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09001Controller : ControllerBase, IGSM09001
    {
        private LoggerGSM09001 _logger;

        public GSM09001Controller(ILogger<GSM09001Controller> logger)
        {
            LoggerGSM09001.R_InitializeLogger(logger);
            _logger = LoggerGSM09001.R_GetInstanceLogger();
        }

        [HttpPost]
        public GetTenantCategoryResultDTO GetTenantCategory()
        {
            _logger.LogInfo("Start || GetTenantCategory(Controller)");
            R_Exception loException = new R_Exception();
            GetTenantCategoryResultDTO loRtn = new GetTenantCategoryResultDTO();
            GetTenantCategoryParameterDTO loParam = new GetTenantCategoryParameterDTO();
            GetTenantCategoryDTO loTempRtn = new GetTenantCategoryDTO();


            try
            {
                _logger.LogInfo("Set Parameter || GetTenantCategory(Controller)");
                GSM09001Cls loCls = new GSM09001Cls();

                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_PROPERTY_ID_CONTEXT);
                loParam.CTENANT_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_TENANT_CATEGORY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenantCategory(Cls) || GetTenantCategory(Controller)");
                loTempRtn = loCls.GetTenantCategory(loParam);

                loRtn.Data = loTempRtn;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantCategory(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList()
        {
            _logger.LogInfo("Start || GetTenantCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetTenantCategoryDTO> loRtn = null;
            GetTenantCategoryParameterDTO loParam = new GetTenantCategoryParameterDTO();
            GSM09001Cls loCls = new GSM09001Cls();
            List<GetTenantCategoryDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09001_PROPERTY_ID_STREAMING_CONTEXT);
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
        private async IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryStream(List<GetTenantCategoryDTO> poParameter)
        {
            foreach (GetTenantCategoryDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09001DTO> GetTenantList()
        {
            _logger.LogInfo("Start || GetTenantList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09001DTO> loRtn = null;
            GSM09001ParameterDTO loParam = new GSM09001ParameterDTO();
            GSM09001Cls loCls = new GSM09001Cls();
            List<GSM09001DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09001_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CTENANT_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09001_FROM_TENANT_CATEGORY_ID_STREAMING_CONTEXT);
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
        private async IAsyncEnumerable<GSM09001DTO> GetTenantStream(List<GSM09001DTO> poParameter)
        {
            foreach (GSM09001DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public SaveMoveTenantCategoryResultDTO SaveMoveTenantCategory(SaveMoveTenantCategoryParameterDTO poParam)
        {
            _logger.LogInfo("Start || SaveMoveTenantCategory(Controller)");
            R_Exception loException = new R_Exception();
            SaveMoveTenantCategoryResultDTO loRtn = new SaveMoveTenantCategoryResultDTO();
            //SaveMoveTenantCategoryParameterDTO loParam = new SaveMoveTenantCategoryParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || SaveMoveTenantCategory(Controller)");
                GSM09001Cls loCls = new GSM09001Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                //poParam.CTENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_TENANT_ID_CONTEXT);
                //poParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_PROPERTY_ID_CONTEXT);
                //poParam.CFROM_TENANT_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_FROM_TENANT_CATEGORY_ID_CONTEXT);
                //poParam.CTO_TENANT_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09001_TO_TENANT_CATEGORY_ID_CONTEXT);

                /*
                                SaveTenantContextParameterDTO poParamContext = R_Utility.R_GetContext<SaveTenantContextParameterDTO>(ContextConstant.GSM09001_TENANT_ID_CONTEXT);*/

                _logger.LogInfo("Run SaveMoveTenantCategory(Cls) || SaveMoveTenantCategory(Controller)");
                loCls.SaveMoveTenantCategory(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || SaveMoveTenantCategory(Controller)");
            return loRtn;
        }
    }
}
