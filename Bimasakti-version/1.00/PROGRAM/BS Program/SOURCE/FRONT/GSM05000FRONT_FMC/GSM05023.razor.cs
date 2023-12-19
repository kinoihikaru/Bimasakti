using GSM05000COMMON_FMC;
using GSM05000MODEL_FMC;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000FRONT_FMC;

public partial class GSM05023 : R_Page
{
    
    private GSM05020ViewModel _viewModel = new();
    private R_ConductorGrid _conductor;
    private R_Grid<GSM05020DTO> _grid;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loArgs = (string)poParameter;
            _viewModel.TransactionCode = loArgs;
            await _viewModel.GetDeptApprovalList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private void Display(R_DisplayEventArgs eventArgs)
    {
        var loData = (GSM05020DTO)eventArgs.Data;
        _viewModel.GetEnableMethod(loData);
    }

    private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetApprovalByDeptList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeDrop(R_GridRowAfterDropEventArgs eventArgs)
    {
    }

    private void AfterDrop(R_GridRowBeforeDropEventArgs eventArgs)
    {
    }

    private async Task ChangedDept(string poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.DeptCode = poParam;

            await _grid.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickNext()
    {

        var loData = _grid.CurrentSelectedData;
        await _viewModel.SwapUpSeqMethod(poBtnClick: GetBtnClickUpOrDown.Up, loData);

        await _grid.R_MoveToNextRow();
    }

    private async Task OnClickPrevious()
    {

        var loData = _grid.CurrentSelectedData;
        await _viewModel.SwapUpSeqMethod(poBtnClick: GetBtnClickUpOrDown.Down, loData);

        await _grid.R_MoveToPreviousRow();
    }
    
    private async Task OnClickSave()
    {
        var loEx = new R_Exception();

        try
        {

            await _viewModel.SaveBacthSeqApproval();
            await this.Close(true, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task OnClickCancel()
    {
        await this.Close(true, false);
    }
}