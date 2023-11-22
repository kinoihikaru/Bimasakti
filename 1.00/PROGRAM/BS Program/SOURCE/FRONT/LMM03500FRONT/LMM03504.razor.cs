using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
using LMM03500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500FRONT
{
    public partial class LMM03504 : R_Page, R_ITabPage
    {
        private LMM03504ViewModel loBillingViewModel = new();

        private R_Conductor _conductorBillingRef;

        private bool IsEnabled = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loBillingViewModel.loTabParameter = (TabParameterDTO)poParameter;

                await _conductorBillingRef.R_GetEntity(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Billing_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loBillingViewModel.loTabParameter = (TabParameterDTO)poParam;

            if (loBillingViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
            {
                IsEnabled = true;
                await _conductorBillingRef.R_GetEntity(null);
            }
            else
            {
                IsEnabled = false;
                loBillingViewModel.Data.Clear();
            }
        }

        private void BeforeCancelBilling(R_BeforeCancelEventArgs eventArgs)
        {
            IsEnabled = true;
        }

        private void BeforeEditBilling(R_BeforeEditEventArgs eventArgs)
        {
            IsEnabled = false;
        }

        private void AfterSaveBilling(R_AfterSaveEventArgs eventArgs)
        {
            IsEnabled = true;
        }


        private async Task GetBillingAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loBillingViewModel.GetBillingAsync();
                eventArgs.Result = loBillingViewModel.loBilling;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void SavingBilling(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loBillingViewModel.BillingValidation((LMM03504DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveBilling(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loBillingViewModel.SaveBillingAsync(
                    (LMM03504DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loBillingViewModel.loBilling;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
