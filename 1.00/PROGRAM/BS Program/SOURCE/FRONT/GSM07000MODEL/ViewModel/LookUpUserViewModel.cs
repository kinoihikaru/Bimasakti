using GSM07000COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM07000MODEL.ViewModel
{
    public class LookUpUserViewModel : R_ViewModel<GSM07010UserDTO>
    {
        private GSM07010Model loModel = new GSM07010Model();

        public ObservableCollection<GSM07010UserDTO> loUserList = new ObservableCollection<GSM07010UserDTO>();

        public GSM07010UserListDTO loUserRtn = null;

        public string SELECTED_APPROVAL_CODE = "";


        public async Task GetLookUpUserListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.SELECTED_APPROVAL_CODE_STREAMING_CONTEXT, SELECTED_APPROVAL_CODE);
                loUserRtn = await loModel.GetLookUpUserListStreamAsync();
                loUserList = new ObservableCollection<GSM07010UserDTO>(loUserRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }
}
