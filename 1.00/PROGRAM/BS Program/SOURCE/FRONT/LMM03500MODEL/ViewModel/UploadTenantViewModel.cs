using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03501;
using R_BlazorFrontEnd.Excel;
using R_ProcessAndUploadFront;
using LMM03500COMMON;
using System.Text.Json;
using System.Threading;

namespace LMM03500MODEL.ViewModel
{
    public class UploadTenantViewModel : R_ViewModel<UploadTenantDTO>, R_IProcessProgressStatus
    {
        public Action<R_Exception> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        public ObservableCollection<UploadTenantDTO> loUploadTenantDisplayList = new ObservableCollection<UploadTenantDTO>();

        public List<UploadTenantDTO> loUploadTenantList = null;

        public UploadTenantResultDTO loRtn = null;

        public UploadTenantParameterDTO loParameter = new UploadTenantParameterDTO();

        public List<UploadTenantErrorDTO> loErrorList = null;

        public UploadTenantErrorResultDTO loErrorRtn = null;

        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public bool IsOverWrite = false;

        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsUploadSuccesful = true;
        public bool IsErrorEmptyFile = false;

        public async Task ValidateUploadTenant()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchValidatePar;
            R_ProcessAndUploadClient loCls;
            List<UploadTenantErrorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_TENANT_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchValidatePar = new R_BatchParameter();
                loBatchValidatePar.COMPANY_ID = SelectedCompanyId;
                loBatchValidatePar.USER_ID = SelectedUserId;
                loBatchValidatePar.UserParameters = loUserParam;
                loBatchValidatePar.ClassName = "LMM03500BACK.ValidateUploadTenantCls";
                loBatchValidatePar.BigObject = loUploadTenantList;
                await loCls.R_BatchProcess<List<UploadTenantDTO>>(loBatchValidatePar, 18);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUploadTenantAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadTenantDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_TENANT_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_TENANT_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadTenantDisplayList.Count == 0)
                    return;

                Bigobject = loUploadTenantDisplayList.ToList<UploadTenantDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "LMM03500BACK.UploadTenantCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadTenantDTO>>(loBatchPar, 18);
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
                        loUploadTenantDisplayList.ToList().ForEach(x =>
                        {
                            if (loResult.Any(y => y.SeqNo == x.NO))
                            {
                                x.Notes = loResult.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                                x.Notes = string.Format(x.Notes, "Tenant");
                                x.Valid = "N";
                                SumInvalid++;
                            }
                            else
                            {
                                x.Valid = "Y";
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
                    COMPANY_ID = SelectedCompanyId,
                    USER_ID = SelectedUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_LM_UPLOAD_TENANTResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM");

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
                        R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_TENANT_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

                        loErrorRtn = await loModel.GetErrorProcessListAsync();
                        loErrorList = loErrorRtn.Data;

                        loUploadTenantList = loErrorList.Select(x => new UploadTenantDTO
                        {
                            TenantId = x.TenantId,
                            TenantName = x.TenantName,
                            Address = x.Address,
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
                            Notes = x.ErrorMessage,
                            Var_Exists = false,
                            CompanyId = SelectedCompanyId,
                            PropertyId = loParameter.PropertyData.CPROPERTY_ID
                        }).ToList();

                        loUploadTenantDisplayList = new ObservableCollection<UploadTenantDTO>(loUploadTenantList);

                        VisibleError = true;
                        IsUploadSuccesful = !VisibleError;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }*/
        #endregion

    }
}
