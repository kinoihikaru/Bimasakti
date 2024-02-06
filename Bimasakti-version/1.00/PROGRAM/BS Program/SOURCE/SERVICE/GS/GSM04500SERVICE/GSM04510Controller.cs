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
    public class GSM04510Controller : ControllerBase, IGSM04510
    {
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM04510DTO> GetJournalGroupGOAList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM04510DTO> loRtn = null;

            try
            {
                var loCls = new GSM04510Cls();
                var loParam = new GSM04510ParameterDTO();

                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParam.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);
                loParam.CJRNGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_CODE);

                var loResult = loCls.GetAllJournalGroupGOA(loParam);

                loRtn = GetListStream<GSM04510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM04510DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM04510DTO>();

            try
            {
                var loCls = new GSM04510Cls();

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
        public R_ServiceSaveResultDTO<GSM04510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM04510DTO> loRtn = new R_ServiceSaveResultDTO<GSM04510DTO>();

            try
            {
                var loCls = new GSM04510Cls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new GSM04510Cls();

                loCls.R_Delete(poParameter.Entity);
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