using BlazorClientHelper;
using LMM06500COMMON;
using LMM06500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System.Collections.ObjectModel;

namespace LMM06500FRONT
{
    public partial class LMM06501 : R_Page
    {
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private LMM06501ViewModel _viewModel = new LMM06501ViewModel();
        private R_Grid<LMM06501ErrorValidateDTO> _StaffMoveDetail_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var Param = (LMM06500DTOInitial)poParameter;
                _viewModel.PropertyValue = Param.CPROPERTY_ID;
                _viewModel.PropertyName = Param.CPROPERTY_NAME;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task _Staff_SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                //import from excel
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                await _StaffMoveDetail_gridRef.R_RefreshGrid(loFileByte);
                if (_viewModel.VisibleError)
                {
                    await R_MessageBox.Show("", "Error Validate Data", R_eMessageBoxButtonType.OK);
                }
                await _StaffMoveDetail_gridRef.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task _Staff_Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.fileByte = (byte[])eventArgs.Parameter;
                await _viewModel.AttachFile((byte[])eventArgs.Parameter, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.ListEntityResult = _viewModel.StaffValidateUploadError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (LMM06501ErrorValidateDTO)eventArgs.Data;

            if (loData.Var_Exists)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            var loData = new LMM06502DTO();
            var loDetailData = new List<LMM06502DetailDTO>();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                   await _viewModel.SaveBulkFile(clientHelper.CompanyId, clientHelper.UserId);

                   await this.Close(true, true);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }


    }
}
