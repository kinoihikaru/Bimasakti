using BlazorClientHelper;
using GSM04500COMMON;
using GSM04500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500FRONT
{
    public partial class GSM04510 : R_Page, R_ITabPage
    {
        private GSM04510ViewModel _viewModel = new();
        private GSM04511ViewModel _viewModelDetail = new();
        private R_Grid<GSM04510DTO> JournalGroupGOA_gridRef;
        private R_Grid<GSM04511DTO> JournalGroupGOAByDept_gridRef;
        private R_ConductorGrid _conRef;
        private R_ConductorGrid _conRefDetail;

        #region Property Partial
        private GSM04500DTO HeaderData = new();
        private string GOAName;
        private bool ByDeptEnable = true;
        private bool OnCRUDMode = true;
        private bool PageOnCRUDMode = true;
        #endregion

        #region Inject
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                if (poParameter is not null)
                {
                    HeaderData = (GSM04500DTO)poParameter;
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM04510ParameterDTO>(poParameter);

                    await JournalGroupGOA_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (poParam is not null)
                {
                    HeaderData = (GSM04500DTO)poParam;
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM04510ParameterDTO>(poParam);
                    await JournalGroupGOA_gridRef.R_RefreshGrid(loParam);
                }
                else
                {
                    HeaderData = new();
                    GOAName = "";
                    JournalGroupGOA_gridRef.DataSource.Clear();
                    JournalGroupGOAByDept_gridRef.DataSource.Clear();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetJournalGroupGOAList((GSM04510ParameterDTO)eventArgs.Parameter);
                eventArgs.ListEntityResult = _viewModel.JournalGroupGOAGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }


        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510DTO)eventArgs.Data;
                await _viewModel.GetJournalGroupGOA(loParam);

                eventArgs.Result = _viewModel.JournalGroupGOA;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveGOA(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04510DTO)eventArgs.Data;

                await _viewModel.SaveJournalGroupGOA(loParam, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.JournalGroupGOA;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            var loData = (GSM04510DTO)eventArgs.Data;
            ByDeptEnable = loData.LDEPARTMENT_MODE;
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                GOAName = loData.CGOA_NAME_CODE;

                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04511ParameterDTO>(loData);
                await JournalGroupGOAByDept_gridRef.R_RefreshGrid(loParam);
            }
            
        }
        private async Task SetOther(R_SetEventArgs eventArgs)
        {
            PageOnCRUDMode = eventArgs.Enable; 
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        #region GOALOOKUP
        //  Button LookUp GOA
        private void BeforeOpenLookUPGOA(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loData = JournalGroupGOA_gridRef.CurrentSelectedData;
            var loParam = R_FrontUtility.ConvertObjectToObject<GSL00520ParameterDTO>(loData);

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00520);

        }

        private void AfterOpenLookGOA(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            if (eventArgs.ColumnName == "GLAccount_No")
            {
                var loTempResult2 = (GSL00520DTO)eventArgs.Result;
                ((GSM04510DTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                ((GSM04510DTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
            }
        }
        #endregion

        #region GroupOfAccountDept
        private async Task GridGOADept_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04511ParameterDTO)eventArgs.Parameter;

                await _viewModelDetail.GetJournalGroupGOAByDeptList(loParam);
                eventArgs.ListEntityResult = _viewModelDetail.JournalGroupGOAByDeptGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordGOADeptAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04511DTO)eventArgs.Data;
                await _viewModelDetail.GetJournalGroupGOAByDept(loParam);

                eventArgs.Result = _viewModelDetail.JournalGroupGOAByDept;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveGOADept(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelDetail.SaveJournalGroupGOAByDept((GSM04511DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelDetail.JournalGroupGOAByDept;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ServiceAfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loHeaderData = (GSM04510DTO)_conRef.R_GetCurrentData();
            var loData = (GSM04511DTO)eventArgs.Data;

            loData.CPROPERTY_ID = loHeaderData.CPROPERTY_ID;
            loData.CJRNGRP_TYPE = loHeaderData.CJRNGRP_TYPE;
            loData.CJRNGRP_CODE = loHeaderData.CJRNGRP_CODE;
            loData.CGOA_CODE = loHeaderData.CGOA_CODE;
        }
        private async Task Display(R_DisplayEventArgs eventArgs)
        {
            OnCRUDMode = eventArgs.ConductorMode == R_eConductorMode.Normal;
            await InvokeTabEventCallbackAsync(eventArgs.ConductorMode == R_eConductorMode.Normal);
        }
        #endregion


        #region LookUpGOADEPT
        //  Button LookUp DeptCode
        private void BeforeOpenLookUpDeptCode(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    eventArgs.Parameter = new GSL00700ParameterDTO();
                    eventArgs.TargetPageType = typeof(GSL00700);
                    break;
                case "GLAccount_No":
                    var loData = _conRef.R_GetCurrentData();
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSL00520ParameterDTO>(loData);
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(GSL00520);
                    break;

            }
        }

        private void AfterOpenLookUpDeptCode(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            switch (eventArgs.ColumnName)
            {
                case "CDEPT_CODE":
                    var loTempResult = (GSL00700DTO)eventArgs.Result;
                    ((GSM04511DTO)eventArgs.ColumnData).CDEPT_CODE = loTempResult.CDEPT_CODE;
                    ((GSM04511DTO)eventArgs.ColumnData).CDEPT_NAME = loTempResult.CDEPT_NAME;
                    break;
                case "GLAccount_No":
                    var loTempResult2 = (GSL00520DTO)eventArgs.Result;
                    ((GSM04511DTO)eventArgs.ColumnData).CGLACCOUNT_NO = loTempResult2.CGLACCOUNT_NO;
                    ((GSM04511DTO)eventArgs.ColumnData).CGLACCOUNT_NAME = loTempResult2.CGLACCOUNT_NAME;
                    break;
            }
        }
        #endregion

    }
}
