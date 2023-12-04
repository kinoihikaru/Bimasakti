    using GSM07000COMMON.DTOs;
using GSM07000MODEL.ViewModel;
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
using R_BlazorFrontEnd;

namespace GSM07000FRONT
{
    public partial class GSM07010 : R_Page
    {
        private GSM07010ViewModel loActivityApprovalUserViewModel = new();

        private R_ConductorGrid _conductorActivityApprovalUserGridRef;

        private R_Grid<GSM07010DTO> _gridActivityApprovalUserRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loActivityApprovalUserViewModel.SELECTED_APPROVAL_CODE = (string)poParameter;
                await _gridActivityApprovalUserRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_ActivityApprovalUserDisplay(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM07010DTO)eventArgs.Data;
                loActivityApprovalUserViewModel.loActivityApprovalUser = loParam;
            }
        }

        private async Task Grid_R_ServiceGetActivityApprovalUserListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loActivityApprovalUserViewModel.GetActivityApprovalListStreamAsync();

                eventArgs.ListEntityResult = loActivityApprovalUserViewModel.loActivityApprovalUserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetActivityApprovalUserRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {

                GSM07010DTO loParam = (GSM07010DTO)eventArgs.Data;
                await loActivityApprovalUserViewModel.GetActivityApprovalUserAsync(loParam);

                eventArgs.Result = loActivityApprovalUserViewModel.loActivityApprovalUser;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceActivityApprovalUserSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loActivityApprovalUserViewModel.SaveActivityApprovalUserAsync(
                    (GSM07010DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loActivityApprovalUserViewModel.loActivityApprovalUser;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceActivityApprovalUserDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM07010DTO loData = (GSM07010DTO)eventArgs.Data;
                await loActivityApprovalUserViewModel.DeleteAssignedUser(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = loActivityApprovalUserViewModel.SELECTED_APPROVAL_CODE;
            eventArgs.TargetPageType = typeof(GSM07011);
        }

        private async Task R_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _gridActivityApprovalUserRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Grid_BeforelookUpUser(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            eventArgs.Parameter = loActivityApprovalUserViewModel.SELECTED_APPROVAL_CODE;
            eventArgs.TargetPageType = typeof(LookUpUser);
        }

        private void Grid_AfterlookUpUser(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSM07010UserDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM07010DTO)eventArgs.ColumnData;
            loGetData.CAPPROVAL_USER = loTempResult.CUSER_ID;
            loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
        }
    }
}
