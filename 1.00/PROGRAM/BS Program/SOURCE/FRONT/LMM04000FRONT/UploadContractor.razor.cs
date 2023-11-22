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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM04000MODEL.ViewModel;
using BlazorClientHelper;
using LMM04000COMMON.DTOs.LMM04010;
using System.Collections.ObjectModel;

namespace LMM04000FRONT
{
    public partial class UploadContractor : R_Page
    {
        [Inject] public R_IExcel Excel { get; set; }
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private UploadContractorViewModel loUploadContractorViewModel = new UploadContractorViewModel();

        private R_ConductorGrid _conGridUploadContractorRef;

        private R_Grid<UploadContractorDTO> _gridUploadContractorRef;

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
                loUploadContractorViewModel.loParameter = (UploadContractorParameterDTO)poParameter;
                loUploadContractorViewModel.SelectedUserId = loClientHelper.UserId;
                loUploadContractorViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loUploadContractorViewModel.StateChangeAction = StateChangeInvoke;
                loUploadContractorViewModel.ShowErrorAction = ShowErrorInvoke;
                loUploadContractorViewModel.ShowSuccessAction = async () =>
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

        private async Task OnSaveToExcel()
        {
            var loEx = new R_Exception();

            try
            {
                List<SaveContractorToExcelDTO> loExcelList = new List<SaveContractorToExcelDTO>();

                loExcelList = loUploadContractorViewModel.loUploadContractorDisplayList.Select(x => new SaveContractorToExcelDTO()
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
                loDataTable.TableName = "Contractor";

                //export to excel
                var loByteFile = Excel.R_WriteToExcel(loDataTable);
                var saveFileName = $"Upload Error Contractor.xlsx";

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
            List<UploadContractorExcelDTO> loExtract = new List<UploadContractorExcelDTO>();
            try
            {
                var loDataSet = Excel.R_ReadFromExcel(loUploadContractorViewModel.fileByte, new string[] { "Contractor" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadContractorExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadContractorExcelDTO>(loResult);

                loUploadContractorViewModel.loUploadContractorList = loExtract.Select((x, i) => new UploadContractorDTO
                {
                    NO = i + 1,
                    TenantId = x.ContractorId,
                    TenantName = x.ContractorName,
                    Address = x.Address,
                    City = x.City,
                    Province = x.Province,
                    Country = x.Country,
                    ZipCode = x.ZipCode,
                    Email = x.Email,
                    PhoneNo1 = x.PhoneNo1,
                    PhoneNo2 = x.PhoneNo2,
                    TenantGroup = x.ContractorGroup,
                    TenantCategory = x.ContractorCategory,
                    TenantType = "",
                    JournalGroup = x.JournalGroup,
                    PaymentTerm = x.PaymentTerm,
                    Currency = x.Currency,
                    Salesman = "",
                    LineOfBusiness = x.LineOfBusiness,
                    FamilyCard = "",
                    Notes = "",
                    CompanyId = loUploadContractorViewModel.SelectedCompanyId,
                    PropertyId = loUploadContractorViewModel.loParameter.PropertyData.CPROPERTY_ID,
                }).ToList();

                loUploadContractorViewModel.loUploadContractorDisplayList = new ObservableCollection<UploadContractorDTO>(loUploadContractorViewModel.loUploadContractorList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                loUploadContractorViewModel.IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loUploadContractorViewModel.fileByte = loMS.ToArray();

                ReadExcelFile();

                if (eventArgs.File.Name.Contains(".xlsx") == false)
                {
                    await R_MessageBox.Show("", "File Type must Microsoft Excel .xlsx", R_eMessageBoxButtonType.OK);
                }
                await _gridUploadContractorRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                if (loUploadContractorViewModel.IsErrorEmptyFile)
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
                //await loUploadContractorViewModel.ValidateUploadContractor();
                loUploadContractorViewModel.SumList = loUploadContractorViewModel.loUploadContractorDisplayList.Count();
                if (loUploadContractorViewModel.SumList > 0)
                {
                    IsProcessEnabled = true;
                }
                else
                {
                    IsProcessEnabled = false;
                }
                eventArgs.ListEntityResult = loUploadContractorViewModel.loUploadContractorDisplayList;
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
                await _conGridUploadContractorRef.R_SaveBatch();
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
                loUploadContractorViewModel.loUploadContractorList = (List<UploadContractorDTO>)eventArgs.Data;
                await loUploadContractorViewModel.SaveUploadContractorAsync();
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