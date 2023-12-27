using APT00300COMMON;
using APT00300MODEL;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00300FRONT
{
    public partial class APT00330 : R_Page
    {
        private APT00330ViewModel _viewModel = new APT00330ViewModel();
        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllInitialVar();

                var loParam = (APT00310DTO)poParameter;
                _viewModel.HeaderData = loParam;
                _viewModel.RefDate = DateTime.ParseExact(loParam.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                await _conductorRef.R_GetEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Summary_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

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
        private async Task Summary_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.SaveSummaryPurchaseDebit((APT00310DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.HeaderData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region PopUp
        private void Before_Open_Deduction_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //eventArgs.Parameter = new AdditionalParameterDTO()
            //{
            //    CADDITIONAL_TYPE = "D",
            //    CREC_ID = _viewModel.loHeader.CREC_ID
            //};
            //eventArgs.TargetPageType = typeof(APT00131);
        }

        private void After_Open_Deduction_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            //if (eventArgs.Success == false)
            //{
            //    return;
            //}
            //_viewModel.Data.NDEDUCTION = (decimal)eventArgs.Result;
            //CalculateProcess();
        }

        private void Before_Open_Addition_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //eventArgs.Parameter = new AdditionalParameterDTO()
            //{
            //    CADDITIONAL_TYPE = "A",
            //    CREC_ID = _viewModel.loHeader.CREC_ID
            //};
            //eventArgs.TargetPageType = typeof(APT00131);
        }

        private void After_Open_Addition_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            //if (eventArgs.Success == false)
            //{
            //    return;
            //}
            //_viewModel.Data.NADDITION = (decimal)eventArgs.Result;
            //CalculateProcess();
        }

        #endregion

    
    }
}
