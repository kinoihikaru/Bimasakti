using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000FRONT
{
    public partial class LookUpLineOfBusiness : R_Page
    {
        private LookUpLineOfBusinessViewModel loViewModel = new LookUpLineOfBusinessViewModel();
        private R_Grid<GetLineOfBusinessDTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
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
                await loViewModel.GetLineOfBusinessListAsync();

                eventArgs.ListEntityResult = loViewModel.loLineOfBusinessList;
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
