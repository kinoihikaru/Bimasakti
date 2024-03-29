﻿using GSM02500COMMON.DTOs.GSM02510;
using GSM02500MODEL.View_Model;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using GSM02500COMMON.DTOs.GSM02503;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON.DTOs.GSM02501;
using R_BlazorFrontEnd.Controls.Popup;

namespace GSM02500FRONT
{
    public partial class GSM02510 : R_Page
    {
        [Inject] private R_PopupService PopupService { get; set; }

        private GSM02510ViewModel loViewModel = new();

        private R_Conductor _conductorRef;

        private R_Grid<GSM02510DTO> _gridRef;

        private string loLabel = "Active";

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        private bool IsBuildingListExist = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                string poPropertyID = (string)poParameter;
                loTabParameter.CSELECTED_PROPERTY_ID = poPropertyID;
                loViewModel.SelectedProperty.CPROPERTY_ID = poPropertyID;

                await loViewModel.GetSelectedPropertyAsync();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02510DetailDTO)eventArgs.Data;

                loViewModel.loBuildingCategoryDetail = loParam;
                loViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loLabel = "Inactive";
                    loViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loLabel = "Active";
                    loViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private void Grid_SavingBuilding(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.BuildingValidation((GSM02510DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetBuildingListStreamAsync();

                if (loViewModel.loBuildingCategoryList.Count() == 0)
                {
                    IsBuildingListExist = false;
                }
                else if (loViewModel.loBuildingCategoryList.Count() > 0)
                {
                    IsBuildingListExist = true;
                }

                eventArgs.ListEntityResult = loViewModel.loBuildingCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            GSM02510DetailDTO loAddField = (GSM02510DetailDTO)eventArgs.Data;
            loAddField.DCREATE_DATE = DateTime.Now;
            loAddField.DUPDATE_DATE = DateTime.Now;

            IsBuildingListExist = false;
        }

        private void Grid_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loViewModel.loBuildingCategoryList.Count() > 0)
            {
                IsBuildingListExist = true;
            }
        }
        private async Task Grid_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            IsBuildingListExist = false;
            //await _propertyNameRef.FocusAsync();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02510DetailDTO loParam = new GSM02510DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02510DetailDTO>(eventArgs.Data);
                await loViewModel.GetBuildingAsync(loParam);

                eventArgs.Result = loViewModel.loBuildingCategoryDetail;
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
                await loViewModel.SaveBuildingAsync(
                    (GSM02510DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loViewModel.loBuildingCategoryList.Count() == 0)
                {
                    IsBuildingListExist = false;
                }
                else if (loViewModel.loBuildingCategoryList.Count() > 0)
                {
                    IsBuildingListExist = true;
                }

                eventArgs.Result = loViewModel.loBuildingCategoryDetail;
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
                GSM02510DetailDTO loData = (GSM02510DetailDTO)eventArgs.Data;
                await loViewModel.DeleteBuildingAsync(loData);

                if (loViewModel.loBuildingCategoryList.Count() == 0)
                {
                    IsBuildingListExist = false;
                }
                else if (loViewModel.loBuildingCategoryList.Count() > 0)
                {
                    IsBuildingListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationBuilding(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02510DetailDTO loData = null;
            try
            {
                loData = (GSM02510DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02502";
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
                            IAPPROVAL_CODE = "GSM02502"
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
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02502"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02510DetailDTO)_conductorRef.R_GetCurrentData();
                    var loHeaderData = (GSM02510DTO)_gridRef.GetCurrentData();
                    await _conductorRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02502" //Uabh Approval Code sesuai Spec masing masing
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
                    await loViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02510DetailDTO)_conductorRef.R_GetCurrentData();
                    var loHeaderData = (GSM02510DTO)_gridRef.GetCurrentData();
                    await _conductorRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void R_Before_OpenFloor_Detail(R_BeforeOpenDetailEventArgs eventArgs)
        {
            loTabParameter.CSELECTED_BUILDING_ID = loViewModel.loBuildingCategoryDetail.CBUILDING_ID;
            eventArgs.Parameter = loTabParameter;
            if (loViewModel.loBuildingCategoryList.Count() > 0)
            {
                eventArgs.TargetPageType = typeof(GSM02520);
            }
        }

        private void R_After_OpenFloor_Detail()
        {

        }
    }
}
