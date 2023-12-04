 using BlazorClientHelper;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500MODEL.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500FRONT
{
    public partial class UploadTenant : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private UploadTenantViewModel loUploadTenantViewModel = new UploadTenantViewModel();

        private R_ConductorGrid _conGridUploadTenantRef;

        private R_Grid<UploadTenantDTO> _gridUploadTenantRef;

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
                loUploadTenantViewModel.loParameter = (UploadTenantParameterDTO)poParameter;
                loUploadTenantViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadTenantViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadTenantViewModel.StateChangeAction = StateChangeInvoke;
                loUploadTenantViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadTenantViewModel.ShowSuccessAction = async () =>
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
/*
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (UploadTenantDTO)eventArgs.Data;

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
            R_Exception loEx = new R_Exception();

            try
            {
                List<SaveTenantToExcelDTO> loExcelList = new List<SaveTenantToExcelDTO>();

                loExcelList = loUploadTenantViewModel.loUploadTenantDisplayList.Select(x => new SaveTenantToExcelDTO()
                {
                    No = x.NO,
                    TenantId = x.TenantId,
                    TenantName = x.TenantName,
                    Address = x.Address,
                    City = x.City,
                    Province = x.Province,
                    Country = x.Country,
                    ZipCode = x.ZipCode,
                    Email = x.Email,
                    PhoneNo1 = x.PhoneNo1,
                    PhoneNo2 = x.PhoneNo2,
                    TenantGroup = x.TenantGroup,
                    TenantCategory = x.TenantCategory,
                    TenantType = x.TenantType,
                    JournalGroup = x.JournalGroup,
                    PaymentTerm = x.PaymentTerm,
                    Currency = x.Currency,
                    Salesman = x.Salesman,
                    LineOfBusiness = x.LineOfBusiness,
                    FamilyCard = x.FamilyCard,
                    Valid = x.Valid,
                    Notes = x.Notes
                }).ToList();

                var loDataTable = R_FrontUtility.R_ConvertTo(loExcelList);
                loDataTable.TableName = "Tenant";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Tenant.xlsx";

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
            List<UploadTenantExcelDTO> loExtract = new List<UploadTenantExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadTenantViewModel.fileByte, new string[] { "Tenant" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadTenantExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadTenantExcelDTO>(loResult);

                loUploadTenantViewModel.loUploadTenantList = loExtract.Select((x, i) => new UploadTenantDTO
                {
                    NO = i + 1,
                    TenantId = x.TenantId,
                    TenantName = x.TenantName,
                    Address = x.Address,
                    City = x.City,
                    Province = x.Province,
                    Country = x.Country,
                    ZipCode = x.ZipCode,
                    Email = x.Email,
                    PhoneNo1 = x.PhoneNo1,
                    PhoneNo2 = x.PhoneNo2,
                    TenantGroup = x.TenantGroup,
                    TenantCategory = x.TenantCategory,
                    TenantType = x.TenantType,
                    JournalGroup = x.JournalGroup,
                    PaymentTerm = x.PaymentTerm,
                    Currency = x.Currency,
                    Salesman = x.Salesman,
                    LineOfBusiness = x.LineOfBusiness,
                    FamilyCard = x.FamilyCard,
                    Notes = "",
                    CompanyId = loUploadTenantViewModel.SelectedCompanyId,
                    PropertyId = loUploadTenantViewModel.loParameter.PropertyData.CPROPERTY_ID,
                }).ToList();

                loUploadTenantViewModel.loUploadTenantDisplayList = new ObservableCollection<UploadTenantDTO>(loUploadTenantViewModel.loUploadTenantList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadTenantViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadTenantViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                await _gridUploadTenantRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadTenantViewModel.IsErrorEmptyFile)
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
                //await loUploadTenantViewModel.ValidateUploadTenant();
                loUploadTenantViewModel.SumList = loUploadTenantViewModel.loUploadTenantDisplayList.Count();
                if (loUploadTenantViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadTenantViewModel.loUploadTenantDisplayList;
                loUploadTenantViewModel.IsUploadSuccesful = !loUploadTenantViewModel.VisibleError;
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
                await _conGridUploadTenantRef.R_SaveBatch();
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
                loUploadTenantViewModel.loUploadTenantList = (List<UploadTenantDTO>)eventArgs.Data;
                await loUploadTenantViewModel.SaveUploadTenantAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

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