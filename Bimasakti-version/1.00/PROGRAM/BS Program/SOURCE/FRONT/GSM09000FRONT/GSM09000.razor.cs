using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09000;
using GSM09000COMMON.DTOs.GSM09001;
using GSM09000MODEL.ViewModel;
using GSM09001MODEL.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000FRONT
{
    public partial class GSM09000 : R_Page
    {
        private GSM09000ViewModel loTenantCategoryViewModel = new();

        private R_Conductor _conductorTenantCategoryRef;

        private R_Grid<GSM09000DTO> _gridTenantCategoryRef;

        private R_Grid<GSM09001DTO> _gridTenantRef;

        private R_TabStrip _TabStripRef;

        private R_TabPage _tabTenantListRef;

        private BindingList<GSM09000DTO> loParentPositionBindingList = new BindingList<GSM09000DTO>();

        //private bool IsListExist = false;
            
        private bool isTenantTabActivated = false;

        private bool IsCRUDMode = false;

        private bool IsLevelNotZero = true;

        private bool IsLevelNotThree = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantCategoryViewModel.GetPropertyListStreamAsync();

                if (loTenantCategoryViewModel.loPropertyList.Count > 0)
                {
                    loTenantCategoryViewModel.loProperty = new GetPropertyListDTO()
                    {
                        CPROPERTY_ID = loTenantCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID,
                        CPROPERTY_NAME = loTenantCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_NAME
                    };
                    await PropertyDropdown_ValueChanged(loTenantCategoryViewModel.loProperty.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_TenantCategory_SetOther(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = eventArgs.Enable;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_TabStripRef.ActiveTab.Id == "Tenant Category")
            {
                eventArgs.Cancel = !IsCRUDMode;
            }
        }

        private void Grid_TenantCategory_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loTenantCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09000DTO>(loTenantCategoryViewModel.loTenantCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_TenantCategory_AfterDelete()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loTenantCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09000DTO>(loTenantCategoryViewModel.loTenantCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
            

        private void Before_Open_TenantList_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new TabParameterDTO()
            {
                CPROPERTY_ID = loTenantCategoryViewModel.loProperty.CPROPERTY_ID,
                CTENANT_CATEGORY_ID = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_ID,
                CTENANT_CATEGORY_NAME = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_NAME
            };
            eventArgs.TargetPageType = typeof(GSM09001);
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            TabParameterDTO loTabParameter = null;
            try
            {
                if (poParam == null)
                {
                    return;
                }/*
                loTenantViewModel.loProperty = loTenantCategoryViewModel.loProperty;*/
                
                if (_TabStripRef.ActiveTab.Id == "Tenant Category")
                {
                    if (_conductorTenantCategoryRef.R_ConductorMode == R_eConductorMode.Add || _conductorTenantCategoryRef.R_ConductorMode == R_eConductorMode.Edit)
                    {
                        return;
                    }
                }

                loTenantCategoryViewModel.loProperty.CPROPERTY_ID = poParam;
                loTenantCategoryViewModel.loProperty.CPROPERTY_NAME = loTenantCategoryViewModel.loPropertyList.Where
                    (x => x.CPROPERTY_ID == poParam).FirstOrDefault().CPROPERTY_NAME;
                await _gridTenantCategoryRef.R_RefreshGrid(null);

                loTabParameter = new TabParameterDTO()
                {
                    CPROPERTY_ID = loTenantCategoryViewModel.loProperty.CPROPERTY_ID,
                    CTENANT_CATEGORY_ID = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_ID,
                    CTENANT_CATEGORY_NAME = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_NAME
                };

                if (_TabStripRef.ActiveTab.Id == "Tenant List")
                {
                    await _tabTenantListRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_TenantCategory_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM09000DetailDTO)eventArgs.Data;
                loTenantCategoryViewModel.loTenantCategoryDetail = loParam;

                if (isTenantTabActivated)
                {/*
                    loTenantViewModel.loTenantCategory.CTENANT_CATEGORY_ID = loTenantCategoryViewModel.loTenantCategoryDetail.CTENANT_CATEGORY_ID;
                    await loTenantViewModel.GetTenantCategoryAsync();*/
                    await _gridTenantRef.R_RefreshGrid(null);
                }
            }
        }

        private async Task Grid_TenantCategory_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantCategoryViewModel.GetTenantCategoryListStreamAsync();

                if (loTenantCategoryViewModel.loTenantCategoryList.Count() == 0)
                {
                    await loTenantCategoryViewModel.InsertNewRootIfListEmptyAsync();
                    await loTenantCategoryViewModel.GetTenantCategoryListStreamAsync();
                    //IsListExist = true;
                }

                loParentPositionBindingList = new BindingList<GSM09000DTO>(loTenantCategoryViewModel.loTenantCategoryList);
                eventArgs.ListEntityResult = loTenantCategoryViewModel.loTenantCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_TenantCategory_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsLevelNotZero = false;
                GSM09000DetailDTO loData = (GSM09000DetailDTO)eventArgs.Data;
/*
                if (loTenantCategoryViewModel.loTenantCategoryList.Count() == 0)
                {
                    IsListExist = false;
                    await loTenantCategoryViewModel.GetParentPositionIfEmptyAsync();
                    loData.CPARENT = loTenantCategoryViewModel.loParentPositionIfEmpty.CCATEGORY_ID;
                }
                else if (loTenantCategoryViewModel.loTenantCategoryList.Count > 0)
                {
                    IsListExist = true;
                    loData.CPARENT = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_ID;
                }
*/
                loData.CPARENT = loTenantCategoryViewModel.loTenantCategoryDetail.CCATEGORY_ID;
                int result = loTenantCategoryViewModel.loParentPositionList.
                    Where(x => x.CCATEGORY_ID == loTenantCategoryViewModel.Data.CCATEGORY_ID).
                    Select(x => x.ILEVEL).
                    FirstOrDefault();
                loData.ILEVEL = result + 1;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Grid_TenantCategory_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            IsLevelNotZero = false;
        }

        private void Grid_TenantCategory_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loTenantCategoryViewModel.loTenantCategoryDetail.ILEVEL != 0)
            {
                IsLevelNotZero = true;
            }
        }

        private async Task Grid_TenantCategory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM09000DetailDTO loParam = new GSM09000DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM09000DetailDTO>(eventArgs.Data);
                await loTenantCategoryViewModel.GetTenantCategoryAsync(loParam);

                if (loTenantCategoryViewModel.loTenantCategoryDetail.ILEVEL == 0)
                {
                    IsLevelNotZero = false;
                }
                else
                {
                    IsLevelNotZero = true;
                }

                if (loTenantCategoryViewModel.loTenantCategoryDetail.ILEVEL == 3)
                {
                    IsLevelNotThree = false;
                }
                else
                {
                    IsLevelNotThree = true;
                }
                eventArgs.Result = loTenantCategoryViewModel.loTenantCategoryDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingTenantCategory(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loTenantCategoryViewModel.TenantCategoryValidation((GSM09000DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_TenantCategory_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantCategoryViewModel.SaveTenantCategoryAsync(
                    (GSM09000DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);
                
                eventArgs.Result = loTenantCategoryViewModel.loTenantCategoryDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_TenantCategory_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM09000DetailDTO loData = (GSM09000DetailDTO)eventArgs.Data;
                await loTenantCategoryViewModel.DeleteTenantCategoryAsync(loData);
/*
                if (loTenantCategoryViewModel.loTenantCategoryList.Count() == 0)
                {
                    IsListExist = false;
                }
                else if (loTenantCategoryViewModel.loTenantCategoryList.Count > 0)
                {
                    IsListExist = true;
                }
*/
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loParentPositionBindingList = new BindingList<GSM09000DTO>(loTenantCategoryViewModel.loTenantCategoryList);
            loEx.ThrowExceptionIfErrors();
        }
/*
        private async Task Grid_Tenant_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantViewModel.GetTenantListStreamAsync();
                eventArgs.ListEntityResult = loTenantViewModel.loTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup_Tenant_Move(R_BeforeOpenPopupEventArgs eventArgs)
        {
            MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();
            loParam.PropertyData = loTenantViewModel.loProperty;
            loParam.TenantCategoryData = loTenantViewModel.loTenantCategory;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(MoveTenantModal);
        }

        private async Task R_After_Open_Popup_Tenant_Move(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                var result = eventArgs.Result;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridTenantRef.R_RefreshGrid(null);
        }
*//*
        private async Task OnTabChanged(R_TabStripTab eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Id == "Tenant List" && isTenantTabActivated == false)
                {
                    isTenantTabActivated = true;
                    loTenantViewModel.loTenantCategory.CTENANT_CATEGORY_ID = loTenantCategoryViewModel.loTenantCategoryDetail.CTENANT_CATEGORY_ID;
                    await loTenantViewModel.GetTenantCategoryAsync();
                    await _gridTenantRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }*/
    }
}