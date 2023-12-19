using GSM05000COMMON_FMC;
using GSM05000MODEL_FMC;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000FRONT_FMC;

public partial class GSM05021 : R_Page
{
    private GSM05020DeptViewModel _deptViewModel = new();
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            _deptViewModel.FromDept = (GSM05020DeptDTO)poParameter;
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GSL00700ParameterDTO { CPROGRAM_ID= "GSM05000" };
        eventArgs.TargetPageType = typeof(GSL00700);
    }

    private void AfterOpenLookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loData = (GSL00700DTO)eventArgs.Result;
        if (loData == null)
            return;

        _deptViewModel.ToDept.CDEPT_CODE = loData.CDEPT_CODE;
        _deptViewModel.ToDept.CDEPT_NAME = loData.CDEPT_NAME;
    }

    public async Task Button_OnClickProcessAsync()
    {
        var loData = _deptViewModel.ToDept;
        await this.Close(true, loData);
    }
    public async Task Button_OnClickCloseAsync()
    {
        await this.Close(true, null);
    }
}