using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM09200MODEL.ViewModel;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON.DTOs.GSM09200;

namespace GSM09200FRONT
{
    public partial class GSM09210 : R_Page, R_ITabPage
    {

        private GSM09210ViewModel loExpenditureViewModel = new GSM09210ViewModel();

        private R_ConductorGrid _conductorExpenditureRef;

        private R_Grid<GSM09210DTO> _gridExpenditureRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                TabParameterDTO loParam = (TabParameterDTO)poParameter;
                if (string.IsNullOrWhiteSpace(loParam.CEXPENDITURE_CATEGORY_ID))
                {
                    return;
                }
                loExpenditureViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
                loExpenditureViewModel.loExpenditureCategory.CCATEGORY_ID = loParam.CEXPENDITURE_CATEGORY_ID;
                loExpenditureViewModel.loExpenditureCategory.CCATEGORY_NAME = loParam.CEXPENDITURE_CATEGORY_NAME;
                await _gridExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            TabParameterDTO loParam = (TabParameterDTO)poParam;
            if (string.IsNullOrWhiteSpace(loParam.CEXPENDITURE_CATEGORY_ID))
            {
                return;
            }
            loExpenditureViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
            loExpenditureViewModel.loExpenditureCategory.CCATEGORY_ID = loParam.CEXPENDITURE_CATEGORY_ID;
            loExpenditureViewModel.loExpenditureCategory.CCATEGORY_NAME = loParam.CEXPENDITURE_CATEGORY_NAME;
            await _gridExpenditureRef.R_RefreshGrid(null);
        }

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
    }
}