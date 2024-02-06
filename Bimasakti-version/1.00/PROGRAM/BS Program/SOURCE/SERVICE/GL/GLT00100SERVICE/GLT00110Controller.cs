using GLT00100BACK;
using GLT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GLT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00110Controller : ControllerBase, IGLT00110
    {
        public GLT00110Controller(ILogger<LoggerGLT00100Universal> logger)
        {
        }

        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion


        [HttpPost]
        public GLT00100RecordResult<GLT00110LastCurrencyRateDTO> GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110LastCurrencyRateDTO> loRtn = new GLT00100RecordResult<GLT00110LastCurrencyRateDTO>();

            try
            {
                var loCls = new GLT00110Cls();

                loRtn.Data = loCls.GetLastCurrency(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00110DTO> GetJournalRecord(GLT00110DTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110DTO> loRtn = new GLT00100RecordResult<GLT00110DTO>();

            try
            {
                var loCls = new GLT00110Cls();

                loRtn.Data = loCls.GetJournalDisplay(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00110DTO> SaveJournal(GLT00110HeaderDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110DTO> loRtn = new GLT00100RecordResult<GLT00110DTO>();

            try
            {
                var loCls = new GLT00110Cls();

                //Save
                var loResult = loCls.SaveJournal(poEntity);
                //Get
                loRtn.Data = loCls.GetJournalDisplay(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}