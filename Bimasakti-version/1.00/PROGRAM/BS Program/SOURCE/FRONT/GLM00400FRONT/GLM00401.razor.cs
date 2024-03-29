﻿using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace GLM00400FRONT
{
    public partial class GLM00401 : R_Page
    {
        private GLM00401ViewModel _AllocationJournalHD_viewModel = new GLM00401ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }


        // object private dispaly
        private string _DeptName;
        private int _IYear;
        private int _IMin;
        private int _IMax;
        private R_TextBox DeptCode_TextBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // set initial proses
                if (poParameter is not null)
                {
                    var loData = (GLM00400DTO)poParameter;
                    _IYear = int.Parse(loData.CSOFT_PERIOD_YY);
                    _IMin = loData.IMIN_YEAR;
                    _IMax = loData.IMAX_YEAR;

                    await _AllocationJournalHD_viewModel.GetAllocationJournalFromToList(null);
                }
                
                await DeptCode_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task DeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_AllocationJournalHD_viewModel.Data.CDEPT_CODE.Length > 0)
                {
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _AllocationJournalHD_viewModel.Data.CDEPT_CODE
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _DeptName = "";
                        goto EndBlock;
                    }
                    _AllocationJournalHD_viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    _DeptName = loResult.CDEPT_NAME;
                }
                else
                {
                    _DeptName = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Allocation_Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs e)
        {
            e.Parameter = new GSL00700ParameterDTO();
            e.TargetPageType = typeof(GSL00700);
        }

        private void Allocation_Department_From_After_Open_Lookup(R_AfterOpenLookupEventArgs e)
        {
            var loData = (GSL00700DTO)e.Result;
            if (loData == null)
            {
                return;
            }

            _AllocationJournalHD_viewModel.Data.CDEPT_CODE = loData.CDEPT_CODE;
            _DeptName = loData.CDEPT_NAME;
        }

        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GLM00400PrintParamDTO)_AllocationJournalHD_viewModel.R_GetCurrentData();

                //Set Data 
                loData.CYEAR = _IYear.ToString();
                loData.CCOMPANY_ID = clientHelper.CompanyId;
                loData.CUSER_ID = clientHelper.UserId;
                loData.CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName;

                await _AllocationJournalHD_viewModel.ValidationAllocation(loData);

                loData.CCOMPANY_ID = clientHelper.CompanyId;
                await _reportService.GetReport(
                "R_DefaultServiceUrlGL",
                "GL",
                "rpt/GLM00400Print/AllAllocationJournalPost",
                "rpt/GLM00400Print/AllStreamAllocationJournalGet",
                loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Button_OnClickCancelAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await this.Close(true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
