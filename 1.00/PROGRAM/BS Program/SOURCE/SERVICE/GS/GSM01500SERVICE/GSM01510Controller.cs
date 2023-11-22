using GSM01500BACK;
using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using GSM01500COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM01510Controller : ControllerBase, IGSM01510
    {
        private LoggerGSM01510 _logger;

        public GSM01510Controller(ILogger<GSM01510Controller> logger)
        {
            LoggerGSM01510.R_InitializeLogger(logger);
            _logger = LoggerGSM01510.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM01510DepartmentDTO> GetCenterDepartmentList()
        {
            _logger.LogInfo("Start || GetCenterDepartmentList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM01510DepartmentDTO> loRtn = null;
            GetCenterDeptListParameter loParam = new GetCenterDeptListParameter();
            //SelectedCenterCodeDTO loCenterContext = new SelectedCenterCodeDTO();
            GSM01510Cls loCls = new GSM01510Cls();
            List<GSM01510DepartmentDTO> loTempRtn = null;

            try
            {
                //loCenterContext = R_Utility.R_GetStreamingContext<SelectedCenterCodeDTO>(ContextConstant.CENTER_CODE_STREAM_CONTEXT);

                _logger.LogInfo("Set Parameter || GetCenterDepartmentList(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CCENTER_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CENTER_CODE_STREAM_CONTEXT); 
                loParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetCenterDepartmentList(Cls) || GetCenterDepartmentList(Controller)");
                loTempRtn = loCls.GetCenterDepartmentList(loParam);

                _logger.LogInfo("Run GetCenterDepartmentStream(Controller) || GetCenterDepartmentList(Controller)");
                loRtn = GetCenterDepartmentStream(loTempRtn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                loException.Add(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCenterDepartmentList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM01510DepartmentDTO> GetCenterDepartmentStream(List<GSM01510DepartmentDTO> poParameter)
        {
            foreach (GSM01510DepartmentDTO item in poParameter)
            {
                yield return item;
            }
        }
        
    }
}
