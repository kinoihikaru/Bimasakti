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
    public class GSM04511Controller : ControllerBase, IGSM04511
    {
        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM04511DTO> GetJournalGroupGOAByDeptList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM04511DTO> loRtn = null;

            try
            {
                var loCls = new GSM04511Cls();
                var loParam = new GSM04511ParameterDTO();

                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParam.CJRNGRP_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_TYPE);
                loParam.CJRNGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CJRNGRP_CODE);
                loParam.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CGOA_CODE);

                var loResult = loCls.GetAllJournalGroupGOAByDept(loParam);

                loRtn = GetListStream<GSM04511DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM04511DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM04511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM04511DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM04511DTO>();

            try
            {
                var loCls = new GSM04511Cls();

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
        public R_ServiceSaveResultDTO<GSM04511DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM04511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM04511DTO> loRtn = new R_ServiceSaveResultDTO<GSM04511DTO>();

            try
            {
                var loCls = new GSM04511Cls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new GSM04511Cls();

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