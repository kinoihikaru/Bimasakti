using APT00900COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APT00900MODEL
{
    public class APT00900ViewModel : R_ViewModel<APT00900DisplayDTO>, R_IProcessProgressStatus
    {
        private APT00900Model _APT00900Model = new APT00900Model();
        public List<APT00900PropertyDTO> PropertyList { get; set; } = new List<APT00900PropertyDTO>();
        public ObservableCollection<APT00900DisplayDTO> InvoiceUploadGrid { get; set; } = new ObservableCollection<APT00900DisplayDTO>();

        #region Proses Upload
        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }

        // Action Get DataSet
        public Func<Task> ActionDataSetExcel { get; set; }

        // Func Proses is Success
        public Func<Task> ActionIsCompleteSuccess { get; set; }

        // DataSet Excel 
        public DataSet ExcelDataSet { get; set; }
        #endregion

        #region Property ViewModel
        public string PropertyValueContext { get; set; } = "";
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public int SumListStaffExcel { get; set; }
        public int SumValidDataStaffExcel { get; set; }
        public int SumInvalidDataStaffExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public string CompanyID { get; set; }
        public string UserId { get; set; }
        #endregion

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00900Model.GetGSPropertyListAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<APT00900UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            APT00900UploadFileDTO loResult = null;

            try
            {
                loResult = await _APT00900Model.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task ConvertGrid(List<APT00900DisplayDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    APT00900DisplayDTO loData = new APT00900DisplayDTO
                    {
                        SEQ_NO = i + 1,
                        Department_Code = loTemp.Department_Code,
                        Reference_No = loTemp.Reference_No,
                        Reference_Date = loTemp.Reference_Date,
                        Reference_Date_Display = !string.IsNullOrWhiteSpace(loTemp.Reference_Date) ? DateTime.ParseExact(loTemp.Reference_Date, "yyyyMMdd", CultureInfo.InvariantCulture) : default,
                        Supplier_Id = loTemp.Supplier_Id,
                        Supplier_Reference_No = loTemp.Supplier_Reference_No,
                        Supplier_Reference_Date = loTemp.Supplier_Reference_Date,
                        Supplier_Reference_Date_Display = !string.IsNullOrWhiteSpace(loTemp.Supplier_Reference_Date) ? DateTime.ParseExact(loTemp.Supplier_Reference_Date, "yyyyMMdd", CultureInfo.InvariantCulture) : default,
                        Currency_Code = loTemp.Currency_Code,
                        Term_Code = loTemp.Term_Code,
                        Tax_Code = loTemp.Tax_Code,
                        Local_Currency_Base_Rate = loTemp.Local_Currency_Base_Rate,
                        Local_Currency_Rate = loTemp.Local_Currency_Rate,
                        Base_Currency_Base_Rate = loTemp.Base_Currency_Base_Rate,
                        Base_Currency_Rate = loTemp.Base_Currency_Rate,
                        Tax_Base_Rate = loTemp.Tax_Base_Rate,
                        Tax_Rate = loTemp.Tax_Rate,
                        Header_Notes = loTemp.Header_Notes,
                        Product_Department = loTemp.Product_Department,
                        Product_Type = loTemp.Product_Type,
                        Product_Id = loTemp.Product_Id,
                        Allocation_Id = loTemp.Allocation_Id,
                        Quantity = loTemp.Quantity,
                        Unit_Price = loTemp.Unit_Price,
                        Detail_Notes = loTemp.Detail_Notes,
                    };
                   
                    return loData;
                }
                ));

                // Onchange Visible Error
                VisibleError = false;
                SumValidDataStaffExcel = 0;
                SumInvalidDataStaffExcel = 0;
                SumListStaffExcel = loData.Count();

                InvoiceUploadGrid = new ObservableCollection<APT00900DisplayDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveBulkFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<APT00900DisplayDTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = PropertyValueContext });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "AP",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlAP",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = CompanyID;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.ClassName = "APT00900BACK.APT00900Cls";
                loBatchPar.BigObject = InvoiceUploadGrid.ToList();

                var lcGuid = await loCls.R_BatchProcess<List<APT00900DisplayDTO>>(loBatchPar, 55);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    await ActionIsCompleteSuccess();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            // Call Method Action Error Unhandle
            ShowErrorAction(ex);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_AP_INVOICE_UPLOADResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "AP",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlAP");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }

                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq
                    InvoiceUploadGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_NO))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_NO).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataStaffExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataStaffExcel++;
                        }
                    });

                    //Set DataSetTable and get error
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<APT00900DisplayDTO>(InvoiceUploadGrid);

                    var loDataTable = R_FrontUtility.R_ConvertTo<APT00900DisplayDTO>(loExcelData);
                    loDataTable.TableName = "AP";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    //// Dowload if get Error
                    ////await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
