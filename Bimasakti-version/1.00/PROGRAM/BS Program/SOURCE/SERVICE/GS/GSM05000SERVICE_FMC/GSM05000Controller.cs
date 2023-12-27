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
public class GSM05000Controller : ControllerBase, IGSM05000
{
    private LoggerGSM05000 _logger;
    
    public GSM05000Controller(ILogger<LoggerGSM05000> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000.R_InitializeLogger(logger);
        _logger = LoggerGSM05000.R_GetInstanceLogger();
    }
    
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000DTO> poParameter)
    {
        _logger.LogInfo("Start - Get Transaction Code Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000DTO> loRtn = new();

        try
        {
            var loCls = new GSM05000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get Transaction Code Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Code Record");
        return loRtn;
    }
    
    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000DTO> poParameter)
    {
        _logger.LogInfo("Start - Save Transaction Code Entity");
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM05000DTO> loRtn = new();
        GSM05000Cls loCls;

        try
        {
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUPDATE_BY = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Save Transaction Code Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Transaction Code Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000DTO> poParameter)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public IAsyncEnumerable<GSM05000DTO> GetTransactionCodeListStream()
    {
        _logger.LogInfo("Start - Get Transaction Code List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05000DTO> loRtn = null;
        GSM05000Cls loCls;

        try
        {
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Get Transaction Code List");
            var loResult = loCls.GetTransactionCodeListDb();
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Code List");
        return loRtn;
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05000ValidateDTO> ValidateTransactionCode(GSM05000ValidateDTO poEntity)
    {
        _logger.LogInfo("Start - Check Exist Data");
        R_Exception loEx = new();
        GSM05000SingleResult<GSM05000ValidateDTO> loRtn = new GSM05000SingleResult<GSM05000ValidateDTO>();
        GSM05000Cls loCls;

        try
        {
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Check Exist Data");
            var loResult = loCls.GetValidateUpdateDb(poEntity);
            loRtn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Check Exist Data");
        return loRtn;
    }
    
    
    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<T> GetTransactionCodeStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}