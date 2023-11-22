using GSM09100BACK;
using GSM09100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM09100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM09100Controller : ControllerBase, IGSM09100
    {
        private LoggerGSM09100 _Logger;

        public GSM09100Controller(ILogger<LoggerGSM09100> logger)
        {
            //Initial and Get Logger
            LoggerGSM09100.R_InitializeLogger(logger);
            _Logger = LoggerGSM09100.R_GetInstanceLogger();
        }

        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM09100SingleResult<GSM09100InitialDTO> GetInitialProductCategory(GSM09100InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GSM09100SingleResult<GSM09100InitialDTO> loRtn = new GSM09100SingleResult<GSM09100InitialDTO>();
            _Logger.LogInfo("Start GetInitialProductCategory");

            try
            {
                var loCls = new GSM09100Cls();

                _Logger.LogInfo("Set Param GetInitialProductCategory");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetInitial");
                loRtn.Data = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialProductCategory");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09100DTO> GetProductCategoryList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM09100DTO> loRtn = null;
            GSM09100DTO loParameter = null;
            _Logger.LogInfo("Start GetProductCategoryList");

            try
            {
                var loCls = new GSM09100Cls();
                loParameter = new GSM09100DTO();

                _Logger.LogInfo("Set Param GetProductCategoryList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetProductCategoryList");
                var loResult = loCls.GetProductCategoryList(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProductCategoryList");
                loRtn = GetStreamData<GSM09100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProductCategoryList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM09100PropertyDTO> GetProperty()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM09100PropertyDTO> loRtn = null;
            GSM09100PropertyDTO loParameter = null;
            _Logger.LogInfo("Start GetProperty");

            try
            {
                var loCls = new GSM09100Cls();
                loParameter = new GSM09100PropertyDTO();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProperty");
                loRtn = GetStreamData<GSM09100PropertyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM09100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete GSM09100");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord GSM09100");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM09100Cls();

                _Logger.LogInfo("Call Back Method R_Delete GSM09100Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete GSM09100");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM09100DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM09100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM09100DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM09100DTO>();
            _Logger.LogInfo("Start ServiceGetRecord GSM09100");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord GSM09100");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM09100Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord GSM09100Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord GSM09100");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM09100DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM09100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM09100DTO> loRtn = new R_ServiceSaveResultDTO<GSM09100DTO>();
            _Logger.LogInfo("Start ServiceSave GSM09100");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave GSM09100");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM09100Cls();

                _Logger.LogInfo("Call Back Method R_Save GSM09100Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave GSM09100");

            return loRtn;
        }
    }

}