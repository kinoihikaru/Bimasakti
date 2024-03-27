using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM06500COMMON;
using LMM06500FrontResources;
using LMM06500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Lookup_LMModel.ViewModel.LML00300;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;

namespace LMM06500FRONT
{
    public partial class LMM06500 : R_Page
    {

        private LMM06500ViewModel _Staff_viewModel = new LMM06500ViewModel();

        private R_Grid<LMM06500DTO> _Staff_gridRef;

        [Inject] private R_IExcel ExcelProvider { get; set; }
        private R_Conductor _Staff_conductorRef;

        private string _Staff_lcLabel = "Actived";
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_viewModel.GetAllInitList();

                if (_Staff_viewModel.PropertyList.Count > 0)
                {
                    LMM06500DTOInitial loParam = _Staff_viewModel.PropertyList.FirstOrDefault();
                    _Staff_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_MODULE_NAME = "LM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (LMM06500DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM06500",
                        Table_Name = "LMM_STAFF",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSTAFF_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM06500",
                        Table_Name = "LMM_STAFF",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSTAFF_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _Staff_viewModel.PropertyValueContext = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                await _Staff_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Staff

        private async Task Staff_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_viewModel.GetStaffList();

                eventArgs.ListEntityResult = _Staff_viewModel.StaffGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Staff_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM06500DTO>(eventArgs.Data);
                await _Staff_viewModel.GetStaff(loParam);

                eventArgs.Result = _Staff_viewModel.Staff;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox StaffName_TextBox;
        private bool EnableInativeNote = false;
        private async Task Staff_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM06500DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {

                    if (loParam.LACTIVE)
                    {
                        _Staff_lcLabel = "Inactive";
                        _Staff_viewModel.StatusChange = false;
                    }
                    else
                    {
                        _Staff_lcLabel = "Activate";
                        _Staff_viewModel.StatusChange = true;
                    }
                    EnableInativeNote = false;
                }
                else
                {
                    EnableInativeNote = loParam.LACTIVE == false;
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await StaffName_TextBox.FocusAsync();
                    SupervisorLookup = loParam.CPOSITION == "02" && !string.IsNullOrWhiteSpace(loParam.CDEPT_CODE);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool btnUploadEnable = false;
        private void Staff_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = !string.IsNullOrEmpty(_Staff_viewModel.PropertyValueContext);
            btnUploadEnable = !string.IsNullOrEmpty(_Staff_viewModel.PropertyValueContext);
        }

        private async Task DeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();
                if (loData.CDEPT_CODE.Length > 0)
                {
                    var param = new GSL00700ParameterDTO
                    {
                        CSEARCH_TEXT = loData.CDEPT_CODE
                    };

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();

                    var loResult = await loLookupViewModel.GetDepartment(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CDEPT_NAME = "";
                        goto EndBlock;
                    }
                    loData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
                else
                {
                    loData.CDEPT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Staff_Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void Staff_Department_OtherTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();

            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;

            SupervisorLookup = !string.IsNullOrWhiteSpace(loData.CPOSITION) && loData.CPOSITION == "02";

        }

        private bool SupervisorLookup = false;
        private async Task Staff_Position_OnChange(string poParam)
        {
            _Staff_viewModel.Data.CPOSITION = poParam;
            var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();
            SupervisorLookup = poParam.ToString() == "02" && !string.IsNullOrWhiteSpace(loData.CDEPT_CODE);
            if (poParam == "01")
            {
                loData.CSUPERVISOR = "";
                loData.CSUPERVISOR_NAME = "";
            }
        }
        private async Task Supervisor_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();
                if (loData.CSUPERVISOR.Length > 0)
                {
                    var param = new LML00300ParameterDTO
                    {
                        CCOMPANY_ID = clientHelper.CompanyId,
                        CPROPERTY_ID = _Staff_viewModel.PropertyValueContext,
                        CUSER_ID = clientHelper.UserId,
                        CSEARCH_TEXT = loData.CSUPERVISOR
                    };

                    LookupLML00300ViewModel loLookupViewModel = new LookupLML00300ViewModel();

                    var loResult = await loLookupViewModel.GetSupervisor(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loData.CSUPERVISOR_NAME = "";
                        goto EndBlock;
                    }
                    loData.CSUPERVISOR = loResult.CSUPERVISOR;
                    loData.CSUPERVISOR_NAME = loResult.CSUPERVISOR_NAME;
                }
                else
                {
                    loData.CSUPERVISOR_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Staff_Supervisor_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();
            var param = new LML00300ParameterDTO()
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _Staff_viewModel.PropertyValueContext,
                CUSER_ID = clientHelper.UserId
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00300);
        }

        private void Staff_Supervisor_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();

            loData.CSUPERVISOR = loTempResult.CSUPERVISOR;
            loData.CSUPERVISOR_NAME = loTempResult.CSUPERVISOR_NAME;
        }

