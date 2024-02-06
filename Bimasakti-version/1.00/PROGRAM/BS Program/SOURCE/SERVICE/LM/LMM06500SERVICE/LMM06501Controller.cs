using LMM06500BACK;
using LMM06500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06501Controller : ControllerBase, ILMM06501
    {
        private LoggerLMM06501 _Logger;
        private readonly ActivitySource _activitySource;

        public LMM06501Controller(ILogger<LoggerLMM06501> logger)
        {
            //Initial and Get Logger
            LoggerLMM06501.R_InitializeLogger(logger);
            _Logger = LoggerLMM06501.R_GetInstanceLogger();
            _activitySource = LMM06501ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM06501Controller));
        }

        [HttpPost]
        public LMM06500UploadFileDTO DownloadTemplateFile()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFile");
            var loEx = new R_Exception();
            var loRtn = new LMM06500UploadFileDTO();
            _Logger.LogInfo("Start DownloadTemplateFile");

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_LM_API");

                _Logger.LogInfo("Load File Template From DownloadTemplateFile");
                var lcResourceFile = "BIMASAKTI_LM_API.Template.Staff.xlsx";
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
        public IAsyncEnumerable<LMM06501ErrorValidateDTO> GetErrorProcess()
        {
            using Activity activity = _activitySource.StartActivity("GetErrorProcess");
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06501ErrorValidateDTO> loRtn = null;

            try
            {
                var lcKeyGuid = R_Utility.R_GetStreamingContext<string>("UploadStaffKeyGuid");

                var loCls = new LMM06500ValidateStaffCls();

                var loResult = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

                loRtn = GetStream<LMM06501ErrorValidateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
    }
}
