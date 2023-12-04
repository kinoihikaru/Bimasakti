using GSM09200BACK;
using GSM09200COMMON;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
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
    public class GSM09210Controller : ControllerBase, IGSM09210
    {
        private LoggerGSM09210 _logger;
        public GSM09210Controller(ILogger<GSM09210Controller> logger)
        {
            LoggerGSM09210.R_InitializeLogger(logger);
            _logger = LoggerGSM09210.R_GetInstanceLogger();
        }

        [HttpPost]
        public GetExpenditureCategoryResultDTO GetExpenditureCategory()
        {
            _logger.LogInfo("Start || GetExpenditureCategory(Controller)");
            R_Exception loException = new R_Exception();
            GetExpenditureCategoryResultDTO loRtn = new GetExpenditureCategoryResultDTO();
            GetExpenditureCategoryParameterDTO loParam = new GetExpenditureCategoryParameterDTO();
            GetExpenditureCategoryDTO loTempRtn = new GetExpenditureCategoryDTO();


            try
            {
                _logger.LogInfo("Set Parameter || GetExpenditureCategory(Controller)");
                GSM09210Cls loCls = new GSM09210Cls();

                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09210_PROPERTY_ID_CONTEXT);
                loParam.CEXPENDITURE_CATEGORY_ID = R_Utility.R_GetContext<string>(ContextConstant.GSM09210_EXPENDITURE_CATEGORY_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetExpenditureCategory(Cls) || GetExpenditureCategory(Controller)");
                loTempRtn = loCls.GetExpenditureCategory(loParam);

                loRtn.Data = loTempRtn;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetExpenditureCategory(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09210DTO> GetExpenditureList()
        {
            _logger.LogInfo("Start || GetExpenditureList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM09210DTO> loRtn = null;
            GSM09210ParameterDTO loParam = new GSM09210ParameterDTO();
            GSM09210Cls loCls = new GSM09210Cls();
            List<GSM09210DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetExpenditureList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09210_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CEXPENDITURE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM09210_FROM_EXPENDITURE_CATEGORY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetExpenditureList(Cls) || GetExpenditureList(Controller)");
                loTempRtn = loCls.GetExpenditureList(loParam);

                _logger.LogInfo("Run GetExpenditureStream(Controller) || GetExpenditureList(Controller)");
                loRtn = GetExpenditureStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetExpenditureList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM09210DTO> GetExpenditureStream(List<GSM09210DTO> poParameter)
        {
            foreach (GSM09210DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public MoveExpenditureCategoryResultDTO MoveExpenditureCategory(MoveExpenditureCategoryParameterDTO poParam)
        {
            _logger.LogInfo("Start || MoveExpenditureCategory(Controller)");
            R_Exception loException = new R_Exception();
            MoveExpenditureCategoryResultDTO loRtn = new MoveExpenditureCategoryResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || MoveExpenditureCategory(Controller)");
                GSM09210Cls loCls = new GSM09210Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run MoveExpenditureCategory(Cls) || MoveExpenditureCategory(Controller)");
                loCls.MoveExpenditureCategory(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || MoveExpenditureCategory(Controller)");
            return loRtn;
        }
    }
}
