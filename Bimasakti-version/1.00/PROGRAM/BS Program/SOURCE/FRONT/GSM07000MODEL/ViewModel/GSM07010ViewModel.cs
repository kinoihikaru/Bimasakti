using GSM07000COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM07000MODEL.ViewModel
{
    public class GSM07010ViewModel : R_ViewModel<GSM07010DTO>
    {
        private GSM07010Model loModel = new GSM07010Model();

        public GSM07010DTO loActivityApprovalUser = null;

        public ObservableCollection<GSM07010DTO> loActivityApprovalUserList = new ObservableCollection<GSM07010DTO>();

        public ActivityApprovalUserListDTO loRtn = null;

        public GSM07010UserListDTO loUserRtn = null;

        public List<GSM07010UserDTO> loUserList = null;

        public string SELECTED_APPROVAL_CODE = "";

        public async Task GetActivityApprovalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.SELECTED_APPROVAL_CODE_STREAMING_CONTEXT, SELECTED_APPROVAL_CODE);
                loRtn = await loModel.GetActivityApprovalUserListStreamAsync();
                loActivityApprovalUserList = new ObservableCollection<GSM07010DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetActivityApprovalUserAsync(GSM07010DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM07010ParameterDTO loParam = null;
            GSM07010ParameterDTO loResult = null;

            try
            {
                loParam = new GSM07010ParameterDTO()
                {
                    Data = poEntity,
                    SELECTED_APPROVAL_CODE = SELECTED_APPROVAL_CODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT, SELECTED_APPROVAL_CODE);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loActivityApprovalUser = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteAssignedUser(GSM07010DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM07010ParameterDTO loParam = null;

            try
            {
                loParam = new GSM07010ParameterDTO()
                {
                    Data = poEntity,
                    SELECTED_APPROVAL_CODE = SELECTED_APPROVAL_CODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT, SELECTED_APPROVAL_CODE);
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveActivityApprovalUserAsync(GSM07010DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM07010ParameterDTO loParam = null;
            GSM07010ParameterDTO loResult = null;

            try
            {
                loParam = new GSM07010ParameterDTO()
                {
                    Data = poEntity,
                    SELECTED_APPROVAL_CODE = SELECTED_APPROVAL_CODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT, SELECTED_APPROVAL_CODE);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loActivityApprovalUser = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
