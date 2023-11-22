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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000FRONT
{
    public partial class GSM09001 : R_Page, R_ITabPage
    {

        private GSM09001ViewModel loTenantViewModel = new GSM09001ViewModel();

        private R_ConductorGrid _conductorTenantRef;

        private R_Grid<GSM09001DTO> _gridTenantRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                TabParameterDTO loParam = (TabParameterDTO)poParameter;
                if (string.IsNullOrWhiteSpace(loParam.CTENANT_CATEGORY_ID))
                {
                    return;
                }
                loTenantViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
                loTenantViewModel.loTenantCategory.CCATEGORY_ID = loParam.CTENANT_CATEGORY_ID;
                loTenantViewModel.loTenantCategory.CCATEGORY_NAME = loParam.CTENANT_CATEGORY_NAME;
                await _gridTenantRef.R_RefreshGrid(null);
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
            if (string.IsNullOrWhiteSpace(loParam.CTENANT_CATEGORY_ID))
            {
                return;
            }
            loTenantViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
            loTenantViewModel.loTenantCategory.CCATEGORY_ID = loParam.CTENANT_CATEGORY_ID;
            loTenantViewModel.loTenantCategory.CCATEGORY_NAME = loParam.CTENANT_CATEGORY_NAME;
            await _gridTenantRef.R_RefreshGrid(null);
        }

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
    }
}