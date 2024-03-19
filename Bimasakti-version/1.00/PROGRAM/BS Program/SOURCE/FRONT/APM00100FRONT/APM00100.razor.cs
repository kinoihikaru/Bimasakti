using APM00100COMMON.DTOs.APM00100;
using APM00100MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
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
using System.Text;
using System.Threading.Tasks;

namespace APM00100FRONT
{
    public partial class APM00100 : R_Page
    {
        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_PopupService PopupService { get; set; }

        private APM00100ViewModel loAPSystemParamViewModel = new APM00100ViewModel();

        private R_Conductor _conductorAPSystemParam;

        private bool IsAllowEditGLLink = false;
        
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResult = null;
            try
            {
                await loAPSystemParamViewModel.GetSystemParamAsync();
                if (loAPSystemParamViewModel.loGetSystemParam.IRESULT == 0)
                {
                    loResult = await PopupService.Show(typeof(APM00100FRONT.APM00110), null);
                    if (loResult.Success == false || (bool)loResult.Result == false)
                    {
                        await this.CloseProgram();
                        return;
                    }
                    if ((bool)loResult.Result)
                    {
                        await R_MessageBox.Show("", "AP System Parameter Created Successfully!", R_eMessageBoxButtonType.OK);
                    }
                }
                await _conductorAPSystemParam.R_GetEntity(null);
                IsAllowEditGLLink = loAPSystemParamViewModel.loAPSystemParam.LALLOW_EDIT_GLLINK;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region AP System Parameter

        private void Grid_SavingAPSystemParam(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loAPSystemParamViewModel.APSystemParameterValidation((APM00100DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        private async Task Grid_ServiceGetRecordAPSystemParam(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            APM00100DTO loParam = null;
            try
            {
                loParam = R_FrontUtility.ConvertObjectToObject<APM00100DTO>(eventArgs.Data);
                await loAPSystemParamViewModel.GetAPSystemParamAsync(loParam);

                eventArgs.Result = loAPSystemParamViewModel.loAPSystemParam;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveAPSystemParam(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loAPSystemParamViewModel.SaveAPSystemParamAsync(
                    (APM00100DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loAPSystemParamViewModel.loAPSystemParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region OnLostFocus
        private void OnLostFocusCurrencyRateType()
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

        private void OnLostFocusTaxRateType()
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
        #endregion

        #region LOOKUP

        //CURRENCY RATE TYPE

        private void R_Before_Open_Currency_Rate_Type_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00800ParameterDTO loParam = new GSL00800ParameterDTO()
            {
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
