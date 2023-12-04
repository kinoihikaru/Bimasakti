using APM00100COMMON.DTOs.APM00100;
using APM00100MODEL.ViewModel;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Lookup_GSCOMMON.DTOs;
using System.Diagnostics.Tracing;

namespace APM00100FRONT
{
    public partial class APM00110 : R_Page
    {
        [Inject] IClientHelper _clientHelper { get; set; }

        private APM00100ViewModel loAPSystemParamViewModel = new APM00100ViewModel();

        private R_Conductor _conductorAPSystemParam;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region AP System Parameter

        private async Task Grid_ServiceGetRecordAPSystemParam(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loAPSystemParamViewModel.loAPSystemParam = new APM00100DTO();
                eventArgs.Result = loAPSystemParamViewModel.loAPSystemParam;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancelAsync()
        {
            await this.Close(true, false);
        }

        private async Task OnClickCreateAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loAPSystemParamViewModel.APSystemParameterValidation(loAPSystemParamViewModel.Data);
                await loAPSystemParamViewModel.SaveAPSystemParamAsync(
                loAPSystemParamViewModel.Data,
                    eCRUDMode.AddMode);
                if (loEx.HasError == false)
                {
                    await this.Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region LOOKUP 

        //CURRENCY RATE TYPE

        private void R_Before_Open_Currency_Rate_Type_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00800ParameterDTO loParam = new GSL00800ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_GSFRONT.GSL00800);
        }

        private void R_After_Open_Currency_Rate_Type_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00800DTO loTempResult = (GSL00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00100DTO)_conductorAPSystemParam.R_GetCurrentData();
            loGetData.CCUR_RATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            loGetData.CCUR_RATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }

        //TAX RATE TYPE

        private void R_Before_Open_Tax_Rate_Type_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00800ParameterDTO loParam = new GSL00800ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_GSFRONT.GSL00800);
        }


        private void R_After_Open_Tax_Rate_Type_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00800DTO loTempResult = (GSL00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00100DTO)_conductorAPSystemParam.R_GetCurrentData();
            loGetData.CTAX_RATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            loGetData.CTAX_RATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }

        //DEPARTMENT

        private void R_Before_Open_Department_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_GSFRONT.GSL00700);
        }

        private void R_After_Open_Department_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00700DTO loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00100DTO)_conductorAPSystemParam.R_GetCurrentData();
            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion
    }
}