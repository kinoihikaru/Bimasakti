using GSM03000Back;
using GSM03000Common;
using GSM03000Common.DTOs;
using GSM03000Common.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM03000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM03000Controller : ControllerBase, IGSM03000
    {
        private LoggerGSM03000 _Gsm03000logger;

        public GSM03000Controller(ILogger<LoggerGSM03000> logger)
        {
            //Initial and Get Logger
            LoggerGSM03000.R_InitializeLogger(logger);
            _Gsm03000logger = LoggerGSM03000.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM03000DTO> GetOtherChargesList()
        {
            var loEx = new R_Exception();
            _Gsm03000logger.LogInfo("Start GetOtherChargesList");

            IAsyncEnumerable<GSM03000DTO> loRtn = null;
            GSM03000DTO loParameter = null;

            try
            {
                var loData = R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY);

                var loCls = new GSM03000Cls();
                loParameter = new GSM03000DTO();

                _Gsm03000logger.LogInfo("Set Param GetOtherChargesList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                _Gsm03000logger.LogInfo("Call Back Method GetAllOtherCharges");
                var loResult = loCls.GetAllOtherCharges(loParameter);

                _Gsm03000logger.LogInfo("Call Stream Method For Stream Data GetOtherChargesList");
                loRtn = GetOtherChargesStream<GSM03000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End GetOtherChargesList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetOtherChargesStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM03000PropertyDTO> GetProperty()
        {
            var loEx = new R_Exception();
            _Gsm03000logger.LogInfo("Start GetProperty");

            IAsyncEnumerable<GSM03000PropertyDTO> loRtn = null;
            var loParameter = new GSM03000PropertyDTO();

            try
            {
                var loCls = new GSM03000Cls();

                _Gsm03000logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Gsm03000logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Gsm03000logger.LogInfo("Call Stream Method For Stream Data GetProperty");
                loRtn = GetOtherChargesStream<GSM03000PropertyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM03000DTO> poParameter)
        {
            var loEx = new R_Exception();
            _Gsm03000logger.LogInfo("Start R_ServiceDelete_OtherCharges");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                _Gsm03000logger.LogInfo("Set Param R_ServiceDelete_OtherCharges");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM03000Cls();

                _Gsm03000logger.LogInfo("Call Back Method R_Delete_OtherCharges");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End R_ServiceDelete_OtherCharges");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM03000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM03000DTO> poParameter) 
        {
            var loEx = new R_Exception();
            _Gsm03000logger.LogInfo("Start R_ServiceGetRecord_OtherCharges");

            R_ServiceGetRecordResultDTO<GSM03000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM03000DTO>();

            try
            {
                _Gsm03000logger.LogInfo("Set Param R_ServiceGetRecord_OtherCharges");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM03000Cls();

                _Gsm03000logger.LogInfo("Call Back Method R_GetRecord_OtherCharges");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End R_ServiceGetRecord_OtherCharges");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM03000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM03000DTO> poParameter)
        {
            var loEx = new R_Exception();
            _Gsm03000logger.LogInfo("Start R_ServiceSave_OtherCharges");

            R_ServiceSaveResultDTO<GSM03000DTO> loRtn = new R_ServiceSaveResultDTO<GSM03000DTO>();

            try
            {
                _Gsm03000logger.LogInfo("Set Param R_ServiceSave_OtherCharges");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM03000Cls();

                _Gsm03000logger.LogInfo("Call Back Method R_Save_OtherCharges");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End R_ServiceSave_OtherCharges");

            return loRtn;
        }

        [HttpPost]
        public GSM03000ActiveDTO GSM03000OtherChargesChangeStatus(GSM03000ActiveParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            _Gsm03000logger.LogInfo("Start GSM03000OtherChargesChangeStatus");

            GSM03000ActiveDTO loRtn = new GSM03000ActiveDTO();
            GSM03000Cls loCls = new GSM03000Cls();

            try
            {
                _Gsm03000logger.LogInfo("Set Param GSM03000OtherChargesChangeStatus");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Gsm03000logger.LogInfo("Call Back Method GSM03000ChangeStatusSP");
                loCls.GSM03000ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Gsm03000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Gsm03000logger.LogInfo("End GSM03000OtherChargesChangeStatus");

            return loRtn;
        }
    }
}
