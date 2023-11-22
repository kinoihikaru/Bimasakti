using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM07000COMMON.DTOs;
using GSM07000MODEL.ViewModel;
using R_BlazorFrontEnd;

namespace GSM07000FRONT
{
    public partial class LookUpUser : R_Page
    {
        private LookUpUserViewModel loUserViewModel = new LookUpUserViewModel();
        private R_Grid<GSM07010UserDTO> _gridUserRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUserViewModel.SELECTED_APPROVAL_CODE = (string)poParameter;
                await _gridUserRef.R_RefreshGrid(null);
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
                await loUserViewModel.GetLookUpUserListStreamAsync();

                eventArgs.ListEntityResult = loUserViewModel.loUserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loData = _gridUserRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }

    }
}