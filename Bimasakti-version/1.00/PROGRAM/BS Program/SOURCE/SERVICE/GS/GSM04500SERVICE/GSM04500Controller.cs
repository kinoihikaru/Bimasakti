using GSM04500BACK;
using GSM04500COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;

namespace GSM04500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04500Controller : ControllerBase, IGSM04500
    {
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM04500Record<GSM04500InitDTO> GetInitData()
        {
            R_Exception loException = new R_Exception();
            GSM04500Record<GSM04500InitDTO> loRtn = new GSM04500Record<GSM04500InitDTO>();

            try
            {
                GSM04500Cls loCls = new GSM04500Cls();

                loRtn.Data = new GSM04500InitDTO()
                {
                    JournalGroupTypeList = loCls.GetAllJournalType(),
                    PropertyList = loCls.GetAllProperty(),
                };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GSM04500DTO> GetJournalGroupList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM04500DTO> loRtn = null;

            try
            {
                var loCls = new GSM04500Cls();

                var loProperty = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                var loJournalTypeID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);

                var loResult = loCls.GetAllJournalGroup(loProperty, loJournalTypeID);

                loRtn = GetListStream<GSM04500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new GSM04500Cls();

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM04500DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM04500DTO>();

            try
            {
                var loCls = new GSM04500Cls();

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM04500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM04500DTO> loRtn = new R_ServiceSaveResultDTO<GSM04500DTO>();

            try
            {
                var loCls = new GSM04500Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GSM04500FileByteDTO GetTemplateJournalGroupExcel()
        {
            var loEx = new R_Exception();
            var loRtn = new GSM04500FileByteDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");

                var lcResourceFile = "BIMASAKTI_GS_API.Template.Journal Group.xlsx";
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
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}