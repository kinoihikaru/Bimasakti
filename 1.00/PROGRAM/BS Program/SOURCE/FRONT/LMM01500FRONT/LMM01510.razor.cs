using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500FRONT
{
    public partial class LMM01510 : R_Page
    {
        private LMM01510ViewModel _BankAccount_viewModel = new LMM01510ViewModel();
        private R_Grid<LMM01510DTO> _BankAccount_gridRef;
        private R_Conductor _BankAccount_conductorRef;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BankAccount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccount_viewModel.GetListTemplateBankAccount();

                eventArgs.ListEntityResult = _BankAccount_viewModel.TemplateBankAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01511DTO>(eventArgs.Data);
                await _BankAccount_viewModel.GetTemplateBankAccount(loParam);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool BankAccountButtonEnable = false;
        private void BankAccount_SetAdd(R_SetEventArgs eventArgs)
        {
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);

        }
        private void BankAccount_SetEdit(R_SetEventArgs eventArgs)
        {
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private async Task BankAccount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CINVOICE_TEMPLATE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Invoice Template Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Departement Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Bank Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_ACCOUNT);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Bank Account Is Required", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;

                await _BankAccount_viewModel.SaveTemplateBankAccount(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;
                await _BankAccount_viewModel.DeleteTemplateBankAccount(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void BankAccount_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _BankAccount_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private void Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01502);
        }

        private void BankAccount_Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01200ParameterDTO()
            {
                CCB_TYPE = "B"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void BankAccount_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _BankAccount_viewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private void BankAccount_BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = _BankAccount_viewModel.TemplateBankAccount;

            var param = new GSL01300ParameterDTO()
            {
                CBANK_TYPE = "B",
                CCB_CODE = loGetData.CBANK_CODE,
                CDEPT_CODE = loGetData.CDEPT_CODE,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }

        private void BankAccount_BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;
        }
    }
}
