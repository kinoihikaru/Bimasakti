using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM07000MODEL.ViewModel;
using GSM07000COMMON.DTOs;
using System.Reflection.Emit;
using R_BlazorFrontEnd.Helpers;
using System.Diagnostics.Tracing;

namespace GSM07000FRONT
{
    public partial class GSM07000 : R_Page
    {
        private GSM07000ViewModel loViewModel = new();

        private R_ConductorGrid _conGridRef;

        private R_Grid<GSM07000DTO> _gridRef;

        private bool IsListExist = true;   

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetApprovalListStreamAsync();

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loActivityApproval = (GSM07000DTO)eventArgs.Data;
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetActivityApprovalListStreamAsync();
                IsListExist = loViewModel.loActivityApprovalList.Count > 0;

                eventArgs.ListEntityResult = loViewModel.loActivityApprovalList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM07000DTO loParam = (GSM07000DTO)eventArgs.Data;
                await loViewModel.GetActivityApprovalAsync(loParam);
                eventArgs.Result = loViewModel.loActivityApproval;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM07000DTO loParam = new GSM07000DTO();
                loParam = (GSM07000DTO)eventArgs.Data;
                loParam.IAPPROVAL_OPTION = int.Parse(loParam.CAPPROVAL_OPTION);

                await loViewModel.SaveActivityApprovalAsync(
                        loParam,
                        (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loActivityApproval;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loActivityApproval.CAPPROVAL_CODE;
            if (_conGridRef.R_ConductorMode == R_eConductorMode.Normal)
            {
                eventArgs.TargetPageType = typeof(GSM07010);
            }
        }

        private void R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }
    }
}