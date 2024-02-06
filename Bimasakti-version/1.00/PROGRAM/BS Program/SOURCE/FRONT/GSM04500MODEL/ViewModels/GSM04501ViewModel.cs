using GSM04500COMMON;
using GSM04500FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GSM04500MODEL
{
    public class GSM04501ViewModel : R_ViewModel<GSM04501DisplayDTO>, R_IProcessProgressStatus
    {
        #region Model
        private GSM04500Model _GSM04500Model = new GSM04500Model();
        #endregion

        #region Init Data
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

        #region Property Progress Status
        public string CompanyID { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool BtnSave { get; set; } = true;
        public int SumListExcel { get; set; }
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public bool VisibleError { get; set; } = false;
        #endregion

        #region Property ViewModel
        public ObservableCollection<GSM04501DisplayDTO> JournalGroupUploadGrid { get; set; } = new ObservableCollection<GSM04501DisplayDTO>();
        public GSM04500ParameterUploadDTO Parameter { get; set; } = new GSM04500ParameterUploadDTO();
        #endregion

        public async Task ConvertGrid(List<GSM04501DisplayDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    GSM04501DisplayDTO loData = new GSM04501DisplayDTO
                    {

                        No = i + 1,
                        JournalGroup = loTemp.JournalGroup,
                        JournalGroupName = loTemp.JournalGroupName,
                        EnableAccrual = loTemp.EnableAccrual,
                        ErrorFlag = "",
                        ErrorMessage = ""
                    };

                    return loData;
                }
                ));

                // Onchange Visible Error
                VisibleError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;
                SumListExcel = loData.Count();

                JournalGroupUploadGrid = new ObservableCollection<GSM04501DisplayDTO>(loData);
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
            List<GSM04501DisplayDTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue()
                { Key = ContextConstant.CPROPERTY_ID, Value = Parameter.CPROPERTY_ID });
                loUserParameneters.Add(new R_KeyValue()
                { Key = ContextConstant.CJRNGRP_TYPE, Value = Parameter.CJRNGRP_TYPE });

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
                loBatchPar.ClassName = "GSM04500BACK.GSM04500Cls";
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.BigObject = JournalGroupUploadGrid.ToList();

                var lcGuid = await loCls.R_BatchProcess<List<GSM04501DisplayDTO>>(loBatchPar, 10);
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_JOURNAL_GROUPResources"
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
                    JournalGroupUploadGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.No))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = "N";
                            SumInvalidDataExcel++;
                        }
                        else
                        {
                            x.ErrorFlag = "Y";
                            SumValidDataExcel++;
                        }
                    });

                    //Set DataSetTable and get error
                    var loExcelData = R_FrontUtility.ConvertCollectionToCollection<GSM04501DisplayDTO>(JournalGroupUploadGrid);

                    var loDataTable = R_FrontUtility.R_ConvertTo<GSM04501DisplayDTO>(loExcelData);
                    loDataTable.TableName = "JournalGroup";

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
