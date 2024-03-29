﻿using BlazorClientHelper;
using LMM06500COMMON;
using LMM06500FrontResources;
using LMM06500MODEL;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Lookup_LMModel.ViewModel.LML00300;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM06500FRONT
{
    public partial class LMM06502 : R_Page
    {
        private LMM06502ViewModel _viewModel = new LMM06502ViewModel();
        private R_Conductor _Staff_Header_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private R_Grid<LMM06502DetailDTO> _StaffMoveDetail_gridRef;
        private R_TextBox OldSupervisor_TextBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM06502HeaderDTO)poParameter;
                _viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _viewModel.StaffMoveHeader.CPROPERTY_ID = loData.CPROPERTY_ID;

                await OldSupervisor_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OldSupervisor_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID.Length > 0)
                {
                    var param = new LML00300ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID
                    };

                    LookupLML00300ViewModel loLookupViewModel = new LookupLML00300ViewModel();

                    var loResult = await loLookupViewModel.GetSupervisor(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.StaffMoveHeader.COLD_SUPERVISOR_NAME = "";
                        _viewModel.StaffMoveHeader.COLD_DEPT_CODE = "";
                        _viewModel.StaffMoveHeader.COLD_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID = loResult.CSUPERVISOR;
                    _viewModel.StaffMoveHeader.COLD_SUPERVISOR_NAME = loResult.CSUPERVISOR_NAME;
                    _viewModel.StaffMoveHeader.COLD_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.StaffMoveHeader.COLD_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.StaffMoveHeader.COLD_SUPERVISOR_NAME = "";
                    _viewModel.StaffMoveHeader.COLD_DEPT_CODE = "";
                    _viewModel.StaffMoveHeader.COLD_DEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private async Task NewSupervisor_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID.Length > 0)
                {
                    var param = new LML00300ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID
                    };

                    LookupLML00300ViewModel loLookupViewModel = new LookupLML00300ViewModel();

                    var loResult = await loLookupViewModel.GetSupervisor(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_NAME = "";
                        _viewModel.StaffMoveHeader.CNEW_DEPT_CODE = "";
                        _viewModel.StaffMoveHeader.CNEW_DEPT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID = loResult.CSUPERVISOR;
                    _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_NAME = loResult.CSUPERVISOR_NAME;
                    _viewModel.StaffMoveHeader.CNEW_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.StaffMoveHeader.CNEW_DEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_NAME = "";
                    _viewModel.StaffMoveHeader.CNEW_DEPT_CODE = "";
                    _viewModel.StaffMoveHeader.CNEW_DEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Staff_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00300ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyValueContext,
                CUSER_ID = clientHelper.UserId
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00300);
        }

        private async Task Staff_Old_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loParam = new LMM06502DetailDTO();
            var loTempResult = (LML00300DTO)eventArgs.Result;

            if (loTempResult == null)
            {
                return;
            }

            _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID = loTempResult.CSUPERVISOR;
            _viewModel.StaffMoveHeader.COLD_SUPERVISOR_NAME = loTempResult.CSUPERVISOR_NAME;
            _viewModel.StaffMoveHeader.COLD_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.StaffMoveHeader.COLD_DEPT_NAME = loTempResult.CDEPT_NAME;

            loParam.COLD_SUPERVISOR_ID = loTempResult.CSUPERVISOR;

            await _StaffMoveDetail_gridRef.R_RefreshGrid(loParam);
        }

        private void Staff_New_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID = loTempResult.CSUPERVISOR;
            _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_NAME = loTempResult.CSUPERVISOR_NAME;
            _viewModel.StaffMoveHeader.CNEW_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.StaffMoveHeader.CNEW_DEPT_NAME = loTempResult.CDEPT_NAME;
        }


        private async Task _Staff_Detail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetStaffMoveList((LMM06502DetailDTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _viewModel.StaffMoveGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            var loData = new LMM06502DTO();
            string loDetailData = "";

            try
            {
                var loDetailTempData = _StaffMoveDetail_gridRef.DataSource.Where(x => x.LSELECTED).Select(x => x.CSTAFF_ID);

                loDetailData = string.Join(",", loDetailTempData);

                if (!string.IsNullOrWhiteSpace(_viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID) && !string.IsNullOrWhiteSpace(_viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID) && loDetailTempData.Count() > 0)
                {
                    if (_viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID != _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID)
                    {
                        var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifReplaceOldToNew"), R_eMessageBoxButtonType.YesNo);
                        if (loValidate == R_eMessageBoxResult.Yes)
                        {
                            loData = R_FrontUtility.ConvertObjectToObject<LMM06502DTO>(_viewModel.StaffMoveHeader);
                            loData.CSTAFF_LIST = loDetailData;

                            await _viewModel.SaveStaffMove(loData);
                            await this.Close(true, true);
                        }
                    }
                    else
                    {
                        await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifDulticateOldAndNew"), R_eMessageBoxButtonType.OK);
                    }
                }
                else
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifNoProcessData"), R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
