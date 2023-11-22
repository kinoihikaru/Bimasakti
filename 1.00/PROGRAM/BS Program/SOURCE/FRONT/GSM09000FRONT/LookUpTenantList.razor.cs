using GSM09000COMMON.DTOs.GSM09001;
using GSM09000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000FRONT
{
    public partial class LookUpTenantList : R_Page
    {
        private LookUpTenantCategoryViewModel loTenantCategoryViewModel = new LookUpTenantCategoryViewModel();

        private R_Grid<GetTenantCategoryDTO> _gridTenantCategoryRef;

        LookUpTenantListParameterDTO loParam = new LookUpTenantListParameterDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loParam = (LookUpTenantListParameterDTO)poParameter;
                loTenantCategoryViewModel.loProperty.CPROPERTY_ID = loParam.CPROPERTY_ID;
                await _gridTenantCategoryRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loTenantCategoryViewModel.GetTenantCategoryListStreamAsync();

                eventArgs.ListEntityResult = loTenantCategoryViewModel.loTenantCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickOk()
        {
            var loData = _gridTenantCategoryRef.GetCurrentData();
            GetTenantCategoryDTO loResult = (GetTenantCategoryDTO)loData;
            await this.Close(true, loResult);
        }
        public async Task Button_OnClickClose()
        {
            await this.Close(false, null);
        }
    }
}
