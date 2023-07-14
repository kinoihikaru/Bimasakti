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
    public partial class GLM00400 : R_Page
    {
        private GLM00400ViewModel _AllocationJournalHD_viewModel = new GLM00400ViewModel();
        private R_Grid<GLM00400DTO> _AllocationJournalHD_gridRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await AllocationJournal_InitialVar_ServiceGetListRecord(null);
                await AllocationJournal_SystemParam_ServiceGetListRecord(null);

                await _AllocationJournalHD_gridRef.R_RefreshGrid(null);

                await _AllocationJournalHD_gridRef.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournal_InitialVar_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400InitialDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetInitialVar(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournal_SystemParam_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400GLSystemParamDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task AllocationJournal_HD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400DTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetAllocationJournalHDList(loParam);

                eventArgs.ListEntityResult = _AllocationJournalHD_viewModel.AllocationJournalHDGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ResfreshGrid_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalHD_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void AllocationEntry_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            var loData = _AllocationJournalHD_gridRef.CurrentSelectedData;
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(GLM00410);
        }

    }
}
