using BlazorClientHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APM00200MODEL.ViewModel;
using APM00200COMMON.DTOs.APM00200;

namespace APM00200FRONT
{
    public partial class UploadExpenditure : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private UploadExpenditureViewModel loUploadExpenditureViewModel = new UploadExpenditureViewModel();

        private R_ConductorGrid _conGridUploadExpenditureRef;

        private R_Grid<UploadExpenditureDTO> _gridUploadExpenditureRef;

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
                loUploadExpenditureViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadExpenditureViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadExpenditureViewModel.StateChangeAction = StateChangeInvoke;
                loUploadExpenditureViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadExpenditureViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };

                await loUploadExpenditureViewModel.GetPropertyListStreamAsync();
                if (loUploadExpenditureViewModel.loPropertyList.Count > 0)
                {
                    loUploadExpenditureViewModel.loProperty = loUploadExpenditureViewModel.loPropertyList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnSaveToExcel()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                List<SaveExpenditureToExcelDTO> loSaveExpenditureToExcel = new List<SaveExpenditureToExcelDTO>();

                loSaveExpenditureToExcel = loUploadExpenditureViewModel.loUploadExpenditureDisplayList.Select(x => new SaveExpenditureToExcelDTO()
                {
                    No = x.No,
                    Expenditure_Id = x.Expenditure_Id,
                    Expenditure_Name = x.Expenditure_Name,
                    Expenditure_Description = x.Expenditure_Description,
                    Journal_Group_Id = x.Journal_Group_Id,
                    Category_Id = x.Category_Id,
                    Unit = x.Unit,
                    Taxable = x.Taxable,
                    Tax_ID = x.Tax_ID,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loSaveExpenditureToExcel);
                loDataTable.TableName = "Expenditure";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Expenditure.xlsx";

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
            List<UploadExpenditureExcelDTO> loExtract = new List<UploadExpenditureExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadExpenditureViewModel.fileByte, new string[] { "Sheet1" });
                var loResult = R_FrontUtility.R_ConvertTo<UploadExpenditureExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadExpenditureExcelDTO>(loResult);

                loUploadExpenditureViewModel.loUploadExpenditureList = loExtract.Select((x, i) => new UploadExpenditureDTO()
                {
                    No = i + 1,
                    Expenditure_Id = x.Expenditure_Id,
                    Expenditure_Name = x.Expenditure_Name,
                    Expenditure_Description = x.Expenditure_Description,
                    Journal_Group_Id = x.Journal_Group_Id,
                    Category_Id = x.Category_Id,
                    Unit = x.Unit,
                    Taxable = x.Taxable,
                    Tax_ID = x.Tax_ID
                }).ToList();


                loUploadExpenditureViewModel.loUploadExpenditureDisplayList = new ObservableCollection<UploadExpenditureDTO>(loUploadExpenditureViewModel.loUploadExpenditureList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadExpenditureViewModel.IsErrorEmptyFile = MatchingError != null;
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
                loUploadExpenditureViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                await _gridUploadExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadExpenditureViewModel.IsErrorEmptyFile)
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
                loUploadExpenditureViewModel.SumList = loUploadExpenditureViewModel.loUploadExpenditureDisplayList.Count();
                if (loUploadExpenditureViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadExpenditureViewModel.loUploadExpenditureDisplayList;
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
                await _conGridUploadExpenditureRef.R_SaveBatch();
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
                loUploadExpenditureViewModel.loUploadExpenditureList = (List<UploadExpenditureDTO>)eventArgs.Data;
                await loUploadExpenditureViewModel.SaveUploadExpenditureAsync();
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