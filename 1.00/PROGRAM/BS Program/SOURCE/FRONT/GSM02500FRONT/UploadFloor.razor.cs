using BlazorClientHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500MODEL.View_Model;
using System.Collections.ObjectModel;

namespace GSM02500FRONT
{
    public partial class UploadFloor : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private UploadFloorViewModel loUploadFloorViewModel = new UploadFloorViewModel();

        private R_ConductorGrid _conGridUploadFloorRef;

        private R_Grid<UploadFloorDTO> _gridUploadFloorRef;

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
            var loEx = new R_Exception();

            try
            {
                loUploadFloorViewModel.loParameter = (UploadFloorParameterDTO)poParameter;
                loUploadFloorViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadFloorViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadFloorViewModel.StateChangeAction = StateChangeInvoke;
                loUploadFloorViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadFloorViewModel.ShowSuccessAction = async () =>
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

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<UploadFloorExcelDTO> loExtract = new List<UploadFloorExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadFloorViewModel.fileByte, new string[] { "Floor" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadFloorExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadFloorExcelDTO>(loResult);

                loUploadFloorViewModel.loUploadFloorList = loExtract.Select((x, i) => new UploadFloorDTO
                {
                    No = i + 1,
                    FloorCode = x.FloorCode,
                    FloorName = x.FloorName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitType = x.UnitType,
                    UnitCategory = x.UnitCategory,
                    Notes = "",
                    CompanyId = loUploadFloorViewModel.SelectedCompanyId,
                    PropertyId = loUploadFloorViewModel.loParameter.PropertyData.CPROPERTY_ID,
                    BuildingId = loUploadFloorViewModel.loParameter.BuildingData.CBUILDING_ID
                }).ToList();

                loUploadFloorViewModel.loUploadFloorDisplayList = new ObservableCollection<UploadFloorDTO>(loUploadFloorViewModel.loUploadFloorList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadFloorViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }
/*
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadFloorDTO)eventArgs.Data;

            if (loData.Var_Exists)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    FontColor = "red"
                };
            }
        }*/

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveFloorToExcelDTO> loExcelList = new List<SaveFloorToExcelDTO>();

                loExcelList = loUploadFloorViewModel.loUploadFloorDisplayList.Select(x => new SaveFloorToExcelDTO()
                {
                    No = x.No,
                    FloorCode = x.FloorCode,
                    FloorName = x.FloorName,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    UnitType = x.UnitType,
                    UnitCategory = x.UnitCategory,
                    Valid = x.Valid,
                    Notes = x.Notes,
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "Floor";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Floor.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadFloorViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                await _gridUploadFloorRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadFloorViewModel.IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //await loUploadFloorViewModel.ValidateUploadFloor();

                loUploadFloorViewModel.SumList = loUploadFloorViewModel.loUploadFloorDisplayList.Count();
                if (loUploadFloorViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }

                eventArgs.ListEntityResult = loUploadFloorViewModel.loUploadFloorDisplayList;
                loUploadFloorViewModel.IsUploadSuccesful = !loUploadFloorViewModel.VisibleError;
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
                await _conGridUploadFloorRef.R_SaveBatch();
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
            var loEx = new R_Exception();
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
            var loEx = new R_Exception();

            try
            {
                loUploadFloorViewModel.loUploadFloorList = (List<UploadFloorDTO>)eventArgs.Data;
                await loUploadFloorViewModel.SaveUploadFloorAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}