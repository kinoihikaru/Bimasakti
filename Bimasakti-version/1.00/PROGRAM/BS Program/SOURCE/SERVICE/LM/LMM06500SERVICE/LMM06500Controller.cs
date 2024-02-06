using LMM06500BACK;
using LMM06500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06500Controller : ControllerBase, ILMM06500
    {
        private LoggerLMM06500 _Logger;
        private readonly ActivitySource _activitySource;
        public LMM06500Controller(ILogger<LoggerLMM06500> logger)
        {
            //Initial and Get Logger
            LoggerLMM06500.R_InitializeLogger(logger);
            _Logger = LoggerLMM06500.R_GetInstanceLogger();
            _activitySource = LMM06500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM06500Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06500DTO> GetStaffList()
        {
            using Activity activity = _activitySource.StartActivity("GetStaffList");
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500DTO> loRtn = null;
            var loParameter = new LMM06500DTO();
            _Logger.LogInfo("Start GetStaffList");

            try
            {
                var loCls = new LMM06500Cls();

                _Logger.LogInfo("Set Param GetStaffList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllStaff");
                var loResult = loCls.GetAllStaff(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetStaffList");
                loRtn = GetStream<LMM06500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetStaffList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }

        [HttpPost]
        public LMM06500DTO LMM06500ActiveInactive(LMM06500DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("LMM06500ActiveInactive");
            R_Exception loException = new R_Exception();
            LMM06500DTO loRtn = new LMM06500DTO();
            LMM06500Cls loCls = new LMM06500Cls();
            _Logger.LogInfo("Start LMM06500ActiveInactive");

            try
            {
                _Logger.LogInfo("Set Param LMM06500ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method LMM06500ActiveInactiveSP");
                loCls.LMM06500ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LMM06500ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete LMM06500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete LMM06500");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new LMM06500Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM06500Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM06500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM06500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM06500DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM06500DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM06500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM06500");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM06500Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM06500Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM06500");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM06500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM06500DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM06500DTO> loRtn = new R_ServiceSaveResultDTO<LMM06500DTO>();
            _Logger.LogInfo("Start ServiceSave LMM06500");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM06500");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM06500Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM06500Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM06500");

            return loRtn;
        }
    }
}
