using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500MODEL.ViewModel;

namespace LMM03500FRONT
{
    public partial class LookUpTenantGroup : R_Page
    {
        private LookUpTenantGroupViewModel loViewModel = new LookUpTenantGroupViewModel();
        private R_Grid<GetTenantGroupDTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.loProperty.CPROPERTY_ID = (string)poParameter;
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetTenantGroupListAsync();

                eventArgs.ListEntityResult = loViewModel.loTenantGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
