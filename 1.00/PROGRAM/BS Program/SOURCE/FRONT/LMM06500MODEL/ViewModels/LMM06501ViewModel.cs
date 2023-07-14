using LMM06500COMMON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Excel;
using R_ProcessAndUploadFront;
using R_APICommonDTO;
using System.Linq;
using BlazorClientHelper;
using System.IO;
using System.Globalization;
using System.Text.Json;

namespace LMM06500MODEL
{
    public class LMM06501ViewModel : R_IProcessProgressStatus
    {
        private LMM06501Model _LMM06501Model = new LMM06501Model();
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }

        public string PropertyValue = "";
        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;
        public bool VisibleError = false;
        public bool BtnSave = false;

        public ObservableCollection<LMM06501DTO> StaffUploadGrid { get; set; } = new ObservableCollection<LMM06501DTO>();
        public ObservableCollection<LMM06500DTO> StaffGrid { get; set; } = new ObservableCollection<LMM06500DTO>();

        public ObservableCollection<LMM06501ErrorValidateDTO> StaffValidateUploadError { get; set; } = new ObservableCollection<LMM06501ErrorValidateDTO>();

        public async Task GetStaffList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValue);

                var loResult = await _LMM06501Model.GetStaffUploadListAsync();

                StaffGrid = new ObservableCollection<LMM06500DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task AttachFile(byte[] poExcelByte, string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();
            R_UploadPar loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<LMM06501DTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = PropertyValue });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //add filebyte to DTO
                var loExcel = new R_Excel();

                var loDataSet = loExcel.R_ReadFromExcel(poExcelByte);

                var loResult = R_FrontUtility.R_ConvertTo<LMM06501DTO>(loDataSet.Tables[0]);

                Bigobject = new List<LMM06501DTO>(loResult);

                //preapare Batch Parameter
                loUploadPar = new R_UploadPar();
                loUploadPar.COMPANY_ID = pcCompanyId;
                loUploadPar.USER_ID = pcUserId;
                loUploadPar.UserParameters = loUserParameneters;
                loUploadPar.ClassName = "LMM06500BACK.LMM06501Cls";
                loUploadPar.BigObject = Bigobject;

                await loCls.R_AttachFile<List<LMM06501DTO>>(loUploadPar);

                await ValidateDataList(Bigobject);

                VisibleError = false;
                BtnSave = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

                var DeserializeList = JsonSerializer.Deserialize<List<LMM06501ErrorValidateDTO>>(loEx.ErrorList[0].ErrDescp);

                if (DeserializeList.Count > 0)
                    StaffValidateUploadError = new ObservableCollection<LMM06501ErrorValidateDTO>(DeserializeList);

                BtnSave = false;
                VisibleError = true;
            }

        }
        
        public async Task ValidateDataList(List<LMM06501DTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await GetStaffList();

                var loMasterData = StaffGrid.Where(x => x.CPROPERTY_ID == PropertyValue).Select(x => x.CSTAFF_ID).ToList();

                var loData = poEntity.Select(item =>
                {
                    item.Var_Exists = loMasterData.Contains(item.StaffId);
                    return item;
                }).ToList();

                await ConvertGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        public async Task ConvertGrid(List<LMM06501DTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = poEntity;
                var loData = loTempParam.Select(loTemp => new LMM06501ErrorValidateDTO
                {
                    StaffId = loTemp.StaffId,
                    StaffName = loTemp.StaffName,
                    Active = loTemp.Active,
                    Department = loTemp.Department,
                    Position = loTemp.Position,
                    JoinDate = loTemp.JoinDate,
                    Supervisor = loTemp.Supervisor,
                    EmailAddress = loTemp.EmailAddress,
                    MobileNo1 = loTemp.MobileNo1,
                    MobileNo2 = loTemp.MobileNo2,
                    Gender = loTemp.Gender,
                    Address = loTemp.Address,
                    InActiveDate = DateTime.ParseExact(loTemp.InActiveDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                    InactiveNote = loTemp.InactiveNote,
                    Var_Exists = loTemp.Var_Exists,
                    Var_Overwrite = loTemp.Var_Overwrite
                }).ToList();

                StaffValidateUploadError = new ObservableCollection<LMM06501ErrorValidateDTO>(loData);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        public async Task SaveBulkFile(string pcCompanyId, string pcUserId)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<LMM06501ErrorValidateDTO> Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                // set Param
                loUserParameneters = new List<R_KeyValue>();
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = PropertyValue });
                loUserParameneters.Add(new R_KeyValue() { Key = ContextConstant.COVERWRITE, Value = OverwriteData });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM",
                    poProcessProgressStatus: this);

                //Set Data
                if (StaffValidateUploadError.Count == 0)
                    return;

                Bigobject = StaffValidateUploadError.ToList<LMM06501ErrorValidateDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = pcCompanyId;
                loBatchPar.USER_ID = pcUserId;
                loBatchPar.UserParameters = loUserParameneters;
                loBatchPar.ClassName = "LMM06500BACK.LMM06501Cls";
                loBatchPar.BigObject = Bigobject;
                var lcGuid = await loCls.R_BatchProcess<List<LMM06501ErrorValidateDTO>>(loBatchPar, Bigobject.Count);
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
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
            }

            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            await Task.CompletedTask;
        }
        #endregion
    }

}
