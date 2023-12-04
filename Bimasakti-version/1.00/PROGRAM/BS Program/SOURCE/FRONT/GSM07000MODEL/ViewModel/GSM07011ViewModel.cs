using GSM07000COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace GSM07000MODEL.ViewModel
{
    public class GSM07011ViewModel : R_ViewModel<GSM07011DTO>
    {
        private GSM07011Model loModel = new GSM07011Model();

        public GSM07011DTO loMultipleUserAssignment = null;

        public ObservableCollection<GSM07011DTO> loMultipleUserAssignmentList = new ObservableCollection<GSM07011DTO>();

        public List<GSM07011DTO> loSelectedUser = null;

        public GSM07011ResultDTO loRtn = null;

        public string GSM07011_SELECTED_APPROVAL_CODE = "";

        public string GSM07011_SELECTED_USER_ID = "";

        public async Task GetMultipleUserAssignmentListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM07011_CAPPROVAL_CODE_STREAMING_CONTEXT, GSM07011_SELECTED_APPROVAL_CODE);
                loRtn = await loModel.GetMultipleUserAssignmentListStreamAsync();
                loMultipleUserAssignmentList = new ObservableCollection<GSM07011DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUserForMultipleUserAssigment(GSM07011DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM07011ParameterDTO loParam = null;
            GSM07011ParameterDTO loResult = null;

            try
            {
                GSM07011_SELECTED_USER_ID = loMultipleUserAssignment.CUSER_ID;;
                //R_FrontContext.R_SetContext(ContextConstant.GSM07011_SELECTED_USER_ID_CONTEXT, GSM07011_SELECTED_USER_ID);

                loParam = new GSM07011ParameterDTO()
                {
                    Data = poEntity,
                    SELECTED_APPROVAL_CODE = GSM07011_SELECTED_APPROVAL_CODE
                };
                    
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loMultipleUserAssignment = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveMultipleUserAssignmentAsync()
        {
            R_Exception loException = new R_Exception();

            try
            {
                SaveMultipleUserAssignmentParameterDTO loParam = new SaveMultipleUserAssignmentParameterDTO();

                loParam.CSELECTED_USER_ID_LIST = new List<string>();

                foreach (var item in loSelectedUser)
                {
                    if (item.LSELECTED)
                    {
                        loParam.CSELECTED_USER_ID_LIST.Add(item.CUSER_ID);
                    }
                }
                loParam.CAPPROVAL_CODE = GSM07011_SELECTED_APPROVAL_CODE;

                await loModel.SaveMultipleUserAssignmentAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveActivityApprovalUserAsync(GSM07011DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM07011ParameterDTO loParam = null;
            GSM07011ParameterDTO loResult = null;

            try
            {
                loParam = new GSM07011ParameterDTO()
                {
                    Data = poEntity,
                    SELECTED_APPROVAL_CODE = GSM07011_SELECTED_APPROVAL_CODE
                };

                //R_FrontContext.R_SetContext(ContextConstant.GSM07011_SELECTED_APPROVAL_CODE_CONTEXT, GSM07011_SELECTED_APPROVAL_CODE);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loMultipleUserAssignment = loResult.Data;
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
