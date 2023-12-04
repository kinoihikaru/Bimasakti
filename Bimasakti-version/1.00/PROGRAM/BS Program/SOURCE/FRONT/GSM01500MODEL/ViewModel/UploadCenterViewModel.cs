using GSM01500COMMON.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GSM01500MODEL.ViewModel
{
    public class UploadCenterViewModel : R_ViewModel<UploadCenterDTO>, R_IProcessProgressStatus
    {
        private UploadCenterModel loModel = new UploadCenterModel();

        public Action<R_Exception> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        public ObservableCollection<UploadCenterErrorDTO> loUploadCenterErrorList = new ObservableCollection<UploadCenterErrorDTO>();

        public ObservableCollection<UploadCenterDTO> loUploadCenterDisplayList = new ObservableCollection<UploadCenterDTO>();

        public List<UploadCenterDTO> loUploadCenterList = new List<UploadCenterDTO>();

        public UploadCenterResultDTO loRtn = null;

        public UploadCenterErrorResultDTO loErrorRtn = null;

        public List<UploadCenterErrorDTO> loErrorList = null;
        
        public CompanyDTO loCompany = new CompanyDTO();

        public CompanyResultDTO loCompanyRtn = null;

        public string SelectedUserId = "";

        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsUploadSuccesful = true;
        public bool IsErrorEmptyFile = false;
/*
        public async Task ValidateUploadCenter()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchValidatePar;
            R_ProcessAndUploadClient loCls;
            List<UploadCenterErrorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_CENTER_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = loCompany.CCOMPANY_ID;
                loBatchValidatePar.USER_ID = SelectedUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "GSM01500BACK.ValidateUploadCenterCls";
                loBatchValidatePar.BigObject = loUploadCenterList;

                lcBatchProcessType = VALIDATION_PROCESS;
                await loCls.R_BatchProcess<List<UploadCenterDTO>>(loBatchValidatePar, 7);

                VisibleError = false; 
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUploadCenterListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_CENTER_PARAMETER_STREAMING_CONTEXT, loUploadCenterList);
                loRtn = await loModel.GetUploadCenterListStreamAsync();
                loUploadCenterDisplayList = new ObservableCollection<UploadCenterDTO>(loRtn.Data);*//*

                GSM01500CenterListDTO loResult = new GSM01500CenterListDTO();
                loResult = await loCenterModel.GetCenterListStreamAsync();

                List<UploadCenterDTO> loTemp = new List<UploadCenterDTO>();

                loTemp = loUploadCenterList.Select(x => new UploadCenterDTO()
                {
                    CCENTER_CODE = x.CCENTER_CODE,
                    CCENTER_NAME = x.CCENTER_NAME,
                    CCOMPANY_ID = x.CCOMPANY_ID,
                    CNOTES = x.CNOTES,
                    LACTIVE = x.LACTIVE,
                    NONACTIVE_DATE = x.NONACTIVE_DATE,
                    LEXIST = loResult.Data.Any(y => x.CCENTER_CODE == y.CCENTER_CODE),
                }).ToList();

                loUploadCenterDisplayList = new ObservableCollection<UploadCenterDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*/
        public async Task GetCompanyAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCompanyRtn = await loModel.GetCompanyAsync();
                loCompany = loCompanyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveUploadCenterAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<UploadCenterDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadCenterDisplayList.Count == 0)
                    return;

                Bigobject = loUploadCenterDisplayList.ToList<UploadCenterDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = loCompany.CCOMPANY_ID;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM01500BACK.UploadCenterCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                await loCls.R_BatchProcess<List<UploadCenterDTO>>(loBatchPar, 10);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_Exception loException = new R_Exception();
            List<R_ErrorStatusReturn> loResult = null;

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";

                    try
                    {
                        loResult = await ServiceGetError(pcKeyGuid);
                        loUploadCenterDisplayList.ToList().ForEach(x =>
                        {
                            if (loResult.Any(y => y.SeqNo == x.INO))
                            {
                                x.CNOTES = loResult.Where(y => y.SeqNo == x.INO).FirstOrDefault().ErrorMessage;
                                string.Format(x.CNOTES, "Tenant");
                                x.CVALID = "N";
                                SumInvalid++;
                            }
                            else
                            {
                                x.CVALID = "Y";
                                SumValid++;
                            }
                        });

                        if (loResult.Any(x => x.SeqNo < 0))
                        {
                            loResult.Where(x => x.SeqNo < 0).ToList().ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                        }
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                    }
                    if (loException.HasError)
                    {
                        ShowErrorAction(loException);
                    }
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loException.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            R_Exception loException = new R_Exception();
            ex.ErrorList.ForEach(l =>
            {
                loException.Add(l.ErrNo, l.ErrDescp);
            });

            ShowErrorAction(loException);
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

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = loCompany.CCOMPANY_ID,
                    USER_ID = SelectedUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_UPLOAD_GSM_CENTERResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loResultData;
        }
        /*
                private async Task GetError(string pcKeyGuid)
                {
                    //R_APIException loException;
                    //R_ProcessAndUploadClient loCls;
                    //List<R_ErrorStatusReturn> loErrRtn;

                    try
                    {
                        R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

                        loErrorRtn = await loModel.GetErrorProcessListAsync();
                        loErrorList = loErrorRtn.Data;


                        loUploadCenterList = loErrorList.Select(x => new UploadCenterDTO
                        {
                            CCENTER_CODE = x.CCENTER_CODE,
                            CCENTER_NAME = x.CCENTER_NAME,
                            LACTIVE = x.ACTIVE,
                            NONACTIVE_DATE = x.NONACTIVEDATE,
                            CNOTES = x.ErrorMessage,
                            CCOMPANY_ID = loCompany.CCOMPANY_ID
                        }).ToList();

                        loUploadCenterDisplayList = new ObservableCollection<UploadCenterDTO>(loUploadCenterList);

                        VisibleError = true;
                        IsUploadSuccesful = !VisibleError;

                        //DO SOMETHING WITH ERROR

                        //loErrRtn = await loCls.R_GetErrorProcess(new R_UploadAndProcessKey() { KEY_GUID = pcKeyGuid });
                        //foreach (R_ErrorStatusReturn item in loErrRtn)
                        //{
                        //    Message = $"Error SeqNo {item.SeqNo} with msg {item.ErrorMessage}";
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }*/
        #endregion

    }
}
