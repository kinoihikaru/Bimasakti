using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;

namespace GLM00400FRONT
{
    public partial class GLM00411 : R_Page, R_ITabPage
    {
        private GLM00411ViewModel _AllocationTargetCenter_viewModel = new GLM00411ViewModel();
        private R_Grid<GLM00412DTO> _AllocationCenter_gridRef;
        private R_Grid<GLM00413DTO> _AllocationCenterPeriod_gridRef;
        private R_Conductor _AllocationCenter_condutorRef;
        private R_ConductorGrid _AllocationCenterPeriod_condutorRef;

        [Inject] IClientHelper clientHelper { get; set; }
        private GLM00410DTO _AllocationDetail = null;
        private bool _SetHasDataDT;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _AllocationDetail = (GLM00410DTO)poParameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00412DTO>(poParameter);

                await _AllocationCenter_gridRef.R_RefreshGrid(loParam);

                _SetHasDataDT = !string.IsNullOrWhiteSpace(loParam.CREC_ID_ALLOCATION_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_MODULE_NAME = "GL";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GLM00412DTO)eventArgs.Data;

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
                        Program_Id = "GLM00411",
                        Table_Name = "GLM_ALLOCATION_CENTER_VALUE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _AllocationCenterPeriod_gridRef.CurrentSelectedData.CREC_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GLM00411",
                        Table_Name = "GLM_ALLOCATION_CENTER_VALUE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _AllocationCenterPeriod_gridRef.CurrentSelectedData.CREC_ID)
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
        #region Allocation Center
        private async Task Allocation_Center_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00412DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;

                await _AllocationTargetCenter_viewModel.GetAllocationCenterList(loParam);

                eventArgs.ListEntityResult = _AllocationTargetCenter_viewModel.AllocationCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_Center_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Center_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00412DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = new GLM00413DTO()
                    {
                        CREC_ID_CENTER_ID = loTempParam.CREC_ID
                    };
                    await _AllocationCenterPeriod_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private bool RefreshBtn = false;
        private void Allocation_Center_SetHasData(R_SetEventArgs eventArgs)
        {
            RefreshBtn = eventArgs.Enable;
        }
        private async Task _AllocationTargetCenter_Refresh_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = _AllocationCenter_gridRef.CurrentSelectedData;
                var loParam = new GLM00413DTO()
                {
                    CREC_ID_CENTER_ID = loTempParam.CREC_ID
                };
                await _AllocationCenterPeriod_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Center_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00412DTO>(_AllocationDetail);

            await _AllocationCenter_gridRef.R_RefreshGrid(loParam);
        }
        #endregion

        #region Allocation Center Period
        private async Task Allocation_CenterPeriod_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00413DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                loParam.CYEAR = _AllocationTargetCenter_viewModel.Year.ToString();

                await _AllocationTargetCenter_viewModel.GetAllocationCenterPeriodList(loParam);

                eventArgs.ListEntityResult = _AllocationTargetCenter_viewModel.AllocationCenterPeriodGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_CenterPeriod_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _AllocationDetail;
            eventArgs.TargetPageType = typeof(GLM00420);
        }

        private async Task Allocation_CenterPeriod_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationTargetCenter_viewModel.GetAllocationCenterPeriod((GLM00413DTO)eventArgs.Data);

                eventArgs.Result = _AllocationTargetCenter_viewModel.AllocationCenterPeriod;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Allocation_CenterPeriod_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
                await _AllocationTargetCenter_viewModel.SaveAllocationCenterPeriod((GLM00413DTO)eventArgs.Data);

                eventArgs.Result = _AllocationTargetCenter_viewModel.AllocationCenterPeriod;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private bool _PageOnCRUDMode;
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            _PageOnCRUDMode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private void Allocation_CenterPeriod_SetEdit(R_SetEventArgs eventArgs)
        {

            _SetHasDataDT = !eventArgs.Enable;
            RefreshBtn = !eventArgs.Enable;
        }
        #endregion

        #region TabPage
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _AllocationDetail = (GLM00410DTO)poParam;
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00412DTO>(poParam);

                await _AllocationCenter_gridRef.R_RefreshGrid(loParam);

                _SetHasDataDT = !string.IsNullOrWhiteSpace(loParam.CREC_ID_ALLOCATION_ID);
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
