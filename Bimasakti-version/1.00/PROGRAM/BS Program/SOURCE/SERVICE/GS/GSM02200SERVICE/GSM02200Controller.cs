using GSM02200BACK;
using GSM02200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;

namespace GSM02200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02200Controller : ControllerBase, IGSM02200
    {
        private LoggerGSM02200 _Logger;
        private readonly ActivitySource _activitySource;

        public GSM02200Controller(ILogger<LoggerGSM02200> logger)
        {
            //Initial and Get Logger
            LoggerGSM02200.R_InitializeLogger(logger);
            _Logger = LoggerGSM02200.R_GetInstanceLogger();
            _activitySource = GSM02200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02200Controller));
        }

        [HttpPost]
        public GSM02200UploadFileDTO DownloadTemplateFile()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFile");
            var loEx = new R_Exception();
            var loRtn = new GSM02200UploadFileDTO();
            _Logger.LogInfo("Start DownloadTemplateFile");

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");

                _Logger.LogInfo("Load File Template From DownloadTemplateFile");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Geography.xlsx";
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End DownloadTemplateFile");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02200DTO> GetGeographyList()
        {
            using Activity activity = _activitySource.StartActivity("GetGeographyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM02200DTO> loRtn = null;
            GSM02200DTO loParameter = null;
            _Logger.LogInfo("Start GetGeographyList");

            try
            {
                var loCls = new GSM02200Cls();
                loParameter = new GSM02200DTO();

                _Logger.LogInfo("Set Param GetGeographyList");
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllGeography");
                var loResult = loCls.GetAllGeography(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetGeographyList");
                loRtn = GetListStream<GSM02200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGeographyList");

            return loRtn;
        }
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM02200SingleResult<GSM02200DTO> GSM02200GeographyChangeStatus(GSM02200DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GSM02200GeographyChangeStatus");
            R_Exception loException = new R_Exception();
            GSM02200SingleResult<GSM02200DTO> loRtn = new GSM02200SingleResult<GSM02200DTO>();
            GSM02200Cls loCls = new GSM02200Cls();
            _Logger.LogInfo("Start GSM02200GeographyChangeStatus");

            try
            {
                _Logger.LogInfo("Set Param GSM02200GeographyChangeStatus");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GSM02200ChangeStatusSP");
                loCls.GSM02200ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GSM02200GeographyChangeStatus");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02200DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02200DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02200DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02200DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02200DTO>();
            _Logger.LogInfo("Start ServiceGetRecord GSM02200");

            try
            {
                // Set Param
                _Logger.LogInfo("Set Param Entity ServiceGetRecord GSM02200");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM02200Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord GSM02200Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord GSM02200");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM02200DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02200DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM02200DTO> loRtn = new R_ServiceSaveResultDTO<GSM02200DTO>();
            _Logger.LogInfo("Start ServiceSave GSM02200");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave GSM02200");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GSM02200Cls();

                _Logger.LogInfo("Call Back Method R_Save GSM02200Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave GSM02200");

            return loRtn;
        }
    }
}