        [Inject] public R_PopupService PopupService { get; set; }
        private async Task Staff_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_viewModel.SaveStaff((LMM06500DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Staff_viewModel.Staff;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Staff_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_viewModel.DeleteStaff((LMM06500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox StaffId_TextBox;
        private async Task Staff_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM06500DTO)eventArgs.Data;
                loData.DINACTIVE_DATE = DateTime.Now;

                await StaffId_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Staff_Validation(R_ValidationEventArgs eventArgs)
        {
            var loData = (LMM06500DTO)eventArgs.Data;
            var loOldData = _Staff_viewModel.Position;
            GFF00900ParameterDTO loPopupParam = null;
            R_PopupResult loResult = null;
            bool result;

            var loEx = new R_Exception();
            try
            {
                loData.CJOIN_DATE = _Staff_viewModel.JoinDateTime.Value.ToString("yyyyMMdd");
                await _Staff_viewModel.StaffValidation(loData, (eCRUDMode)eventArgs.ConductorMode);
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    if (loOldData == "02" && loData.CPOSITION != loOldData)
                    {
                        loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM06502"; //Uabh Approval Code sesuai Spec masing masing
                        await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                        //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                        if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                        {
                        }
                        else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                        {
                            loPopupParam = new GFF00900ParameterDTO()
                            {
                                Data = loValidateViewModel.loRspActivityValidityList,
                                IAPPROVAL_CODE = "LMM06502" //Uabh Approval Code sesuai Spec masing masing
                            };
                            loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                            if (loResult.Success == false)
                            {
                                eventArgs.Cancel = true;
                                return;
                            }

                            result = (bool)loResult.Result;
                            if (result == true)
                            {
                            }
                            else
                            {
                                eventArgs.Cancel = true;
                            }
                        }
                    }
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM06501"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        var loActiveData = await _Staff_viewModel.ActiveInactiveProcessAsync(loData);
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        loPopupParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "LMM06501" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                        if (loResult.Success == false)
                        {
                            eventArgs.Cancel = true;
                            return;
                        }

                        result = (bool)loResult.Result;
                        if (result == true)
                        {
                            var loActiveData = await _Staff_viewModel.ActiveInactiveProcessAsync(loData);
                        }
                        else
                        {
                            eventArgs.Cancel = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool BtnEnable = false;
        private void Staff_SetHasData(R_SetEventArgs eventArgs)
        {
            BtnEnable = eventArgs.Enable;
        }

        private void Staff_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            SupervisorLookup = false;
        }

        private void Staff_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            SupervisorLookup = false;
        }
        private async Task _Staff_DownloadBtn_OnClick()
        {
            var loData = new List<LMM06501DTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifDownloadTemplate"), R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    foreach (var item in _Staff_gridRef.DataSource)
                    {
                        var loTempData = new LMM06501DTO()
                        {
                            StaffId = item.CSTAFF_ID,
                            StaffName = item.CSTAFF_NAME,
                            //Active = item.LACTIVE,
                            Department = item.CDEPT_NAME,
                            Position = item.CPOSITION,
                            JoinDate = item.CJOIN_DATE,
                            Supervisor = item.CSUPERVISOR,
                            EmailAddress = item.CEMAIL,
                            MobileNo1 = item.CMOBILE_PHONE1,
                            MobileNo2 = item.CMOBILE_PHONE2,
                            Gender = item.CGENDER,
                            Address = item.CADDRESS,
                            InActiveDate = item.DINACTIVE_DATE.ToString("yyyyMMdd"),
                            InactiveNote = item.CINACTIVE_NOTE,
                        };

                        loData.Add(loTempData);
                    }

                    var loDataTable = R_FrontUtility.R_ConvertTo(loData);

                    loDataTable.TableName = "Staff";

                    var loTemp = R_FrontUtility.R_ConvertTo<LMM06501DTO>(loDataTable);
                    //export to excel
                    var loByteFile = ExcelProvider.R_WriteToExcel(loDataTable);
                    var saveFileName = string.Format("Staff {0}.xlsx", _Staff_viewModel.PropertyValueContext);

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task _Staff_TemplateBtn_OnClick()
        {
            var loData = new List<LMM06501DTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifDownloadTemplate"), R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _Staff_viewModel.DownloadTemplate();

                    var saveFileName = string.Format("Staff {0}.xlsx", _Staff_viewModel.PropertyValueContext);

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task _Staff_InActive_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM06500DTO)_Staff_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM06501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _Staff_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Staff_conductorRef.R_SetCurrentData(loGetData);

                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM06501" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task _Staff_InActive_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM06500DTO)_Staff_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loActiveData = await _Staff_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Staff_conductorRef.R_SetCurrentData(loActiveData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void _Staff_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LMM06502HeaderDTO()
            {
                CPROPERTY_ID = _Staff_viewModel.PropertyValueContext,
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LMM06502);
        }


        private void _Staff_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = _Staff_viewModel.PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == _Staff_viewModel.PropertyValueContext);
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(LMM06501);
        }

        private async Task _Staff_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task _Staff_Move_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Staff_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public bool _pageSupplierOnCRUDmode = false;
        private void Staff_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
        }

        private void City_OnLostFocus(object poParam)
        {
            //_Staff_viewModel.Data.CCITY_CODE = (string)poParam;
        }
        private void _Staff_City_Before_Open_Popup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            
            eventArgs.TargetPageType = typeof(GSL02000);
        }

        private async Task _Staff_City_After_Open_Popup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL02000DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loData = (LMM06500DTO)_Staff_conductorRef.R_GetCurrentData();
                loData.CCITY_CODE = loTempResult.CCODE;
                loData.CSTATE_CODE = loTempResult.CCODE_PROVINCE;
                loData.CCOUNTRY_CODE = loTempResult.CCODE_COUNTRY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
    }
}
