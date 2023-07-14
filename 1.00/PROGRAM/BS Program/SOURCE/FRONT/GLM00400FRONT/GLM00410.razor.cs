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
    public partial class GLM00410 : R_Page
    {
        private GLM00410ViewModel _AllocationJournalDT_viewModel = new GLM00410ViewModel();
        private R_Conductor _AllocationJournalDT_conductorRef;
        private R_Grid<GLM00411DTO> _AllocationAccount_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00400DTO)poParameter;

                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00410DTO>(loTempParam);
                loParam.CREC_ID_ALLOCATION_ID = loTempParam.CREC_ID;

                await _AllocationJournalDT_conductorRef.R_GetEntity(loParam);

                await _AllocationAccount_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void AllocationJournalDT_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

        private R_TabStrip _tabAllocationChill;

        private async Task Allocation_Account_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00411DTO>(eventArgs.Parameter);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _AllocationJournalDT_viewModel.GetAllocationAccountList(loParam);

                eventArgs.ListEntityResult = _AllocationJournalDT_viewModel.AllocationAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Allocation_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();
                if (eventArgs.TabStripTab.Id == "Account")
                {
                     await _AllocationAccount_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Allocation_Center_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00411);
        }

        private void Allocation_Period_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (GLM00410DTO)_AllocationJournalDT_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00412);
        }
    }
}
