using GSM05000BACK_FMC;
using GSM05000COMMON_FMC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000SERVICE_FMC;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000UniversalController : ControllerBase, IGSM05000Universal
{
    private LoggerGSM05000Universal _logger;
    
    public GSM05000UniversalController(ILogger<LoggerGSM05000Universal> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000Universal.R_InitializeLogger(logger);
        _logger = LoggerGSM05000Universal.R_GetInstanceLogger();
    }

    [HttpPost]
    public GSM05000ListResult<GSM05000UniversalDTO> GetRefNoDelimiterList()
    {
        _logger.LogInfo("Start - Get Delimiter List");
        R_Exception loEx = new();
        GSM05000ListResult<GSM05000UniversalDTO> loRtn = new GSM05000ListResult<GSM05000UniversalDTO>();
        GSM05000UniversalCls loCls;

        try
        {
            loCls = new GSM05000UniversalCls();

            _logger.LogInfo("Get Delimiter List");
            var loResult = loCls.GetDelimiterListDb();
            loRtn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Delimiter List");
        return loRtn;
    }
}