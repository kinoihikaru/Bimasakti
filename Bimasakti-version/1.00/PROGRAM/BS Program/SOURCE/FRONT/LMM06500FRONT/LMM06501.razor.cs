using BlazorClientHelper;
using LMM06500COMMON;
using LMM06500FrontResources;
using LMM06500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Data;

namespace LMM06500FRONT
{
    public partial class LMM06501 : R_Page
    {
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };

        private LMM06501ViewModel _viewModel = new LMM06501ViewModel();
        private R_Grid<LMM06501ErrorValidateDTO> _StaffMoveDetail_gridRef;

        private bool FileHasData = false;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_IExcel ExcelInject { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        // Create Method Action For Download Excel if Has Error
        private async Task ActionFuncDataSetExcel()
        {
            var loByte = ExcelInject.R_WriteToExcel(_viewModel.ExcelDataSet);
            var lcName = string.Format("Staff {0}" + ".xlsx", _viewModel.PropertyValue);

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }

        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifSuccessUpload"), R_eMessageBoxButtonType.OK);
            await this.Close(true, true);
        }

        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            this.R_DisplayException(loEx);
        }

        private R_InputFile InputFileUpload;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var Param = (LMM06500DTOInitial)poParameter;
                _viewModel.PropertyValue = Param.CPROPERTY_ID;
                _viewModel.PropertyName = Param.CPROPERTY_NAME;

                //Assign Action
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.ShowErrorAction = ShowErrorInvoke;
                _viewModel.ActionDataSetExcel = ActionFuncDataSetExcel;
                _viewModel.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;

                //Asign Company and User id
                _viewModel.CompanyID = clientHelper.CompanyId;
                _viewModel.UserId = clientHelper.UserId;

                await InputFileUpload.FocusAsync();
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
                _viewModel.SourceFileName = eventArgs.File.Name;
                //import from excel
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = ExcelInject;

                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Staff"});

                var loResult = R_FrontUtility.R_ConvertTo<LMM06501DTO>(loDataSet.Tables[0]);

                var loValidateActive = loResult.Any(x => int.Parse(x.Active) < 0 || int.Parse(x.Active) > 1);
                if (loValidateActive)
                {
                    loEx.Add("", "Invalid to Convert Int to Bool");
                    goto EndBlock;
                }

                FileHasData = loResult.Count > 0 ? true : false;

                await _StaffMoveDetail_gridRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            R_DisplayException(loEx);
        }

        private async Task _Staff_Upload_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (List<LMM06501DTO>)eventArgs.Parameter;

                await _viewModel.ConvertGrid(loData);

                eventArgs.ListEntityResult = _viewModel.StaffValidateUploadError;
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
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifImportData"), R_eMessageBoxButtonType.YesNo);

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
