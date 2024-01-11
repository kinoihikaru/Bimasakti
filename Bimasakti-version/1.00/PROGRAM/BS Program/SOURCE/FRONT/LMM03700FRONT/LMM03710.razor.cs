using LMM03700MODEL;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
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
using LMM03700COMMON;

namespace LMM03700FRONT
{
    public partial class LMM03710 : R_Page, R_ITabPage
    {
        private LMM03710ViewModel _viewModelTenantClass = new();//viewModel TenantClass
        private LMM03700ViewModel _viewModelTenantClassGrp = new();//viewModel TenantClass

        private R_Conductor _conTenantClassGrpRef; //conductor grid TenantClassGrp tab 2
        private R_ConductorGrid _conTenantClassRef; //conductor grid TenantClass tab 2
        private R_Grid<LMM03700DTO> _gridTenantClassGrpRef; //gridref TenantClassGrp
        private R_Grid<LMM03710DTO> _gridTenantClassRef; //gridref TenantClass 
        private R_Grid<LMM03711DTO> _gridTenantRef; //gridref Tenant 

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelTenantClassGrp.PropertyId = (string)poParameter; //getting parameter
                await _gridTenantClassGrpRef.R_RefreshGrid(null);
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
                _viewModelTenantClassGrp.PropertyId = (string)poParam;
                await _gridTenantClassGrpRef.R_RefreshGrid(null);
                if (_viewModelTenantClassGrp.TenantClassGrpGrid.Count <= 0)
                {
                    _gridTenantClassRef.DataSource.Clear();
                    _gridTenantRef.DataSource.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region TenantClassGrp
        private async Task TenantClassGrp_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelTenantClassGrp.GetTenantClassGrpList();
                eventArgs.ListEntityResult = _viewModelTenantClassGrp.TenantClassGrpGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private void TenantClassGrp_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClassGrp_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM03710DTO>(eventArgs.Data);
                await _gridTenantClassRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TenantClass
        private async Task TenantClass_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelTenantClass.GetTenantClassList((LMM03710DTO)eventArgs.Parameter);
                eventArgs.ListEntityResult = _viewModelTenantClass.TenantClassGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task TenantClass_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM03710DTO)eventArgs.Data;
                await _viewModelTenantClass.GetTenantClassList(loParam);
                eventArgs.Result = _viewModelTenantClass.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClass_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM03710DTO)eventArgs.Data;
                await _viewModelTenantClass.DeleteTenantClass(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TenantClass_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (LMM03710DTO)eventArgs.Data;
                loData.CPROPERTY_ID = _viewModelTenantClassGrp.PropertyId;
                loData.CTENANT_CLASSIFICATION_ID = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_ID) ? "" : loData.CTENANT_CLASSIFICATION_ID;
                loData.CTENANT_CLASSIFICATION_NAME = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_NAME) ? "" : loData.CTENANT_CLASSIFICATION_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task TenantClass_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (LMM03710DTO)eventArgs.Data;
                await _viewModelTenantClass.SaveTenantClass(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelTenantClass.TenantClass;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task TenantClass_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<LMM03711DTO>(eventArgs.Data);
                await _gridTenantRef.R_RefreshGrid(loEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClass_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        #endregion

        #region Tab2-TenantList
        private async Task Tenant_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelTenantClass.GetTenantClassTenantList((LMM03711DTO)eventArgs.Parameter);
                eventArgs.ListEntityResult = _viewModelTenantClass.TenantClassTenantGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Tab2-Assign Tenant
        private void R_Before_Open_Popup_AssignTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = (LMM03710DTO)_gridTenantClassRef.GetCurrentData();
            eventArgs.TargetPageType = typeof(LMM03711);
        }
        private async Task R_After_Open_Popup_AssignTenantAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = _conTenantClassRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM03711DTO>(loData);
                await _gridTenantRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Tab2-Move Tenant
        private void R_Before_Open_Popup_MoveTenant(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = (LMM03710DTO)_gridTenantClassRef.GetCurrentData();
            eventArgs.TargetPageType = typeof(LMM03712);
        }
        private async Task R_After_Open_Popup_MoveTenant(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = _conTenantClassGrpRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM03710DTO>(loData);
                await _gridTenantClassRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
