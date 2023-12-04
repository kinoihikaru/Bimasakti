using BlazorClientHelper;
using GSM01500COMMON.DTOs;
using GSM01500MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GSM01500FRONT
{
    public partial class UploadCenter : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private UploadCenterViewModel loUploadCenterViewModel = new UploadCenterViewModel();

        private R_ConductorGrid _conGridUploadCenterRef;

        private R_Grid<UploadCenterDTO> _gridUploadCenterRef;

        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Excel };


        private bool IsProcessEnabled = false;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_Exception poException)
        {
            this.R_DisplayException(poException);
        }

        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Upload Successfully", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loUploadCenterViewModel.GetCompanyAsync();
                loUploadCenterViewModel.SelectedUserId = loClientHelper.UserId;

                loUploadCenterViewModel.StateChangeAction = StateChangeInvoke;
                loUploadCenterViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadCenterViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        /*private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadCenterDTO)eventArgs.Data;

            if (loData.LEXIST)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
        }*/

        private async Task OnSaveToExcel()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                List<SaveCenterToExcelDTO> loEmployeList = new List<SaveCenterToExcelDTO>();

                loEmployeList = loUploadCenterViewModel.loUploadCenterDisplayList.Select(x => new SaveCenterToExcelDTO()
                {
                    No = x.INO,
                    CenterCode = x.CCENTER_CODE,
                    CenterName = x.CCENTER_NAME,
                    Active = x.LACTIVE,
                    NonActiveDate = x.NONACTIVE_DATE,
                    Valid = x.CVALID,
                    Notes = x.CNOTES
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loEmployeList);
                loDataTable.TableName = "Center";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Center.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        public void ReadExcelFile()
        {
            R_Exception loEx = new R_Exception();
            List<GetCenterFromExcelDTO> loExtract = new List<GetCenterFromExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadCenterViewModel.fileByte, new string[] { "Center" });

                var loResult = R_FrontUtility.R_ConvertTo<GetCenterFromExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<GetCenterFromExcelDTO>(loResult);

                loUploadCenterViewModel.loUploadCenterList = loExtract.Select((x, i) => new UploadCenterDTO()
                {
                    INO = i + 1,
                    CCENTER_CODE = x.CenterCode,
                    CCENTER_NAME = x.CenterName,
                    LACTIVE = x.Active,
                    NONACTIVE_DATE = x.NonActiveDate,
                    CCOMPANY_ID = loUploadCenterViewModel.loCompany.CCOMPANY_ID,
                    CNOTES = "",
                    CVALID = ""
                }).ToList();


                loUploadCenterViewModel.loUploadCenterDisplayList = new ObservableCollection<UploadCenterDTO>(loUploadCenterViewModel.loUploadCenterList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadCenterViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                MemoryStream loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadCenterViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                await _gridUploadCenterRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadCenterViewModel.IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //await loUploadCenterViewModel.ValidateUploadCenter();
                loUploadCenterViewModel.SumList = loUploadCenterViewModel.loUploadCenterDisplayList.Count();
                if (loUploadCenterViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadCenterViewModel.loUploadCenterDisplayList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _conGridUploadCenterRef.R_SaveBatch();
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnCancel()
        {
            await this.Close(true, false);
        }

        private async Task R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                var loTemp = await R_MessageBox.Show("", "Are You sure want process these records? ", R_eMessageBoxButtonType.YesNo);

                if (loTemp == R_eMessageBoxResult.No)
                {
                    events.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loUploadCenterViewModel.loUploadCenterList = (List<UploadCenterDTO>)eventArgs.Data;
                await loUploadCenterViewModel.SaveUploadCenterAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {

        }
    }
}