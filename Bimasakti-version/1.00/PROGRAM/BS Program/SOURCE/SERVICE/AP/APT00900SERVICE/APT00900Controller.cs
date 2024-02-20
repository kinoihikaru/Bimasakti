using APT00900BACK;
using APT00900COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;

namespace APT00900SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00900Controller : ControllerBase, IAPT00900
    {
        private LoggerAPT00900 _Logger;
        private readonly ActivitySource _activitySource;
        public APT00900Controller(ILogger<LoggerAPT00900> logger)
        {
            //Initial and Get Logger
            LoggerAPT00900.R_InitializeLogger(logger);
            _Logger = LoggerAPT00900.R_GetInstanceLogger();
            _activitySource = APT00900ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00900Controller));
        }

        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<APT00900PropertyDTO> GetGSPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetGSPropertyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00900PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetGSPropertyList");

            try
            {
                _Logger.LogInfo("Call Back Method GetProperty");
                var loCls = new APT00900Cls();
                var loTempRtn = loCls.GetProperty();

                _Logger.LogInfo("Call Stream Method Data GetGSPropertyList");
                loRtn = GetStreamData<APT00900PropertyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPropertyList");

            return loRtn;
        }

        [HttpPost]
        public APT00900UploadFileDTO DownloadTemplateFile()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFile");
            var loEx = new R_Exception();
            var loRtn = new APT00900UploadFileDTO();
            _Logger.LogInfo("Start DownloadTemplateFile");

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_AP_API");

                _Logger.LogInfo("Load File Template From DownloadTemplateFile");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.AP_INVOICE_UPLOAD.xlsx";
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
    }
}