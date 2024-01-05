using BlazorClientHelper;
using LMM02500COMMON;
using LMM02500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace LMM02500FRONT
{
    public partial class LMM02520 : R_Page, R_ITabPage
    {
        private LMM02520ViewModel _viewModel = new LMM02520ViewModel();
        private R_Grid<LMM02500TenantDTO> _gridRef;
        private R_Conductor _conductorRef;
        private bool _isDataExist { get; set; } = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.TenantGrp = (LMM02500DTO)poParameter;
                await _gridRef.R_RefreshGrid(null);
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
                var loData = (LMM02500DTO)poParam;
                if (!string.IsNullOrWhiteSpace(loData.CTENANT_GROUP_ID))
                {
                    _viewModel.TenantGrp = loData;
                    await _gridRef.R_RefreshGrid(null);
                }
                else
                {
                    _viewModel.TenantGrp = new LMM02500DTO();
                    _gridRef.DataSource.Clear();
                    _isDataExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Tenant Group
        private async Task Tenant_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetTenantList();

                eventArgs.ListEntityResult = _viewModel.TenantGrid;
                _isDataExist = _viewModel.TenantGrid.Count > 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Tenant_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        #endregion

        #region MovePopup
        private void R_Before_Open_Popup_Tenant_Move(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = _conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(LMM02521);
        }

        private async Task R_After_Open_Popup_Tenant_Move(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                if ((bool)eventArgs.Result == false || eventArgs.Success == false)
                {
                    return;
                }

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
