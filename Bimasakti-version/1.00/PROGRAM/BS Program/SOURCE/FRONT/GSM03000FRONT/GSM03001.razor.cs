using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM03000Common.DTOs;
using GSM03000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace GSM03000FRONT
{
    public partial class GSM03001 : R_Page
    {
        private GSM03000PrintViewModel _viewModel = new GSM03000PrintViewModel();
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        private R_ComboBox<GSM03000DTO, string> ChargesId_ComboBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000DTO>(poParameter);

                await _viewModel.GetProperty(loParam);
                await _viewModel.GetOtherChargesList(loParam);

                var loData = _viewModel.OtherChargeList.FirstOrDefault();
                _viewModel.Data.CCHARGES_ID_FROM = loData.CCHARGES_ID;
                _viewModel.Data.CCHARGES_ID_TO = loData.CCHARGES_ID;
                _viewModel.Data.CCHARGES_TYPE = loParam.CCHARGES_TYPE;

                await ChargesId_ComboBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = _viewModel.Data;

                // set Param
                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CUSER_LOGIN_ID = clientHelper.UserId;
                loParam.CPROPERTY_ID = _viewModel.Property.CPROPERTY_ID;
                loParam.CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME;

                await _reportService.GetReport(
                    "R_DefaultServiceUrl",
                    "GS",
                    "rpt/GSM03000Print/AllOtherChargesPost",
                    "rpt/GSM03000Print/AllStreamOtherChargesGet",
                    loParam);

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
