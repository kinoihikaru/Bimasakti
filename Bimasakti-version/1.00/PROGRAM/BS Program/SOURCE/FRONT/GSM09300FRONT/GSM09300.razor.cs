using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM09300MODEL.ViewModel;
using GSM09300COMMON.DTOs.GSM09300;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON;

namespace GSM09300FRONT
{
    public partial class GSM09300 : R_Page
    {
        private GSM09300ViewModel loSupplierCategoryViewModel = new();

        private R_Conductor _conductorSupplierCategoryRef;

        private R_Grid<GSM09300DTO> _gridSupplierCategoryRef;

        private R_Grid<GSM09310DTO> _gridSupplierRef;

        private R_TabStrip _TabStripRef;

        private R_TabPage _tabSupplierListRef;

        private BindingList<GSM09300DTO> loParentPositionBindingList = new BindingList<GSM09300DTO>();

        //private bool IsListExist = false;

        private bool isSupplierTabActivated = false;

        private bool IsCRUDMode = false;

        private bool IsLevelNotZero = true;

        private bool IsLevelNotThree = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loSupplierCategoryViewModel.GetPropertyListStreamAsync();

                if (loSupplierCategoryViewModel.loPropertyList.Count > 0)
                {
                    loSupplierCategoryViewModel.loProperty = new GetPropertyListDTO()
                    {
                        CPROPERTY_ID = loSupplierCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID,
                        CPROPERTY_NAME = loSupplierCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_NAME
                    };
                    await PropertyDropdown_ValueChanged(loSupplierCategoryViewModel.loProperty.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_SupplierCategory_SetOther(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = eventArgs.Enable;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_TabStripRef.ActiveTab.Id == "Supplier Category")
            {
                eventArgs.Cancel = !IsCRUDMode;
            }
        }

        private void Grid_SupplierCategory_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loSupplierCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09300DTO>(loSupplierCategoryViewModel.loSupplierCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_SupplierCategory_AfterDelete()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loSupplierCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09300DTO>(loSupplierCategoryViewModel.loSupplierCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private void Before_Open_SupplierList_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new TabParameterDTO()
            {
                CPROPERTY_ID = loSupplierCategoryViewModel.loProperty.CPROPERTY_ID,
                CSUPPLIER_CATEGORY_ID = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_ID,
                CSUPPLIER_CATEGORY_NAME = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_NAME
            };
            eventArgs.TargetPageType = typeof(GSM09310);
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
                }

                loSupplierCategoryViewModel.loProperty.CPROPERTY_ID = poParam;
                loSupplierCategoryViewModel.loProperty.CPROPERTY_NAME = loSupplierCategoryViewModel.loPropertyList.Where
                    (x => x.CPROPERTY_ID == poParam).FirstOrDefault().CPROPERTY_NAME;

                if (_TabStripRef.ActiveTab.Id == "Supplier Category")
                {
                    await loSupplierCategoryViewModel.ValidateIsCategoryExistAsync();
                    if (_conductorSupplierCategoryRef.R_ConductorMode == R_eConductorMode.Add || _conductorSupplierCategoryRef.R_ConductorMode == R_eConductorMode.Edit)
                    {
                        return;
                    }
                }

                await _gridSupplierCategoryRef.R_RefreshGrid(null);

                loTabParameter = new TabParameterDTO()
                {
                    CPROPERTY_ID = loSupplierCategoryViewModel.loProperty.CPROPERTY_ID,
                    CSUPPLIER_CATEGORY_ID = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_ID,
                    CSUPPLIER_CATEGORY_NAME = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_NAME
                };

                if (_TabStripRef.ActiveTab.Id == "Supplier List")
                {
                    await _tabSupplierListRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_SupplierCategory_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM09300DetailDTO)eventArgs.Data;
                loSupplierCategoryViewModel.loSupplierCategoryDetail = loParam;

                if (isSupplierTabActivated)
                {/*
                    loSupplierViewModel.loSupplierCategory.CSUPPLIER_CATEGORY_ID = loSupplierCategoryViewModel.loSupplierCategoryDetail.CSUPPLIER_CATEGORY_ID;
                    await loSupplierViewModel.GetSupplierCategoryAsync();*/
                    await _gridSupplierRef.R_RefreshGrid(null);
                }
            }
        }

        private async Task Grid_SupplierCategory_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loSupplierCategoryViewModel.GetSupplierCategoryListStreamAsync();

