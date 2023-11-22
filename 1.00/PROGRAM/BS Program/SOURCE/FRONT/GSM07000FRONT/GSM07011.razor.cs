using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM07000MODEL.ViewModel;
using GSM07000COMMON.DTOs;
using R_BlazorFrontEnd.Enums;
using Microsoft.AspNetCore.Components;

namespace GSM07000FRONT
{
    public partial class GSM07011 : R_Page
    {
        private GSM07011ViewModel loViewModel = new();

        private R_ConductorGrid _conGridRef;

        private R_Grid<GSM07011DTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.GSM07011_SELECTED_APPROVAL_CODE = (string)poParameter;
                await _gridRef.R_RefreshGrid(null);
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
                GSM07011DTO loParam = (GSM07011DTO)eventArgs.Data;
                await loViewModel.GetUserForMultipleUserAssigment(loParam);

                eventArgs.Result = loViewModel.loMultipleUserAssignment;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                GSM07011DTO loParam = (GSM07011DTO)eventArgs.Data;
                loViewModel.loMultipleUserAssignment = loParam;
            }
        }
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetMultipleUserAssignmentListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loMultipleUserAssignmentList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _conGridRef.R_SaveBatch();
                await this.Close(true, true);
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            await this.Close(true, false);
        }
        private async void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<GSM07011DTO>)events.Data;
            bool IsThereSelectedUser = false;
            foreach (GSM07011DTO item in loData)
            {
                if (item.LSELECTED)
                {
                    IsThereSelectedUser = true;
                    break;
                }
            }
            if (IsThereSelectedUser == false)
            {
                await this.Close(true, false);
                events.Cancel = true;
            }
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loSelectedUser = (List<GSM07011DTO>)eventArgs.Data;
                await loViewModel.SaveMultipleUserAssignmentAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {

        }
    }
}
