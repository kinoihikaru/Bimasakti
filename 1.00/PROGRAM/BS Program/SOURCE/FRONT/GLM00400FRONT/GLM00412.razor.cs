using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400FRONT
{
    public partial class GLM00412 : R_Page
    {
        private GLM00412ViewModel _AllocationPeriod_viewModel = new GLM00412ViewModel();
        private R_Grid<GLM00414DTO> _AllocationPeriod_gridRef;
        private R_Grid<GLM00415DTO> _AllocationPeriodCenter_gridRef;
        private R_Conductor _AllocationPeriod_condutorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00410DTO)poParameter;
                _AllocationPeriod_viewModel.AllocationId = loTempParam.CREC_ID_ALLOCATION_ID;
                var loParam = new GLM00414DTO();

                await _AllocationPeriod_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Period_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00414DTO)eventArgs.Parameter;
                loParam.CCYEAR = _AllocationPeriod_viewModel.Year.ToString();

                await _AllocationPeriod_viewModel.GetAllocationPeriodList(loParam);

                eventArgs.ListEntityResult = _AllocationPeriod_viewModel.AllocationPeriodGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_Period_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

            R_DisplayException(loEx);
        }

        private void Allocation_Period_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLM00415DTO>(eventArgs.Data);
                    _AllocationPeriodCenter_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_CenterPeriod_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00415DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                loParam.CYEAR = _AllocationPeriod_viewModel.Year.ToString();
                loParam.CREC_ID_ALLOCATION_ID = _AllocationPeriod_viewModel.AllocationId;

                await _AllocationPeriod_viewModel.GetAllocationPeriodCenterList(loParam);

                eventArgs.ListEntityResult = _AllocationPeriod_viewModel.AllocationPeriodCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationPeriod_Refresh_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00414DTO();

                await _AllocationPeriod_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
