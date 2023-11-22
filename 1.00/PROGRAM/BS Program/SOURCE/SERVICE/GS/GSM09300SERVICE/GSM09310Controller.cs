using GSM09300BACK;
using GSM09300COMMON;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09310Controller : ControllerBase, IGSM09310
    {
        private LoggerGSM09310 _logger;
        public GSM09310Controller(ILogger<GSM09310Controller> logger)
        {
            LoggerGSM09310.R_InitializeLogger(logger);
            _logger = LoggerGSM09310.R_GetInstanceLogger();
        }

        [HttpPost]
        public GetSupplierCategoryResultDTO GetSupplierCategory()
        {
            _logger.LogInfo("Start || GetSupplierCategory(Controller)");
            R_Exception loException = new R_Exception();
            GetSupplierCategoryResultDTO loRtn = new GetSupplierCategoryResultDTO();
            GetSupplierCategoryParameterDTO loParam = new GetSupplierCategoryParameterDTO();
            GetSupplierCategoryDTO loTempRtn = new GetSupplierCategoryDTO();


            try
            {
                _logger.LogInfo("Set Parameter || GetSupplierCategory(Controller)");
                GSM09310Cls loCls = new GSM09310Cls();

                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09310_PROPERTY_ID_CONTEXT);
                loParam.CSUPPLIER_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09310_SUPPLIER_CATEGORY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetSupplierCategory(Cls) || GetSupplierCategory(Controller)");
                loTempRtn = loCls.GetSupplierCategory(loParam);

                loRtn.Data = loTempRtn;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || GetSupplierCategory(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09310DTO> GetSupplierList()
        {
            _logger.LogInfo("Start || GetSupplierList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09310DTO> loRtn = null;
            GSM09310ParameterDTO loParam = new GSM09310ParameterDTO();
            GSM09310Cls loCls = new GSM09310Cls();
            List<GSM09310DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetSupplierList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09310_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSUPPLIER_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09310_FROM_SUPPLIER_CATEGORY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetSupplierList(Cls) || GetSupplierList(Controller)");
                loTempRtn = loCls.GetSupplierList(loParam);

                _logger.LogInfo("Run GetSupplierStream(Controller) || GetSupplierList(Controller)");
                loRtn = GetSupplierStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || GetSupplierList(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        private async IAsyncEnumerable<GSM09310DTO> GetSupplierStream(List<GSM09310DTO> poParameter)
        {
            foreach (GSM09310DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public MoveSupplierCategoryResultDTO MoveSupplierCategory(MoveSupplierCategoryParameterDTO poParam)
        {
            _logger.LogInfo("Start || MoveSupplierCategory(Controller)");
            R_Exception loException = new R_Exception();
            MoveSupplierCategoryResultDTO loRtn = new MoveSupplierCategoryResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || MoveSupplierCategory(Controller)");
                GSM09310Cls loCls = new GSM09310Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run MoveSupplierCategory(Cls) || MoveSupplierCategory(Controller)");
                loCls.MoveSupplierCategory(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            _logger.LogInfo("End || MoveSupplierCategory(Controller)");
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
