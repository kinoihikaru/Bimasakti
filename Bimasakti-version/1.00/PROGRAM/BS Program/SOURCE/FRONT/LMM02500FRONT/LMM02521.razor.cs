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
    public partial class LMM02521 : R_Page
    {
        private LMM02521ViewModel _viewModel = new LMM02521ViewModel();
        private R_Grid<LMM02500TenantDTO> _gridRef;
        private R_Conductor _conductorRef;
        [Inject] private IClientHelper? clientHelper { get; set; }

        private bool _ProsesBtn = false;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM02500TenantDTO)poParameter;
                _viewModel.HeaderMove = R_FrontUtility.ConvertObjectToObject<LMM02500ParameterMoveTenantDTO>(loData);
                _viewModel.HeaderMove.CFROM_TENANT_GROUP = loData.CTENANT_GROUP_ID;
                _viewModel.HeaderMove.CFROM_TENANT_GROUP_NAME = loData.CTENANT_GROUP_NAME;

                await _viewModel.GetTenantGrpList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Tenant Group
        private async Task TenantGrp_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetTenantList();

                eventArgs.ListEntityResult = _viewModel.TenantMoveGrid;
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
                _viewModel.HeaderMove.CTO_TENANT_GROUP = poParam;
                _viewModel.HeaderMove.CTO_TENANT_GROUP_NAME = _viewModel.TenantGrpComboBox.Where(x => x.CTENANT_GROUP_ID == poParam).FirstOrDefault().CTENANT_GROUP_NAME;

                if (poParam == _viewModel.HeaderMove.CFROM_TENANT_GROUP)
                {
                    await R_MessageBox.Show("", "To Tenant Group Code Can't be the same From Tenant Group Code",
                        R_eMessageBoxButtonType.OKCancel);
                    _gridRef.DataSource.Clear();
                    _ProsesBtn = false;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(poParam))
                {
                    await _gridRef.R_RefreshGrid(null);
                    _ProsesBtn = _viewModel.TenantMoveGrid.Count > 0;
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
                var loData = _gridRef.DataSource.Where(x => x.LSELECTED).ToList();
                if (loData.Count > 0)
                {
                    var loValidate = await R_MessageBox.Show("", "Are you sure move this all selected data?", R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.Yes)
                    {
                        await _viewModel.MoveTenant(loData, clientHelper.CompanyId, clientHelper.UserId);

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
