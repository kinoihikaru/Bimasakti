using GSM09100BACK;
using GSM09100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GSM09100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09110Controller : ControllerBase, IGSM09110
    {
        private LoggerGSM09110 _Logger;
        private readonly ActivitySource _activitySource;
        public GSM09110Controller(ILogger<LoggerGSM09110> logger)
        {
            //Initial and Get Logger
            LoggerGSM09110.R_InitializeLogger(logger);
            _Logger = LoggerGSM09110.R_GetInstanceLogger();
            _activitySource = GSM09110ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM09110Controller));
        }

        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09110DTO> GetProductList()
        {
            using Activity activity = _activitySource.StartActivity("GetProductList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM09110DTO> loRtn = null;
            GSM09110DTO loParameter = null;
            _Logger.LogInfo("Start GetProductList");

            try
            {
                var loCls = new GSM09110Cls();
                loParameter = new GSM09110DTO();

                _Logger.LogInfo("Set Param GetProductList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCATEGORY_ID);

                _Logger.LogInfo("Call Back Method GetProductList");
                var loResult = loCls.GetProductList(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProductList");
                loRtn = GetStreamData<GSM09110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProductList");

            return loRtn;
        }

        [HttpPost]
        public GSM09100SingleResult<GSM09120DTO> MoveProduct(GSM09120DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("MoveProduct");
            R_Exception loException = new R_Exception();
            GSM09100SingleResult<GSM09120DTO> loRtn = new GSM09100SingleResult<GSM09120DTO>();
            _Logger.LogInfo("Start MoveProduct");

            try
            {
                GSM09110Cls loCls = new GSM09110Cls();

                _Logger.LogInfo("Set Param MoveProduct");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method MoveProduct");
                loCls.MoveProduct(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End MoveProduct");

            return loRtn;
        }
    }

}