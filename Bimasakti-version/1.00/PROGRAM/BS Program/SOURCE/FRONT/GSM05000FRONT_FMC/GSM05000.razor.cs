using GSM05000COMMON_FMC;
using GSM05000MODEL_FMC;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000FRONT_FMC;

public partial class GSM05000 : R_Page
{
    private GSM05000ViewModel _GSM05000ViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM05000DTO> _gridRef;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetDelimiterList();

            var loGroupDescriptor = new List<R_GridGroupDescriptor>
            {
                new() { FieldName = "MODULE" }
            };
            await _gridRef.R_GroupBy(loGroupDescriptor);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Tab Transaction
    private R_PredefinedDock preNumber;
    private R_PredefinedDock preApprov;
    private async Task Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM05000DTO)eventArgs.Data;
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                await GetUpdateSample();
                _radioGroup = false;
                preNumber.Enabled = loParam.LINCREMENT_FLAG;
                preApprov.Enabled = loParam.LAPPROVAL_FLAG;
            }
            else
            {
                if (loParam.CPERIOD_MODE != "N")
                {
                    _radioGroup = true;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task Grid_GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetGridList();
            eventArgs.ListEntityResult = _GSM05000ViewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(eventArgs.Data);
            await _GSM05000ViewModel.GetEntity(loParam);
            eventArgs.Result = _GSM05000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GSM05000DTO)eventArgs.Data;
            await _GSM05000ViewModel.ValidationEntity(loData);

            var llCondition2 = loData.LINCREMENT_FLAG != _GSM05000ViewModel.TempEntity.LINCREMENT_FLAG;
            var llCondition3 = loData.LDEPT_MODE != _GSM05000ViewModel.TempEntity.LDEPT_MODE;
            var llCondition4 = loData.LTRANSACTION_MODE != _GSM05000ViewModel.TempEntity.LTRANSACTION_MODE;
            var llCondition5 = loData.CPERIOD_MODE != _GSM05000ViewModel.TempEntity.CPERIOD_MODE;
            var llCondition6 = loData.LAPPROVAL_FLAG != _GSM05000ViewModel.TempEntity.LAPPROVAL_FLAG;
            var llCondition7 = loData.LAPPROVAL_DEPT != _GSM05000ViewModel.TempEntity.LAPPROVAL_DEPT;
            var llCondition8 = loData.LUSE_THIRD_PARTY != _GSM05000ViewModel.TempEntity.LUSE_THIRD_PARTY;

            var llIsExistNumbering = await _GSM05000ViewModel.CheckExistData(loData, GSM05000eTabName.Numbering);
            if (llIsExistNumbering)
            {
                if (llCondition2 || llCondition3 || llCondition4 || llCondition5)
                {
                    var llReturn = await R_MessageBox.Show(_localizer["ConfirmLabel"],
                        _localizer["Confirm01"],
                        R_eMessageBoxButtonType.OKCancel);

                    if (llReturn == R_eMessageBoxResult.Cancel)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }
                }
            }

            var llIsExistApproval = await _GSM05000ViewModel.CheckExistData(loData, GSM05000eTabName.Approval);

            if (llIsExistApproval)
            {
                if (llCondition6 || llCondition7 || llCondition8)
                {
                    var llReturn = await R_MessageBox.Show(_localizer["ConfirmLabel"],
                        _localizer["Confirm02"],
                        R_eMessageBoxButtonType.OKCancel);

                    if (llReturn == R_eMessageBoxResult.Cancel)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void Saving(R_SavingEventArgs eventArgs)
    {
        var loData = (GSM05000DTO)eventArgs.Data;
        loData.CDEPT_DELIMITER ??= "";
        loData.CTRANSACTION_DELIMITER ??= "";
        loData.CPERIOD_DELIMITER ??= "";
        loData.CNUMBER_DELIMITER ??= "";
        loData.CPREFIX_DELIMITER ??= "";
        loData.DUPDATE_DATE = DateTime.Now;
    }
    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM05000DTO)eventArgs.Data;
            await _GSM05000ViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _gridEnabled = eventArgs.Enable;
    }
    private void BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        _GSM05000ViewModel.GetSequence();
    }
    #endregion

    #region Predifiend
    private void InstanceNumberingTab(R_InstantiateDockEventArgs eventArgs)
    {
        var loData = _conductorRef.R_GetCurrentData();
        eventArgs.TargetPageType = typeof(GSM05010);
        eventArgs.Parameter = loData;
    }

    private void InstanceApprovalTab(R_InstantiateDockEventArgs eventArgs)
    {
        var loData = _conductorRef.R_GetCurrentData();
        eventArgs.TargetPageType = typeof(GSM05020);
        eventArgs.Parameter = loData;
    }
    #endregion

    #region On Value Change
    private async Task CheckIncrementFlag(bool eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.LINCREMENT_FLAG = eventArgs;
            if (eventArgs == false)
            {
                _GSM05000ViewModel.Data.LDEPT_MODE = false;
                _GSM05000ViewModel.Data.LTRANSACTION_MODE = false;
                _GSM05000ViewModel.Data.CPERIOD_MODE = "";
                _GSM05000ViewModel.Data.INUMBER_LENGTH = 0;
                _GSM05000ViewModel.Data.CPREFIX = "";
                _GSM05000ViewModel.Data.CSUFFIX = "";
                _GSM05000ViewModel.Data.CYEAR_FORMAT = "";

                _GSM05000ViewModel.Data.CDEPT_DELIMITER = "";
                _GSM05000ViewModel.Data.CTRANSACTION_DELIMITER = "";
                _GSM05000ViewModel.Data.CPERIOD_DELIMITER = "";
                _GSM05000ViewModel.Data.CNUMBER_DELIMITER = "";
                _GSM05000ViewModel.Data.CPREFIX_DELIMITER = "";

                _GSM05000ViewModel.DeptSequence = 0;
                _GSM05000ViewModel.TrxSequence = 0;
                _GSM05000ViewModel.PeriodSequence = 0;
                _GSM05000ViewModel.LenSequence = 0;
                _GSM05000ViewModel.PrefixSequence = 0;
            }

            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void CheckApprovalFlag(bool eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.LAPPROVAL_FLAG = eventArgs;
            if (eventArgs == false)
            {
                _GSM05000ViewModel.Data.LAPPROVAL_FLAG = false;
                _GSM05000ViewModel.Data.CAPPROVAL_MODE = "";
                _GSM05000ViewModel.Data.LAPPROVAL_DEPT = false;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void CheckDeptMode(bool eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.LDEPT_MODE = eventArgs;
            //_GSM05000ViewModel.AutoSequence(eNumericList.DeptMode, (bool)eventArgs);
            if (!eventArgs)
            {
                _GSM05000ViewModel.Data.CDEPT_DELIMITER = "";
                _GSM05000ViewModel.DeptSequence = 0;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void CheckTrxMode(bool eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.LTRANSACTION_MODE = eventArgs;
            //_GSM05000ViewModel.AutoSequence(eNumericList.TrxMode, (bool)eventArgs);
            if (eventArgs == false)
            {
                _GSM05000ViewModel.Data.CTRANSACTION_DELIMITER = "";
                _GSM05000ViewModel.TrxSequence = 0;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CmbBoxTransDel(string eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.CTRANSACTION_DELIMITER = eventArgs;
            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CmbBoxDeptDel(string eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.CDEPT_DELIMITER = eventArgs;
            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CmbBoxPeriodDel(string eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.CPERIOD_DELIMITER = eventArgs;
            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CmbBoxListNumDel(string eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.CNUMBER_DELIMITER = eventArgs;
            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CmbBoxPrefixDel(string eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.Data.CPREFIX_DELIMITER = eventArgs;
            await GetUpdateSample();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private async Task CheckPeriodMode(string eventArgs)
    {
        _GSM05000ViewModel.Data.CPERIOD_MODE = eventArgs;
        var llCondition = eventArgs != "N";
        _radioGroup = llCondition;
        //if ((_GSM05000ViewModel.TempEntity.CPERIOD_MODE == "N" && llCondition) ||
        //    (_GSM05000ViewModel.TempEntity.CPERIOD_MODE != "N" && !llCondition))
        //{
        //    _GSM05000ViewModel.AutoSequence(eNumericList.PeriodMode, llCondition);
        //}
        //_GSM05000ViewModel.TempEntity.CPERIOD_MODE = (string)eventArgs;
        await GetUpdateSample();
    }

    private async Task CheckLenMode(int eventArgs)
    {
        _GSM05000ViewModel.Data.INUMBER_LENGTH = eventArgs;
        var llCondition = eventArgs != 0;
        //if ((_GSM05000ViewModel.TempEntity.INUMBER_LENGTH == 0 && llCondition)
        //    || (_GSM05000ViewModel.TempEntity.INUMBER_LENGTH != 0 && !llCondition))
        //{
        //    _GSM05000ViewModel.AutoSequence(eNumericList.LenMode, llCondition);
        //}
        //_GSM05000ViewModel.TempEntity.INUMBER_LENGTH = (int)eventArgs;
        await GetUpdateSample();
    }

    private async Task GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetSeqValue();
            await _GSM05000ViewModel.getFormatDocRefNo();
            
            if (_GSM05000ViewModel.REFNO.Length > 30)
            {
                loEx.Add("Err08", _localizer["Err08"]);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private bool _radioGroup = false;

    private bool _gridEnabled;
    #endregion
}