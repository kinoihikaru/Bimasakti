using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500MODEL.View_Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Menu.Tab;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class GSM02503 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        private bool IsUnitTypeCategoryHidden = false;

        private bool IsUnitTypeListExist = false;

        private UnitTypeTabParameterDTO loParameter = new UnitTypeTabParameterDTO();

        //Unit Type

        private GSM02503UnitTypeViewModel loUnitTypeViewModel = new();
        private R_Conductor _conductorUnitTypeRef;
        private R_Grid<GSM02503UnitTypeDTO> _gridUnitTypeRef;
        private string loUnitTypeLabel = "Active";
        private SelectedUnitTypeCategoryDTO _selectedUnitTypeCategory = new SelectedUnitTypeCategoryDTO();


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loParameter = (UnitTypeTabParameterDTO)poParameter;

                loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID = loParameter.CSELECTED_PROPERTY_ID;
                loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID;
                loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loParameter.CSELECTED_UNIT_TYPE_CATEGORY_NAME;

                await loUnitTypeViewModel.GetSelectedPropertyAsync();
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitType_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateUnitTypeDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loUnitTypeViewModel.DownloadTemplateUnitTypeAsync();

                    var saveFileName = $"Unit Type.xlsx";
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
            eventArgs.Parameter = new UploadUnitTypeParameterDTO()
            {
                PropertyData = loUnitTypeViewModel.SelectedProperty,
                SelectedUnitTypeCategory = loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID
            };
            eventArgs.TargetPageType = typeof(UploadUnitType);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridUnitTypeRef.R_RefreshGrid(null);
            }
        }


        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_conductorUnitTypeRef.R_ConductorMode == R_eConductorMode.Add || _conductorUnitTypeRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                eventArgs.Cancel = true;
                return;
            }
            if (eventArgs.TabStripTab.Id == "UnitType")
            {
                IsUnitTypeCategoryHidden = false;
            }
            else if (eventArgs.TabStripTab.Id == "Image")
            {
                if (_conductorUnitTypeRef.R_ConductorMode == R_eConductorMode.Normal && loUnitTypeViewModel.loUnitTypeList.Count() > 0)
                {
                    IsUnitTypeCategoryHidden = true;
                }
                else
                {
                    eventArgs.Cancel = true;
                }
            }
        }

        private void Grid_SavingUnitType(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUnitTypeViewModel.UnitTypeValidation((GSM02503UnitTypeDetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_Image_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            ImageTabParameterDTO loParam = new ImageTabParameterDTO()
            {
                CSELECTED_PROPERTY_ID = loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID,
                CSELECTED_UNIT_TYPE_ID = loUnitTypeViewModel.loUnitTypeDetail.CUNIT_TYPE_ID
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM02503Image);
        }

        private void After_Open_Image_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {

        }

        #region Unit Type

        private async Task Grid_DisplayUnitType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02503UnitTypeDetailDTO)eventArgs.Data;
                loUnitTypeViewModel.loUnitTypeDetail = loParam;
                loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loUnitTypeLabel = "Inactive";
                    loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loUnitTypeLabel = "Active";
                    loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetUnitTypeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeViewModel.GetUnitTypeListStreamAsync();
                eventArgs.ListEntityResult = loUnitTypeViewModel.loUnitTypeList;

                if (loUnitTypeViewModel.loUnitTypeList.Count() == 0)
                {
                    IsUnitTypeListExist = false;
                }
                else if (loUnitTypeViewModel.loUnitTypeList.Count() > 0)
                {
                    IsUnitTypeListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitTypeRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02503UnitTypeDetailDTO loParam = new GSM02503UnitTypeDetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02503UnitTypeDetailDTO>(eventArgs.Data);
                await loUnitTypeViewModel.GetUnitTypeAsync(loParam);

                eventArgs.Result = loUnitTypeViewModel.loUnitTypeDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void AssignUnitTypeCategoryToTemp()
        {
            _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID;
            _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME;
        }

        private void Grid_BeforeAddUnitType(R_BeforeAddEventArgs eventArgs)
        {
            AssignUnitTypeCategoryToTemp();
        }

        private void Grid_BeforeEditUnitType(R_BeforeEditEventArgs eventArgs)
        {
            AssignUnitTypeCategoryToTemp();
        }

        private void Grid_BeforeCancelUnitType(R_BeforeCancelEventArgs eventArgs)
        {
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID;
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME;
        }

        private async Task Grid_ServiceSaveUnitType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeViewModel.SaveUnitTypeAsync(
                    (GSM02503UnitTypeDetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitTypeViewModel.loUnitTypeDetail;

                if (loUnitTypeViewModel.loUnitTypeList.Count() == 0)
                {
                    IsUnitTypeListExist = false;
                }
                else if (loUnitTypeViewModel.loUnitTypeList.Count() > 0)
                {
                    IsUnitTypeListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUnitType(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02503UnitTypeDetailDTO loData = (GSM02503UnitTypeDetailDTO)eventArgs.Data;
                await loUnitTypeViewModel.DeleteUnitTypeAsync(loData);

                if (loUnitTypeViewModel.loUnitTypeList.Count() == 0)
                {
                    IsUnitTypeListExist = false;
                }
                else if (loUnitTypeViewModel.loUnitTypeList.Count() > 0)
                {
                    IsUnitTypeListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationUnitType(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02503UnitTypeDetailDTO loData = null;
            try
            {
                loData = (GSM02503UnitTypeDetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorUnitTypeRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02507";
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
                            IAPPROVAL_CODE = "GSM02507"
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


        private async Task R_Before_Open_Popup_ActivateInactiveUnitType(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02507"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loUnitTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02503UnitTypeDetailDTO)_conductorUnitTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02503UnitTypeDTO)_gridUnitTypeRef.GetCurrentData();
                    await _conductorUnitTypeRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02507" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactiveUnitType(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loUnitTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02503UnitTypeDetailDTO)_conductorUnitTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02503UnitTypeDTO)_gridUnitTypeRef.GetCurrentData();
                    await _conductorUnitTypeRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Lookup_UnitType_UnitTypeCategory(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CUSER_ID = "",
                CPROPERTY_ID = loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00600);
        }

        private void R_After_Open_Lookup_UnitType_UnitTypeCategory(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            /*
                        var loGetData = (GSM02502DTO)_conductorUnitTypeRef.R_GetCurrentData();
                        loGetData.CUNIT_TYPE_CATEGORY_ID = loTempResult.CUNIT_TYPE_CATEGORY_ID;
                        loGetData.CUNIT_TYPE_CATEGORY_NAME = loTempResult.CUNIT_TYPE_CATEGORY_NAME;*/

            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loTempResult.CUNIT_TYPE_CATEGORY_ID;
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loTempResult.CUNIT_TYPE_CATEGORY_NAME;

            if (_conductorUnitTypeRef.R_ConductorMode == R_eConductorMode.Normal)
            {
                _gridUnitTypeRef.R_RefreshGrid(null);
            }
        }
        #endregion
    }
}
