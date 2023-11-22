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
using GSM09300MODEL.ViewModel;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON.DTOs.GSM09300;

namespace GSM09300FRONT
{
    public partial class GSM09310 : R_Page, R_ITabPage
    {

        private GSM09310ViewModel loSupplierViewModel = new GSM09310ViewModel();

        private R_ConductorGrid _conductorSupplierRef;

        private R_Grid<GSM09310DTO> _gridSupplierRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                TabParameterDTO loParam = (TabParameterDTO)poParameter;
                if (string.IsNullOrWhiteSpace(loParam.CSUPPLIER_CATEGORY_ID))
                {
                    return;
                }
                loSupplierViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
                loSupplierViewModel.loSupplierCategory.CCATEGORY_ID = loParam.CSUPPLIER_CATEGORY_ID;
                loSupplierViewModel.loSupplierCategory.CCATEGORY_NAME = loParam.CSUPPLIER_CATEGORY_NAME;
                await _gridSupplierRef.R_RefreshGrid(null);
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
            if (string.IsNullOrWhiteSpace(loParam.CSUPPLIER_CATEGORY_ID))
            {
                return;
            }
            loSupplierViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
            loSupplierViewModel.loSupplierCategory.CCATEGORY_ID = loParam.CSUPPLIER_CATEGORY_ID;
            loSupplierViewModel.loSupplierCategory.CCATEGORY_NAME = loParam.CSUPPLIER_CATEGORY_NAME;
            await _gridSupplierRef.R_RefreshGrid(null);
        }

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
    }
}