using GSM02200COMMON;
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

namespace GSM02200MODEL
{
    public class GSM02201ViewModel : R_IProcessProgressStatus
    {
        private GSM02200Model _GSM02200Model = new GSM02200Model();

        // Grid Display Staff Upload
        public ObservableCollection<GSM02201DTO> GeographyUpload { get; set; } = new ObservableCollection<GSM02201DTO>();

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

        public string SourceFileName { get; set; } = "";
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool BtnSave { get; set; } = true;
        public int SumListStaffExcel { get; set; }
        public int SumValidDataStaffExcel { get; set; }
        public int SumInvalidDataStaffExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        public string CompanyID { get; set; }
        public string UserId { get; set; }

        
        public async Task ConvertGrid(List<GSM02201DTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {

                    GSM02201DTO loData = new GSM02201DTO
                    {

                        SeqNo = i + 1,
                        GeographyCode = loTemp.GeographyCode,
                        GeographyName = loTemp.GeographyName,
                        Active = loTemp.Active,
                        ParentCode = loTemp.ParentCode,
                        Notes = "",
                        Valid = ""
                    };

                    // set Parent Name
                    var loParent = (await _GSM02200Model.R_ServiceGetRecordAsync(new GSM02200DTO { CCODE = loTemp.ParentCode }));
                    loData.CNAME = loParent is null ? "" : loParent.CNAME;

                    return loData;
                }
                ));

                // Onchange Visible Error
                VisibleError = false;
                SumValidDataStaffExcel = 0;
                SumInvalidDataStaffExcel = 0;
                SumListStaffExcel = loData.Count();

                GeographyUpload = new ObservableCollection<GSM02201DTO>(loData);
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
            List<GSM02201DTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = CompanyID;
                loBatchPar.USER_ID = UserId;
                loBatchPar.ClassName = "GSM02200BACK.GSM02200Cls";
                loBatchPar.BigObject = GeographyUpload.ToList();

                var lcGuid = await loCls.R_BatchProcess<List<GSM02201DTO>>(loBatchPar, 9);
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_GEOGRAPHYResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

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
                    GeographyUpload.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SeqNo))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SeqNo).FirstOrDefault().ErrorMessage;
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
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<GSM02201DTO>(GeographyUpload);

                    var loDataTable = R_FrontUtility.R_ConvertTo<GSM02201DTO>(loExcelData);
                    loDataTable.TableName = "Geography";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Asign Dataset
                    ExcelDataSet = loDataSet;

                    // Dowload if get Error
                    //await ActionDataSetExcel.Invoke();
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
