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
public class GSM05020Controller : ControllerBase, IGSM05020
{
    private LoggerGSM05020 _logger;
    
    public GSM05020Controller(ILogger<LoggerGSM05020> logger)
    {
        //Initial and Get Logger
        LoggerGSM05020.R_InitializeLogger(logger);
        _logger = LoggerGSM05020.R_GetInstanceLogger();
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05020DTO> BatchSeqApproval(GSM05000ParameterWithCRUDMode<GSM05020DTO> poParameter)
    {
        _logger.LogInfo("Start - Save Batch Approval User Entity");
        R_Exception loEx = new();
        GSM05000SingleResult<GSM05020DTO> loRtn = null;
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();
            loRtn = new GSM05000SingleResult<GSM05020DTO>();

            _logger.LogInfo("Save Batch Approval User Entity");
            loCls.SaveBatchSeq(poParameter.Data, poParameter.poCRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Batch Approval User Entity");
        return loRtn;
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyFromApproval(GSM05020ParamFromToDeptDTO poEntity)
    {
        _logger.LogInfo("Start - Copy From Approval");
        R_Exception loEx = new();
        GSM05000SingleResult<GSM05020ParamFromToDeptDTO> loRtn = new GSM05000SingleResult<GSM05020ParamFromToDeptDTO>();
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            _logger.LogInfo("Copy From Approval");
            loCls.GSM05000ApprovalCopyFrom(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Copy From Approval");
        return loRtn;
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyToApproval(GSM05020ParamFromToDeptDTO poEntity)
    {
        _logger.LogInfo("Start - Copy To Approval");
        R_Exception loEx = new();
        GSM05000SingleResult<GSM05020ParamFromToDeptDTO> loRtn = new GSM05000SingleResult<GSM05020ParamFromToDeptDTO>();
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            _logger.LogInfo("Copy To Approval");
            loCls.GSM05000ApprovalCopyTo(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Copy To Approval");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05020DTO> GetApprovalListStream()
    {
        _logger.LogInfo("Start - Get Approval List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05020DTO> loRtn = null;
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            //SetParam
            GSM05020DTO loParam = new GSM05020DTO();
            loParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANSACTION_CODE);
            loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
            loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Approval List");
            var loResult = loCls.GetApprovalListDb(loParam);
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
    public IAsyncEnumerable<GSM05020DeptDTO> GetDeptTransListStream()
    {
        _logger.LogInfo("Start - Get Department Transaction List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05020DeptDTO> loRtn = null;
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            //SetParam
            var loTransCode = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANSACTION_CODE);

            _logger.LogInfo("Get Department Transaction List");
            var loResult = loCls.GSM05000DepartmentChangeSequence(loTransCode);
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Department Transaction List");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05020DTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Approval User Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Delete Approval User Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Approval User Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05020DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05020DTO> poParameter)
    {
        _logger.LogInfo("Start - Get Approval User Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05020DTO> loRtn = new();

        try
        {
            var loCls = new GSM05020Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Approval User Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval User Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05020DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05020DTO> poParameter)
    {
        _logger.LogInfo("Start - Save Approval User Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05020DTO> loRtn = null;
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();
            loRtn = new R_ServiceSaveResultDTO<GSM05020DTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Save Approval User Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Approval User Entity");
        return loRtn;
    }

    [HttpPost]
    public GSM05000SingleResult<GSM05020ValidateDTO> ValidateApprovalReplacement(GSM05020ValidateDTO poEntity)
    {
        _logger.LogInfo("Start - Check Approval Replacement Data");
        R_Exception loEx = new();
        GSM05000SingleResult<GSM05020ValidateDTO> loRtn = new GSM05000SingleResult<GSM05020ValidateDTO>();
        GSM05020Cls loCls;

        try
        {
            loCls = new GSM05020Cls();

            _logger.LogInfo("Check Approval Replacement Data");
            var loResult = loCls.GetValidateApprovalReplacementDB(poEntity);
            loRtn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Check Approval Replacement Data");
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