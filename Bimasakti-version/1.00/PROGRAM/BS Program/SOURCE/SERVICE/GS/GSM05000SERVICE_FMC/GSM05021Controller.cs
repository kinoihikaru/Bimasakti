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
public class GSM05021Controller : ControllerBase, IGSM05021
{
    private LoggerGSM05021 _logger;
    
    public GSM05021Controller(ILogger<GSM05021Controller> logger)
    {
        //Initial and Get Logger
        LoggerGSM05021.R_InitializeLogger(logger);
        _logger = LoggerGSM05021.R_GetInstanceLogger();
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05021DTO> GetApprovalReplacementListStream()
    {
        _logger.LogInfo("Start - Get Approval List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05021DTO> loRtn = null;
        GSM05021Cls loCls;

        try
        {
            loCls = new GSM05021Cls();

            //SetParam
            GSM05021DTO loParam = new GSM05021DTO();
            loParam.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANSACTION_CODE);
            loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
            loParam.CUSER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CUSER_ID);
            loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Approval List");
            var loResult = loCls.GetApprovaReplacelListDb(loParam);
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval List");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05021DTO> poParameter)
    {
        _logger.LogInfo("Start - Delete User Replacement Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05021Cls loCls;

        try
        {
            loCls = new GSM05021Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Delete User Replacement Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete User Replacement Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05021DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05021DTO> poParameter)
    {
        _logger.LogInfo("Start - Get User Replacement Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05021DTO> loRtn = new();

        try
        {
            var loCls = new GSM05021Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get User Replacement Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get User Replacement Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05021DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05021DTO> poParameter)
    {
        _logger.LogInfo("Start - Save User Replacement Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05021DTO> loRtn = null;
        GSM05021Cls loCls;

        try
        {
            loCls = new GSM05021Cls();
            loRtn = new R_ServiceSaveResultDTO<GSM05021DTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Save User Replacement Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save User Replacement Entity");
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