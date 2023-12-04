using LMM06500COMMON;
using LMM06500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace LMM06500MODEL
{
    public class LMM06500ViewModel : R_ViewModel<LMM06500DTO>
    {
        private LMM06500Model _LMM06500Model = new LMM06500Model();
        private LMM06501Model _LMM06501Model = new LMM06501Model();
        private LMM06500UniversalModel _LMM06500UniversalModel = new LMM06500UniversalModel();

        public ObservableCollection<LMM06500DTO> StaffGrid { get; set; } = new ObservableCollection<LMM06500DTO>();
        public List<LMM06500UniversalDTO> PositionList { get; set; } = new List<LMM06500UniversalDTO>();
        public List<LMM06500UniversalDTO> GenderList { get; set; } = new List<LMM06500UniversalDTO>();
        public List<LMM06500DTOInitial> PropertyList { get; set; } = new List<LMM06500DTOInitial>();


        public LMM06500DTO Staff = new LMM06500DTO();

        public string PropertyValueContext = "";
        public bool StatusChange = false;
        public DateTime JoinDateTime = DateTime.Now;
        public string Position = "";


        public async Task GetAllInitList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500UniversalModel.GetAllUniversalListAsync();

                PropertyList = loResult.PropertyList;
                PositionList = loResult.PositionList;
                GenderList = loResult.GenderList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStaffList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM06500Model.GetStaffListAsync(PropertyValueContext);

                StaffGrid = new ObservableCollection<LMM06500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetStaff(LMM06500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM06500Model.R_ServiceGetRecordAsync(poParam);

                if (loResult.CJOIN_DATE != "0")
                {
                    JoinDateTime = DateTime.ParseExact(loResult.CJOIN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                Position = loResult.CPOSITION;

                Staff = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task StaffValidation(LMM06500DTO poParam, eCRUDMode peCRUDMode)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CSTAFF_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6514"));
                }
                else
                {
                    llCancel = poParam.CSTAFF_ID.Length > 20;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "6519"));
                    }
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSTAFF_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6515"));
                }
                else
                {
                    llCancel = poParam.CSTAFF_NAME.Length > 100;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "6520"));
                    }
                }
                
                llCancel = string.IsNullOrWhiteSpace(poParam.CEMAIL);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6516"));
                }
                else
                {
                    llCancel = poParam.CEMAIL.Length > 100;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "6523"));
                    }
                }
                
                llCancel = string.IsNullOrWhiteSpace(poParam.CMOBILE_PHONE1);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6517"));
                }
                else
                {
                    llCancel = poParam.CMOBILE_PHONE1.Length > 30;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "6521"));
                    }
                }
                

                llCancel = !string.IsNullOrWhiteSpace(poParam.CMOBILE_PHONE2) && poParam.CMOBILE_PHONE2.Length > 30;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6522"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CGENDER);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6518"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CCITY_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6524"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6524"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CPOSITION);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "6526"));
                }
                if (poParam.CPOSITION == "02")
                {
                    llCancel = string.IsNullOrWhiteSpace(poParam.CSUPERVISOR);
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "6527"));
                    }
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveStaff(LMM06500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValueContext;
                }
                var loResult = await _LMM06500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                Staff = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteStaff(LMM06500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM06500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LMM06500DTO> ActiveInactiveProcessAsync(LMM06500DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            LMM06500DTO loRtn = null;

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.LACTIVE = StatusChange;

                await _LMM06500Model.LMM06500ActiveInactiveAsync(poParameter);
                var loResult = await _LMM06500Model.R_ServiceGetRecordAsync(poParameter);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<LMM06500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            LMM06500UploadFileDTO loResult = null;

            try
            {
                loResult = await _LMM06501Model.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class RadioButtonInt
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
