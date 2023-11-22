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
using GSM09200MODEL.ViewModel;
using GSM09200COMMON.DTOs.GSM09200;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON;

namespace GSM09200FRONT
{
    public partial class GSM09200 : R_Page
    {
        private GSM09200ViewModel loExpenditureCategoryViewModel = new();

        private R_Conductor _conductorExpenditureCategoryRef;

        private R_Grid<GSM09200DTO> _gridExpenditureCategoryRef;

        private R_Grid<GSM09210DTO> _gridExpenditureRef;

        private R_TabStrip _TabStripRef;

        private R_TabPage _tabExpenditureListRef;

        private BindingList<GSM09200DTO> loParentPositionBindingList = new BindingList<GSM09200DTO>();

        //private bool IsListExist = false;

        private bool isExpenditureTabActivated = false;

        private bool IsCRUDMode = false;

        private bool IsLevelNotZero = true;

        private bool IsLevelNotThree = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loExpenditureCategoryViewModel.GetPropertyListStreamAsync();

                if (loExpenditureCategoryViewModel.loPropertyList.Count > 0)
                {
                    loExpenditureCategoryViewModel.loProperty = new GetPropertyListDTO()
                    {
                        CPROPERTY_ID = loExpenditureCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID,
                        CPROPERTY_NAME = loExpenditureCategoryViewModel.loPropertyList.FirstOrDefault().CPROPERTY_NAME
                    };
                    await PropertyDropdown_ValueChanged(loExpenditureCategoryViewModel.loProperty.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_ExpenditureCategory_SetOther(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = eventArgs.Enable;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_TabStripRef.ActiveTab.Id == "Expenditure Category")
            {
                eventArgs.Cancel = !IsCRUDMode;
            }
        }

        private void Grid_ExpenditureCategory_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loExpenditureCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09200DTO>(loExpenditureCategoryViewModel.loExpenditureCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_ExpenditureCategory_AfterDelete()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loExpenditureCategoryViewModel.UpdateParentPositionList();
                loParentPositionBindingList = new BindingList<GSM09200DTO>(loExpenditureCategoryViewModel.loExpenditureCategoryList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private void Before_Open_ExpenditureList_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new TabParameterDTO()
            {
                CPROPERTY_ID = loExpenditureCategoryViewModel.loProperty.CPROPERTY_ID,
                CEXPENDITURE_CATEGORY_ID = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_ID,
                CEXPENDITURE_CATEGORY_NAME = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_NAME
            };
            eventArgs.TargetPageType = typeof(GSM09210);
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

                loExpenditureCategoryViewModel.loProperty.CPROPERTY_ID = poParam;
                loExpenditureCategoryViewModel.loProperty.CPROPERTY_NAME = loExpenditureCategoryViewModel.loPropertyList.Where
                    (x => x.CPROPERTY_ID == poParam).FirstOrDefault().CPROPERTY_NAME;

                if (_TabStripRef.ActiveTab.Id == "Expenditure Category")
                {
                    await loExpenditureCategoryViewModel.ValidateIsCategoryExistAsync();
                    if (_conductorExpenditureCategoryRef.R_ConductorMode == R_eConductorMode.Add || _conductorExpenditureCategoryRef.R_ConductorMode == R_eConductorMode.Edit)
                    {
                        return;
                    }
                }

                await _gridExpenditureCategoryRef.R_RefreshGrid(null);

                loTabParameter = new TabParameterDTO()
                {
                    CPROPERTY_ID = loExpenditureCategoryViewModel.loProperty.CPROPERTY_ID,
                    CEXPENDITURE_CATEGORY_ID = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_ID,
                    CEXPENDITURE_CATEGORY_NAME = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_NAME
                };

                if (_TabStripRef.ActiveTab.Id == "Expenditure List")
                {
                    await _tabExpenditureListRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ExpenditureCategory_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM09200DetailDTO)eventArgs.Data;
                loExpenditureCategoryViewModel.loExpenditureCategoryDetail = loParam;

                if (isExpenditureTabActivated)
                {/*
                    loExpenditureViewModel.loExpenditureCategory.CExpenditure_CATEGORY_ID = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CExpenditure_CATEGORY_ID;
                    await loExpenditureViewModel.GetExpenditureCategoryAsync();*/
                    await _gridExpenditureRef.R_RefreshGrid(null);
                }
            }
        }

        private async Task Grid_ExpenditureCategory_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureCategoryViewModel.GetExpenditureCategoryListStreamAsync();

                loParentPositionBindingList = new BindingList<GSM09200DTO>(loExpenditureCategoryViewModel.loExpenditureCategoryList);
                eventArgs.ListEntityResult = loExpenditureCategoryViewModel.loExpenditureCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ExpenditureCategory_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                IsLevelNotZero = false;
                GSM09200DetailDTO loData = (GSM09200DetailDTO)eventArgs.Data;
                /*
                                if (loExpenditureCategoryViewModel.loExpenditureCategoryList.Count() == 0)
                                {
                                    IsListExist = false;
                                    await loExpenditureCategoryViewModel.GetParentPositionIfEmptyAsync();
                                    loData.CPARENT = loExpenditureCategoryViewModel.loParentPositionIfEmpty.CCATEGORY_ID;
                                }
                                else if (loExpenditureCategoryViewModel.loExpenditureCategoryList.Count > 0)
                                {
                                    IsListExist = true;
                                    loData.CPARENT = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_ID;
                                }
                */
                loData.CPARENT = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CCATEGORY_ID;
                int result = loExpenditureCategoryViewModel.loParentPositionList.
                    Where(x => x.CCATEGORY_ID == loExpenditureCategoryViewModel.Data.CCATEGORY_ID).
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

        private void Grid_ExpenditureCategory_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            IsLevelNotZero = false;
        }

