using BlazorClientHelper;
using LMM03700COMMON;
using LMM03700MODEL;
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

namespace LMM03700FRONT
{
    public partial class LMM03712 : R_Page
    {
        private LMM03712ViewModel _viewModel = new LMM03712ViewModel();
        private R_Grid<LMM03711DTO> _gridRef;
        private R_Conductor _conductorRef;
        [Inject] private IClientHelper? clientHelper { get; set; }

        private bool _ProsesBtn = false;
        private LMM03711MoveTenantDTO _MoveTenantParam = new LMM03711MoveTenantDTO();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM03710DTO)poParameter;

                _MoveTenantParam = R_FrontUtility.ConvertObjectToObject<LMM03711MoveTenantDTO>(loData);
                _MoveTenantParam.CFROM_TENANT_CLASSIFICATION_ID = loData.CTENANT_CLASSIFICATION_ID;
                _MoveTenantParam.CFROM_TENANT_CLASSIFICATION_NAME = loData.CTENANT_CLASSIFICATION_NAME;

                await _viewModel.GetTenantClassList(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Tenant Class Tenant
        private async Task TenantClass_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM03711DTO>(eventArgs.Parameter);
                await _viewModel.GetTenantClassTenantGrid(loParam);

                eventArgs.ListEntityResult = _viewModel.TenantClassTenantGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ToTenantGrpId(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _MoveTenantParam.CTO_TENANT_CLASSIFICATION_ID = poParam;
                var loParam = _viewModel.TenantClassList.Where(x => x.CTENANT_CLASSIFICATION_ID == poParam).FirstOrDefault();
                _MoveTenantParam.CTO_TENANT_CLASSIFICATION_NAME = loParam.CTENANT_CLASSIFICATION_NAME;

                if (poParam == _MoveTenantParam.CFROM_TENANT_CLASSIFICATION_ID)
                {
                    await R_MessageBox.Show("", "To Tenant Classification Can't be the same From Tenant Classification",
                        R_eMessageBoxButtonType.OKCancel);
                    _gridRef.DataSource.Clear();
                    _ProsesBtn = false;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(poParam))
                {
                    await _gridRef.R_RefreshGrid(loParam);
                    _ProsesBtn = _viewModel.TenantClassTenantGrid.Count > 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }
        public async Task OnClickProcessButton()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _gridRef.DataSource.Where(x => x.LSELECTED).Select(x => x.CTENANT_CLASSIFICATION_ID).ToList();
                if (loData.Count > 0)
                {
                    var loValidate = await R_MessageBox.Show("", "Are you sure move this all selected data?", R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.Yes)
                    {
                        string idString = string.Join(",", loData);
                        _MoveTenantParam.CTENANT_LIST = idString;
                        await _viewModel.MoveTenantClass(_MoveTenantParam);

                        await this.Close(true, true);
                    }
                }
                else
                {
                    await R_MessageBox.Show("", "No data to process!!!", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
