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
using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Excel;
using R_ProcessAndUploadFront;
using LMM04000COMMON;
using System.Text.Json;

namespace LMM04000MODEL.ViewModel
{
    public class UploadContractorViewModel : R_ViewModel<UploadContractorDTO>, R_IProcessProgressStatus
    {
        public Action<R_Exception> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        //private UploadContractorModel loModel = new UploadContractorModel();

        public ObservableCollection<UploadContractorDTO> loUploadContractorDisplayList = new ObservableCollection<UploadContractorDTO>();

        public List<UploadContractorDTO> loUploadContractorList = null;

        //public List<UploadContractorErrorDTO> loErrorList = null;

        //public UploadContractorErrorResultDTO loErrorRtn = null;

        //public UploadContractorResultDTO loRtn = null;

        public UploadContractorParameterDTO loParameter = new UploadContractorParameterDTO();

        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsUploadSuccesful = true;
        public bool IsErrorEmptyFile = false;

        public void ReadExcelFile()
        {
            var loEx = new R_Exception();
            List<UploadContractorExcelDTO> loExtract = new List<UploadContractorExcelDTO>();
            try
            {
                //add filebyte to DTO
                var loExcel = new R_Excel();

                var loDataSet = loExcel.R_ReadFromExcel(fileByte, new string[] { "Contractor" });

                var loResult = R_FrontUtility.R_ConvertTo<UploadContractorExcelDTO>(loDataSet.Tables[0]);

                loExtract = new List<UploadContractorExcelDTO>(loResult);

                loUploadContractorList = loExtract.Select((x, i) => new UploadContractorDTO
                {
                    NO = i + 1,
                    TenantId = x.ContractorId,
                    TenantName = x.ContractorName,
                    Address = x.Address,
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
                    CompanyId = SelectedCompanyId,
                    PropertyId = loParameter.PropertyData.CPROPERTY_ID,
                }).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                var MatchingError = loEx.ErrorList.FirstOrDefault(x => x.ErrDescp == "SkipNumberOfRowsStart was out of range: 0");
                IsErrorEmptyFile = MatchingError != null;
            }
            loEx.ThrowExceptionIfErrors();
        }
/*
        public async Task ValidateUploadContractor()
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_CONTRACTOR_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = SelectedCompanyId;
                loUploadPar.USER_ID = SelectedUserId;
                loUploadPar.UserParameters = loUserParam;
                loUploadPar.ClassName = "LMM04000BACK.ValidateUploadContractorCls";
                loUploadPar.BigObject = loUploadContractorList;

                await loCls.R_BatchProcess<List<UploadContractorDTO>>(loUploadPar, 16);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task GetUploadContractorListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_CONTRACTOR_STREAMING_CONTEXT, loUploadContractorList);
                loRtn = await loModel.GetUploadContractorListStreamAsync();
                loUploadContractorDisplayList = new ObservableCollection<UploadContractorDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*/
        public async Task SaveUploadContractorAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadContractorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_CONTRACTOR_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                
                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadContractorDisplayList.Count == 0)
                    return;

                Bigobject = loUploadContractorDisplayList.ToList<UploadContractorDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "LMM04000BACK.UploadContractorCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadContractorDTO>>(loBatchPar, 16);
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
                        loUploadContractorDisplayList.ToList().ForEach(x =>
                        {
                            if (loResult.Any(y => y.SeqNo == x.NO))
                            {
                                x.Notes = loResult.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                                x.Notes = string.Format(x.Notes, "Contractor");
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
        #endregion
    }
}