                loParentPositionBindingList = new BindingList<GSM09300DTO>(loSupplierCategoryViewModel.loSupplierCategoryList);
                eventArgs.ListEntityResult = loSupplierCategoryViewModel.loSupplierCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_SupplierCategory_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsLevelNotZero = false;
                GSM09300DetailDTO loData = (GSM09300DetailDTO)eventArgs.Data;
                /*
                                if (loSupplierCategoryViewModel.loSupplierCategoryList.Count() == 0)
                                {
                                    IsListExist = false;
                                    await loSupplierCategoryViewModel.GetParentPositionIfEmptyAsync();
                                    loData.CPARENT = loSupplierCategoryViewModel.loParentPositionIfEmpty.CCATEGORY_ID;
                                }
                                else if (loSupplierCategoryViewModel.loSupplierCategoryList.Count > 0)
                                {
                                    IsListExist = true;
                                    loData.CPARENT = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_ID;
                                }
                */
                loData.CPARENT = loSupplierCategoryViewModel.loSupplierCategoryDetail.CCATEGORY_ID;
                int result = loSupplierCategoryViewModel.loParentPositionList.
                    Where(x => x.CCATEGORY_ID == loSupplierCategoryViewModel.Data.CCATEGORY_ID).
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

        private void Grid_SupplierCategory_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            IsLevelNotZero = false;
        }

        private void Grid_SupplierCategory_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loSupplierCategoryViewModel.loSupplierCategoryDetail.ILEVEL != 0)
            {
                IsLevelNotZero = true;
            }
        }

        private async Task Grid_SupplierCategory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM09300DetailDTO loParam = new GSM09300DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM09300DetailDTO>(eventArgs.Data);
                await loSupplierCategoryViewModel.GetSupplierCategoryAsync(loParam);

                if (loSupplierCategoryViewModel.loSupplierCategoryDetail.ILEVEL == 0)
                {
                    IsLevelNotZero = false;
                }
                else
                {
                    IsLevelNotZero = true;
                }

                if (loSupplierCategoryViewModel.loSupplierCategoryDetail.ILEVEL == 3)
                {
                    IsLevelNotThree = false;
                }
                else
                {
                    IsLevelNotThree = true;
                }
                eventArgs.Result = loSupplierCategoryViewModel.loSupplierCategoryDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingSupplierCategory(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loSupplierCategoryViewModel.SupplierCategoryValidation((GSM09300DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_SupplierCategory_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loSupplierCategoryViewModel.SaveSupplierCategoryAsync(
                    (GSM09300DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loSupplierCategoryViewModel.loSupplierCategoryDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_SupplierCategory_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM09300DetailDTO loData = (GSM09300DetailDTO)eventArgs.Data;
                await loSupplierCategoryViewModel.DeleteSupplierCategoryAsync(loData);
                /*
                                if (loSupplierCategoryViewModel.loSupplierCategoryList.Count() == 0)
                                {
                                    IsListExist = false;
                                }
                                else if (loSupplierCategoryViewModel.loSupplierCategoryList.Count > 0)
                                {
                                    IsListExist = true;
                                }
                */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loParentPositionBindingList = new BindingList<GSM09300DTO>(loSupplierCategoryViewModel.loSupplierCategoryList);
            loEx.ThrowExceptionIfErrors();
        }
        /*
                private async Task Grid_Supplier_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
                {
                    var loEx = new R_Exception();

                    try
                    {
                        await loSupplierViewModel.GetSupplierListStreamAsync();
                        eventArgs.ListEntityResult = loSupplierViewModel.loSupplierList;
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loEx.ThrowExceptionIfErrors();
                }

                private void R_Before_Open_Popup_Supplier_Move(R_BeforeOpenPopupEventArgs eventArgs)
                {
                    MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();
                    loParam.PropertyData = loSupplierViewModel.loProperty;
                    loParam.SupplierCategoryData = loSupplierViewModel.loSupplierCategory;
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(MoveSupplierModal);
                }

                private async Task R_After_Open_Popup_Supplier_Move(R_AfterOpenPopupEventArgs eventArgs)
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
                    await _gridSupplierRef.R_RefreshGrid(null);
                }
        *//*
                private async Task OnTabChanged(R_TabStripTab eventArgs)
                {
                    R_Exception loException = new R_Exception();
                    try
                    {
                        if (eventArgs.Id == "Supplier List" && isSupplierTabActivated == false)
                        {
                            isSupplierTabActivated = true;
                            loSupplierViewModel.loSupplierCategory.CSUPPLIER_CATEGORY_ID = loSupplierCategoryViewModel.loSupplierCategoryDetail.CSUPPLIER_CATEGORY_ID;
                            await loSupplierViewModel.GetSupplierCategoryAsync();
                            await _gridSupplierRef.R_RefreshGrid(null);
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