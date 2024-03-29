﻿using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;

namespace LMM01500FRONT
{
    public partial class LMM01530 : R_Page, R_ITabPage
    {
        private LMM01530ViewModel _OtherCharges_viewModel = new LMM01530ViewModel();
        private R_Grid<LMM01530DTO> _OtherCharges_gridRef;
        private R_ConductorGrid _OtherCharges_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<LMM01530DTO>(poParameter);

                _OtherCharges_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _OtherCharges_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _OtherCharges_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _OtherCharges_gridRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_MODULE_NAME = "LM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (LMM01530DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM01530",
                        Table_Name = "LMM_INVGRP_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM01530",
                        Table_Name = "LMM_INVGRP_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CINVGRP_CODE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        private bool enableAdd = true;
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMM01530DTO>(poParam);
            if (!string.IsNullOrWhiteSpace(loParam.CINVGRP_CODE))
            {
                _OtherCharges_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                _OtherCharges_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                _OtherCharges_viewModel.InvGrpName = loParam.CINVGRP_NAME;
                enableAdd = true;
                await _OtherCharges_gridRef.R_RefreshGrid(loParam);
            }
            else
            {
                enableAdd = false;
                _OtherCharges_gridRef.DataSource.Clear();
                _OtherCharges_viewModel.InvGrpCode = "";
                _OtherCharges_viewModel.InvGrpName = "";
            }
        }

        private async Task OtherCharges_Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _OtherCharges_viewModel.GetOtherChargesList((LMM01530DTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _OtherCharges_viewModel.OtherChargesGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                LMM01530DTO loParam = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.GetOtherCharges(loParam);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01530DTO loData = (LMM01530DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext;
                    loData.CINVGRP_CODE = _OtherCharges_viewModel.InvGrpCode;
                }

                await _OtherCharges_viewModel.SaveOtherCharges(
                    loData,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01530DTO loData = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OtherCharges_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {

            var param = new LMM01531DTO()
            {
                CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext,
                CUSER_ID = clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LMM01531);
        }

        private void OtherCharges_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (LMM01531DTO)eventArgs.Result;
            var loData = (LMM01530DTO)eventArgs.ColumnData;
            if (loTempResult is null)
            {
                return;
            }

            loData.CINVGRP_CODE = _OtherCharges_viewModel.Data.CINVGRP_CODE;
            loData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            loData.UNIT_UTILITY_CHARGE = loTempResult.UNIT_UTILITY_CHARGE;
            loData.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
            loData.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
        }

        private void OtherCharges_CellLostFocus(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
                if (eventArgs.ColumnName == "CCHARGES_ID")
                {
                    var loData = (LMM01530DTO)_OtherCharges_gridRef.GetCurrentData();
                    loData.CCHARGES_NAME = "tes";
                    //if (eventArgs.Value.ToString().Length > 0)
                    //{
                    //    var loParam = new LMM01531DTO { CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext, CSEARCH_TEXT = eventArgs.Value.ToString() };

                    //    LMM01531ViewModel loLookupViewModel = new LMM01531ViewModel();

                    //    var loResult = await loLookupViewModel.GetLookupOtherChargesRecord(loParam);

                    //    var loChargesNameColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_NAME");
                    //    var loUnitUtilityChargeColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "UNIT_UTILITY_CHARGE");
                    //    var loUnitChargesTypeDescColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_TYPE_DESCR");
                    //    var loUnitChargesTypeColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_TYPE");

                    //    if (loResult == null)
                    //    {
                    //        loEx.Add(R_FrontUtility.R_GetError(
                    //                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    //                "_ErrLookup01"));

                    //        //loChargesNameColumn.Value = "";
                    //        //loUnitUtilityChargeColumn.Value = "";
                    //        //loUnitChargesTypeDescColumn.Value = "";
                    //        //loUnitChargesTypeColumn.Value = "";
                    //        goto EndBlock;
                    //    }

                    //    //loChargesNameColumn.Value = loResult.CCHARGES_NAME;
                    //    //loUnitUtilityChargeColumn.Value = loResult.UNIT_UTILITY_CHARGE;
                    //    //loUnitChargesTypeDescColumn.Value = loResult.CCHARGES_TYPE_DESCR;
                    //    //loUnitChargesTypeColumn.Value = loResult.CCHARGES_TYPE;
                    //}
                    //else
                    //{
                    //    //loChargesNameColumn.Value = "";
                    //    //loUnitUtilityChargeColumn.Value = "";
                    //    //loUnitChargesTypeDescColumn.Value = "";
                    //    //loUnitChargesTypeColumn.Value = "";
                    //}
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private void OtherCharges_ValueChange(R_CellValueChangedEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01530DTO)_OtherCharges_gridRef.GetCurrentData();
                if (eventArgs.ColumnName == "CCHARGES_ID")
                {
                    
                    //if (eventArgs.Value.ToString().Length > 0)
                    //{
                    //    var loParam = new LMM01531DTO { CPROPERTY_ID = _OtherCharges_viewModel.PropertyValueContext, CSEARCH_TEXT = eventArgs.Value.ToString() };

                    //    LMM01531ViewModel loLookupViewModel = new LMM01531ViewModel();

                    //    var loResult = await loLookupViewModel.GetLookupOtherChargesRecord(loParam);

                    //    var loChargesNameColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_NAME");
                    //    var loUnitUtilityChargeColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "UNIT_UTILITY_CHARGE");
                    //    var loUnitChargesTypeDescColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_TYPE_DESCR");
                    //    var loUnitChargesTypeColumn = eventArgs.Columns.FirstOrDefault(x => x.Name == "CCHARGES_TYPE");

                    //    if (loResult == null)
                    //    {
                    //        loEx.Add(R_FrontUtility.R_GetError(
                    //                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    //                "_ErrLookup01"));

                    //        //loChargesNameColumn.Value = "";
                    //        //loUnitUtilityChargeColumn.Value = "";
                    //        //loUnitChargesTypeDescColumn.Value = "";
                    //        //loUnitChargesTypeColumn.Value = "";
                    //        goto EndBlock;
                    //    }

                    //    //loChargesNameColumn.Value = loResult.CCHARGES_NAME;
                    //    //loUnitUtilityChargeColumn.Value = loResult.UNIT_UTILITY_CHARGE;
                    //    //loUnitChargesTypeDescColumn.Value = loResult.CCHARGES_TYPE_DESCR;
                    //    //loUnitChargesTypeColumn.Value = loResult.CCHARGES_TYPE;
                    //}
                    //else
                    //{
                    //    //loChargesNameColumn.Value = "";
                    //    //loUnitUtilityChargeColumn.Value = "";
                    //    //loUnitChargesTypeDescColumn.Value = "";
                    //    //loUnitChargesTypeColumn.Value = "";
                    //}
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
