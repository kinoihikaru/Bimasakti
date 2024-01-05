using BlazorClientHelper;
using LMM02500COMMON;
using LMM02500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace LMM02500FRONT
{
    public partial class LMM02510 : R_Page, R_ITabPage
    {
        private R_Conductor _conductorRef;
        private LMM02510ViewModel _viewModel = new();

        private R_TabStrip _tabStripRef;

        [Inject] private IClientHelper? clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<LMM02510DTO>(poParameter);
                _viewModel.PropertyId = loData.CPROPERTY_ID;
                if (!string.IsNullOrWhiteSpace(loData.CTENANT_GROUP_ID))
                {
                    await _conductorRef.R_GetEntity(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Tenant Grp
        private async Task ServiceGetRecordLMM02510(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetTenantGrp((LMM02510DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.TenantGrp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDeleteGSM06010(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM02510DTO)eventArgs.Data;

                await _viewModel.DeleteTenantGrp(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private Task R_AfterAddLMM02510(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM02510DTO)eventArgs.Data;
                _viewModel.ExpiredDate = DateTime.Now;
                loParam.CPROPERTY_ID = _viewModel.PropertyId;
                loParam.CCREATE_BY = clientHelper.UserId;
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.CUPDATE_BY = clientHelper.UserId;
                loParam.DUPDATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return Task.CompletedTask;
        }

        public async Task R_AfterDeleteLMM02510()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task R_ValidationLMM02510(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (LMM02510DTO)eventArgs.Data;

                await _viewModel.ValidationTenantGrp(loData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;

            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveLMM02510(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveTenantGrp((LMM02510DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.TenantGrp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ProfileAndTaxInfo_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        #endregion

        #region Tab Page
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<LMM02510DTO>(poParam);
                _viewModel.PropertyId = loData.CPROPERTY_ID;
                if (!string.IsNullOrWhiteSpace(loData.CTENANT_GROUP_ID))
                {
                    await _conductorRef.R_GetEntity(loData);
                }
                else
                {
                    await _conductorRef.R_SetCurrentData(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        private async Task OnClickNextButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _tabStripRef.SetActiveTabAsync("TaxInfo");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnChanged_LUSE_GROUP_TAX_LMMFull02500(bool poParam)
        {
            var loException = new R_Exception();

            try
            {
                _viewModel.Data.LUSE_GROUP_TAX = poParam;
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    if (poParam)
                    {
                        var llTrue = await R_MessageBox.Show("", "All tenants tax info will overwrite with this tenant group tax info!",
                        R_eMessageBoxButtonType.OKCancel);
                    }
                    else
                    {
                        var llFalse = await R_MessageBox.Show("",
                        "Please check all tenants tax info under this tenant group!",
                        R_eMessageBoxButtonType.OKCancel);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