        private void Grid_ExpenditureCategory_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loExpenditureCategoryViewModel.loExpenditureCategoryDetail.ILEVEL != 0)
            {
                IsLevelNotZero = true;
            }
        }

        private async Task Grid_ExpenditureCategory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM09200DetailDTO loParam = new GSM09200DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM09200DetailDTO>(eventArgs.Data);
                await loExpenditureCategoryViewModel.GetExpenditureCategoryAsync(loParam);

                if (loExpenditureCategoryViewModel.loExpenditureCategoryDetail.ILEVEL == 0)
                {
                    IsLevelNotZero = false;
                }
                else
                {
                    IsLevelNotZero = true;
                }

                if (loExpenditureCategoryViewModel.loExpenditureCategoryDetail.ILEVEL == 3)
                {
                    IsLevelNotThree = false;
                }
                else
                {
                    IsLevelNotThree = true;
                }
                eventArgs.Result = loExpenditureCategoryViewModel.loExpenditureCategoryDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingExpenditureCategory(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loExpenditureCategoryViewModel.ExpenditureCategoryValidation((GSM09200DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ExpenditureCategory_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loExpenditureCategoryViewModel.SaveExpenditureCategoryAsync(
                    (GSM09200DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loExpenditureCategoryViewModel.loExpenditureCategoryDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ExpenditureCategory_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM09200DetailDTO loData = (GSM09200DetailDTO)eventArgs.Data;
                await loExpenditureCategoryViewModel.DeleteExpenditureCategoryAsync(loData);
                /*
                                if (loExpenditureCategoryViewModel.loExpenditureCategoryList.Count() == 0)
                                {
                                    IsListExist = false;
                                }
                                else if (loExpenditureCategoryViewModel.loExpenditureCategoryList.Count > 0)
                                {
                                    IsListExist = true;
                                }
                */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loParentPositionBindingList = new BindingList<GSM09200DTO>(loExpenditureCategoryViewModel.loExpenditureCategoryList);
            loEx.ThrowExceptionIfErrors();
        }
        /*
                private async Task Grid_Expenditure_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
                {
                    var loEx = new R_Exception();

                    try
                    {
                        await loExpenditureViewModel.GetExpenditureListStreamAsync();
                        eventArgs.ListEntityResult = loExpenditureViewModel.loExpenditureList;
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loEx.ThrowExceptionIfErrors();
                }

                private void R_Before_Open_Popup_Expenditure_Move(R_BeforeOpenPopupEventArgs eventArgs)
                {
                    MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();
                    loParam.PropertyData = loExpenditureViewModel.loProperty;
                    loParam.ExpenditureCategoryData = loExpenditureViewModel.loExpenditureCategory;
                    eventArgs.Parameter = loParam;
                    eventArgs.TargetPageType = typeof(MoveExpenditureModal);
                }

                private async Task R_After_Open_Popup_Expenditure_Move(R_AfterOpenPopupEventArgs eventArgs)
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
                    await _gridExpenditureRef.R_RefreshGrid(null);
                }
        *//*
                private async Task OnTabChanged(R_TabStripTab eventArgs)
                {
                    R_Exception loException = new R_Exception();
                    try
                    {
                        if (eventArgs.Id == "Expenditure List" && isExpenditureTabActivated == false)
                        {
                            isExpenditureTabActivated = true;
                            loExpenditureViewModel.loExpenditureCategory.CExpenditure_CATEGORY_ID = loExpenditureCategoryViewModel.loExpenditureCategoryDetail.CExpenditure_CATEGORY_ID;
                            await loExpenditureViewModel.GetExpenditureCategoryAsync();
                            await _gridExpenditureRef.R_RefreshGrid(null);
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