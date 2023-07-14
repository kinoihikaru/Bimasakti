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
    public partial class GLM00411 : R_Page
    {
        private GLM00411ViewModel _AllocationTargetCenter_viewModel = new GLM00411ViewModel();
        private R_Grid<GLM00412DTO> _AllocationCenter_gridRef;
        private R_Grid<GLM00413DTO> _AllocationCenterPeriod_gridRef;
        private R_Conductor _AllocationCenter_condutorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00412DTO>(poParameter);

                await _AllocationCenter_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Center_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00412DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _AllocationTargetCenter_viewModel.GetAllocationCenterList(loParam);

                eventArgs.ListEntityResult = _AllocationTargetCenter_viewModel.AllocationCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_Center_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

        private async Task Allocation_Center_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00412DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = new GLM00413DTO()
                    {
                        CREC_ID_CENTER_ID = loTempParam.CREC_ID
                    };
                    await _AllocationCenterPeriod_gridRef.R_RefreshGrid(loParam);
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
                var loParam = (GLM00413DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                loParam.CYEAR = _AllocationTargetCenter_viewModel.Year.ToString();

                await _AllocationTargetCenter_viewModel.GetAllocationCenterPeriodList(loParam);

                eventArgs.ListEntityResult = _AllocationTargetCenter_viewModel.AllocationCenterPeriodGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task _AllocationTargetCenter_Refresh_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = _AllocationCenter_gridRef.CurrentSelectedData;
                var loParam = new GLM00413DTO()
                {
                    CREC_ID_CENTER_ID = loTempParam.CREC_ID
                };
                await _AllocationCenterPeriod_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
