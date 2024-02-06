using BlazorClientHelper;
using GSM04500COMMON;
using GSM04500MODEL;
using Lookup_GSFrontResources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500FRONT
{
    public partial class GSM04501 : R_Page
    {
        private GSM04501ViewModel _viewModel = new();
        private R_Grid<GSM04501DisplayDTO> JournalGroup_gridRef;

        #region Property Partial
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private bool FileHasData = false;
        private R_InputFile InputRef;
        #endregion

        #region Inject
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_IExcel ExcelInject { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        #endregion

        #region Init Proses
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = "Journal Group" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_UploadStatus"), R_eMessageBoxButtonType.OK);
            await this.Close(true, true);
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            this.R_DisplayException(loEx);
        }
        #endregion
        
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.Parameter = (GSM04500ParameterUploadDTO)poParameter;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;
                _viewModel.CompanyID = clientHelper.CompanyId;
                _viewModel.UserId = clientHelper.UserId;

                await InputRef.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task SourceUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //import excel from user
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var fileByte = loMS.ToArray();
                //READ EXCEL
                var loExcel = ExcelInject;
                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new[] { "JournalGroup" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM04501DisplayDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await JournalGroup_gridRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                List<GSM04501DisplayDTO> loData = (List<GSM04501DisplayDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);
                eventArgs.ListEntityResult = _viewModel.JournalGroupUploadGrid;
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
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveBulkFile();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task Button_OnClickSaveAsync()
        {
            var loEx = new R_Exception();
            try
            {
                //var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

                //if (loValidate == R_eMessageBoxResult.Yes)
                //{
                //}
                    await ActionFuncDataSetExcel();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
