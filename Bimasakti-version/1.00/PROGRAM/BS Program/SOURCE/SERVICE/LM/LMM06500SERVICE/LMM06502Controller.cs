using LMM06500BACK;
using LMM06500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06502Controller : ControllerBase, ILMM06502
    {
        private LoggerLMM06502 _Logger;
        public LMM06502Controller(ILogger<LoggerLMM06502> logger)
        {
            //Initial and Get Logger
            LoggerLMM06502.R_InitializeLogger(logger);
            _Logger = LoggerLMM06502.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06502DetailDTO> GetStaffMoveList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06502DetailDTO> loRtn = null;
            _Logger.LogInfo("Start GetStaffMoveList");

            try
            {
                var loCls = new LMM06502Cls();
                var poEntity = new LMM06502DetailDTO();

                _Logger.LogInfo("Set Param GetStaffMoveList");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poEntity.COLD_SUPERVISOR_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.COLD_SUPERVISOR_ID);
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllStaffMove");
                var loResult = loCls.GetAllStaffMove(poEntity);

                _Logger.LogInfo("Call Stream Method Data GetStaffMoveList");
                loRtn = GetStream<LMM06502DetailDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetStaffMoveList");

            return loRtn;
        }

        [HttpPost]
        public LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM06502DTO loRtn = new LMM06502DTO();
            LMM06502Cls loCls = new LMM06502Cls();

            try
            {
                loCls.SaveNewStaffMove(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

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
