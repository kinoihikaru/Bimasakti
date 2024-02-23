using APT00900COMMON;
using APT00900MODEL;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Enums;

namespace APT00900FRONT
{
    public partial class APT00900 : R_Page
    {
        #region Private Property
        private APT00900ViewModel _viewModel = new APT00900ViewModel();
        private R_Grid<APT00900DisplayDTO> _gridRef;

        private R_ComboBox<APT00900PropertyDTO, string> PropertyComboBox;
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };
        private bool FileHasData = false;
        private string FileName = "";
        #endregion

        #region Inject
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_IExcel ExcelInject { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] R_ILocalizer<APT00900FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        #endregion

        #region Upload Method
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = "AP_INVOICE_UPLOAD_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", _localizer["_NotifSuccesUpload"], R_eMessageBoxButtonType.OK);
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
                #region Initialized Method
                // Set Param Company to viewmodel
                _viewModel.CompanyID = clientHelper.CompanyId;
                _viewModel.UserId = clientHelper.UserId;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;
                #endregion

                await _viewModel.GetPropertyList();
                if (_viewModel.PropertyList.Count > 0)
                {
                    APT00900PropertyDTO loParam = _viewModel.PropertyList.FirstOrDefault();
                    _viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                }
                await PropertyComboBox.FocusAsync();
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
                //file Name
                FileName = eventArgs.File.Name;

                //import from excel
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = ExcelInject;

                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "AP" });

                var loResult = R_FrontUtility.R_ConvertTo<APT00900DisplayDTO>(loDataSet.Tables[0]);

                FileHasData = loResult.Count > 0 ? true : false;

                await _gridRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<APT00900DisplayDTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);

                eventArgs.ListEntityResult = _viewModel.InvoiceUploadGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task TemplateBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_NotifBeforeProcess"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();

                    var saveFileName = "AP_INVOICE_UPLOAD.xlsx";

                    await JSRuntime.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickProcessAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_NotifBeforeProcess"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SaveBulkFile();
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

        public async Task Button_OnClickSaveExcelAsync()
        {
            //var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel?", R_eMessageBoxButtonType.YesNo);

            //if (loValidate == R_eMessageBoxResult.Yes)
            //{
            //    await ActionFuncDataSetExcel();
            //}
            await ActionFuncDataSetExcel();
        }
    }
}
