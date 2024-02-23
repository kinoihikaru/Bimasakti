using BlazorClientHelper;
using GSM02200COMMON;
using GSM02200FrontResources;
using GSM02200MODEL;
using Lookup_GSCOMMON.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM02200FRONT
{
    public partial class GSM02200 : R_Page
    {
        private GSM02200ViewModel _viewModel = new GSM02200ViewModel();
        private R_TreeView<GSM02200TreeDTO> _treeRef;
        private R_Conductor _conductorRef;

        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        private string _lcLabelActivate = "Activate";
        private R_TextBox _CodeGeographyTextBox;
        private GSM02200TreeDTO loDataParentAfterAdd = new GSM02200TreeDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _treeRef.R_RefreshTree(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GSM02200DTO)eventArgs.Data;

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
                        Program_Id = "GSM02200",
                        Table_Name = "GSB_GEOGRAPHY",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CCODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM02200",
                        Table_Name = "GSB_GEOGRAPHY",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CCODE)
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
        private async Task ShowInactiveCheckbox_OnChange(bool poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ShowInactiveGeography = poParam;

                await _treeRef.R_RefreshTree(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Geography 
        private async Task Geography_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetGeographyList();

                eventArgs.ListEntityResult = _viewModel.GeographyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private R_TextBox GeoName_TextBox;
        private async Task Geography_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02200DTO)eventArgs.Data;

                if (loParam.LACTIVE)
                {
                    _lcLabelActivate = "Inactive";
                    _viewModel.StatusChange = false;
                }
                else
                {
                    _lcLabelActivate = "Activate";
                    _viewModel.StatusChange = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await GeoName_TextBox.FocusAsync();
            }
        }
        private void Geography_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loData = (GSM02200DTO)eventArgs.Data;

            eventArgs.Allow = !string.IsNullOrWhiteSpace(loData.CCODE) & loData.LACTIVE;
        }
        private void Geography_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            string loId;
            int loNum = 0; 

            try
            {
                var loTreeViewList = (List<GSM02200TreeDTO>)_treeRef.DataSource;
                var loCurrentData = (GSM02200TreeDTO)_treeRef.CurrentSelectedData;

                if (loCurrentData is not null)
                {
                    loId = loCurrentData.ParentId;

                    loTreeViewList.ForEach(x =>
                    {
                        if (loId == x.CCODE)
                        {
                            loNum++;
                            loId = x.ParentId;
                        }
                    });

                    eventArgs.Allow = loNum < 2;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private bool _PageOnCRUDMode = false;
        private void Geography_SetOther(R_SetEventArgs eventArgs)
        {
            _PageOnCRUDMode = eventArgs.Enable;
        }
        private void Geography_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {

            var loConductorData = (GSM02200DTO)eventArgs.Data;

            var loData = R_FrontUtility.ConvertObjectToObject<GSM02200TreeDTO>(eventArgs.Data);
            loData.Description = string.Format("{0} - {1}", loConductorData.CCODE, loConductorData.CNAME);
            loData.Id = loConductorData.CCODE;
            loData.ParentId = loConductorData.CPARENT_CODE;
            
            eventArgs.GridData = loData;
        }
        private R_TextBox GeoCode_TextBox;
        private async Task Geography_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
           var loParentData = (GSM02200TreeDTO)_treeRef.CurrentSelectedData;

           var loData = (GSM02200DTO)eventArgs.Data;

            loData.CPARENT_CODE = loParentData.CCODE;
            loData.CPARENT_NAME = loParentData.CNAME;
            loData.DCREATE_DATE = DateTime.Now;
            loData.DUPDATE_DATE = DateTime.Now;

            await GeoCode_TextBox.FocusAsync();
        }
        private async Task Geography_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GSM02200DTO>(eventArgs.Data);
                await _viewModel.GetGeography(loData);

                eventArgs.Result = _viewModel.Geography;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Geography_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.ValidationGeography((GSM02200DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Geography_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveGeography((GSM02200DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.Geography;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        private async Task Geography_TemplateBtn_OnClick()
        {
            try
            {
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifTemplate"), R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();

                    var saveFileName = "Geography Template.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Geography_ActiveInactive_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM02200DTO)_conductorRef.R_GetCurrentData();

                var loValidate = await R_MessageBox.Show("", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifActiveInactive"), _lcLabelActivate), R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.ActiveInactiveProcessAsync(loData);

                    if (_viewModel.ShowInactiveGeography)
                    {
                        await _conductorRef.R_SetCurrentData(_viewModel.Geography);
                    }
                    else
                    {
                        await _treeRef.R_RefreshTree(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

       
        private void Geography_Upload_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM02201);
        }

        private async Task Geography_Upload_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _treeRef.R_RefreshTree(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
