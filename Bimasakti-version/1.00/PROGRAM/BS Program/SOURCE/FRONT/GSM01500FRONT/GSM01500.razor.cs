using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM01500COMMON.DTOs;
using GSM01500MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM01500FRONT
{
    public partial class GSM01500 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        private GSM01500ViewModel CenterViewModel = new GSM01500ViewModel();

        private R_ConductorGrid _conGridCenterRef;

        private R_Grid<GSM01500DTO> _gridCenterRef;

        private string loLabel = "Active";

        private bool IsEnabled = true;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridCenterRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_SetOther(R_SetEventArgs eventArgs)
        {
            IsEnabled = eventArgs.Enable;
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await CenterViewModel.DownloadTemplateCenterAsync();

                    var saveFileName = $"Center.xlsx";
/*
                    var saveFileName = $"Staff {CenterViewModel.PropertyValueContext}.xlsx";*/

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(UploadCenter);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridCenterRef.R_RefreshGrid(null);
            }
        }

        private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            SelectedCenterCodeDTO loParam = null;
            try
            {
                loParam = new SelectedCenterCodeDTO()
                {
                    CENTER_ID = CenterViewModel.loCenter.CCENTER_CODE,
                    CENTER_NAME = CenterViewModel.loCenter.CCENTER_NAME
                };
                eventArgs.Parameter = loParam;
                if (CenterViewModel.loCenterList.Count() > 0)
                {
                    eventArgs.TargetPageType = typeof(GSM01510);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        private void Grid_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                CenterViewModel.CenterValidation((GSM01500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM01500DTO)eventArgs.Data;
                CenterViewModel.loCenter = loParam;
                CenterViewModel.SelectedActiveInactiveCenterCode = loParam.CCENTER_CODE;
                CenterViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;

                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    CenterViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loLabel = "Active";
                    CenterViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await CenterViewModel.GetCenterListStreamAsync();
                eventArgs.ListEntityResult = CenterViewModel.loCenterList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM01500DTO loParam = (GSM01500DTO)eventArgs.Data;
                await CenterViewModel.GetCenterAsync(loParam);

                eventArgs.Result = CenterViewModel.loCenter;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await CenterViewModel.SaveCenterAsync(
                    (GSM01500DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = CenterViewModel.loCenter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM01500DTO loData = (GSM01500DTO)eventArgs.Data;
                await CenterViewModel.DeleteCenterAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup_CopyFrom(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(CopyFromModal);
        }

        private async Task R_After_Open_Popup_CopyFrom(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _gridCenterRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridCenterRef.R_RefreshGrid(null);
        }

        private void R_Before_Open_Popup_PrintCenter(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM01501);
        }

        private void R_After_Open_Popup_PrintCenter(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM01500DTO loData = null;
            try
            {
                loData = (GSM01500DTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conGridCenterRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM01501";
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM01501"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        if (loResult.Success == false || (bool)loResult.Result == false)
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM01501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1) 
                {
                    await CenterViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridCenterRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM01501" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await CenterViewModel.ActiveInactiveProcessAsync();
                    await _gridCenterRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
