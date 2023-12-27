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
public class GSM05010Controller : ControllerBase, IGSM05010
{
    private LoggerGSM05010 _logger;
    
    public GSM05010Controller(ILogger<LoggerGSM05010> logger)
    {
        //Initial and Get Logger
        LoggerGSM05010.R_InitializeLogger(logger);
        _logger = LoggerGSM05010.R_GetInstanceLogger();
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05011DTO> GetHeaderTransCodeNumber(GSM05011DTO poEntity)
    {
        _logger.LogInfo("Start - Get Transaction Code HD Number");
        R_Exception loEx = new R_Exception();
        GSM05000SingleResult<GSM05011DTO> loRtn = new GSM05000SingleResult<GSM05011DTO>();
        GSM05010Cls loCls;

        try
        {
            loCls = new GSM05010Cls();

            _logger.LogInfo("Get Transaction Code HD Number");
            var loResult = loCls.GetNumberingHeaderDb(poEntity);
            loRtn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Transaction Code HD Number");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05010DTO> GetHeaderTransCodeNumberListStream()
    {
        _logger.LogInfo("Start - Get Transaction Code Number List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05010DTO> loRtn = null;
        GSM05010Cls loCls;

        try
        {
            loCls = new GSM05010Cls();

            //SetParam
            GSM05010DTO loParam = new GSM05010DTO();
            loParam.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANSACTION_CODE);
            loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Transaction Code Number List");
            var loResult = loCls.GetNumberingListDb(loParam);
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Code Number List");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05010DTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Numbering Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05010Cls loCls;

        try
        {
            loCls = new GSM05010Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Numbering Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Numbering Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05010DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05010DTO> poParameter)
    {
        _logger.LogInfo("Start - Get Numbering Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05010DTO> loRtn = new();

        try
        {
            var loCls = new GSM05010Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Numbering Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Numbering Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05010DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05010DTO> poParameter)
    {
        _logger.LogInfo("Start - Save Numbering Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05010DTO> loRtn = new();
        GSM05010Cls loCls;

        try
        {
            loCls = new GSM05010Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Numbering Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Numbering Entity");
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