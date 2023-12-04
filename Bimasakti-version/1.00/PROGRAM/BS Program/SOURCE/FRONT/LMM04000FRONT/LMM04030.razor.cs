using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04030;
using LMM04000MODEL.ViewModel;
using LMM04000FRONT;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMM04000FRONT
{
    public partial class LMM04030 : R_Page, R_ITabPage
    {
        private LMM04030ViewModel loBankInfoViewModel = new LMM04030ViewModel();

        private R_ConductorGrid _conductorBankInfoRef;

        private R_Grid<LMM04030DTO> _gridBankInfoRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loBankInfoViewModel.loTabParameter = (TabParameterDTO)poParameter;
                await loBankInfoViewModel.GetTenantAsync();
                await _gridBankInfoRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Bank_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loBankInfoViewModel.loTabParameter = (TabParameterDTO)poParam;

            if (loBankInfoViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
            {
                await loBankInfoViewModel.GetBankInfoListStreamAsync();
            }
            else
            {
                loBankInfoViewModel.loBankInfoList.Clear();
            }
        }

        private async Task Grid_BankInfo_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loBankInfoViewModel.GetBankInfoListStreamAsync();
                eventArgs.ListEntityResult = loBankInfoViewModel.loBankInfoList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_BankInfo_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                LMM04030DTO loParam = (LMM04030DTO)eventArgs.Data;
                await loBankInfoViewModel.GetBankInfoAsync(loParam);
                eventArgs.Result = loBankInfoViewModel.loBankInfo;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_BankInfo_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loBankInfoViewModel.SaveBankInfoAsync(
                    (LMM04030DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loBankInfoViewModel.loBankInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_BankInfo_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM04030DTO loData = (LMM04030DTO)eventArgs.Data;
                await loBankInfoViewModel.DeleteBankInfoAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_BankInfo_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loBankInfoViewModel.loBankInfo = (LMM04030DTO)eventArgs.Data;
            }
        }

        private void Grid_BeforelookUp(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(LMM04030DTO.CBANK_CODE))
            {
                eventArgs.TargetPageType = typeof(LookUpBankCode);
            }
            else if (eventArgs.ColumnName == nameof(LMM04030DTO.CCURRENCY_CODE))
            {
                eventArgs.TargetPageType = typeof(LookUpCurrency);
            }
        }

        private void Grid_AfterlookUp(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(LMM04030DTO.CBANK_CODE))
            {
                var loTempResult = (GetBankCodeDTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loGetData = (LMM04030DTO)eventArgs.ColumnData;
                loGetData.CBANK_CODE = loTempResult.CCB_CODE;
                loGetData.CBANK_NAME = loTempResult.CCB_NAME;
            }
            else if (eventArgs.ColumnName == nameof(LMM04030DTO.CCURRENCY_CODE))
            {
                var loTempResult = (GetCurrencyDTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loGetData = (LMM04030DTO)eventArgs.ColumnData;
                loGetData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
        }
    }
}
