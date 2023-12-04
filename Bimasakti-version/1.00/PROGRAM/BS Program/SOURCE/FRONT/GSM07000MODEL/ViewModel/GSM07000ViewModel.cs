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
    public class GSM07000ViewModel : R_ViewModel<GSM07000DTO>
    {

        private GSM07000Model loModel = new GSM07000Model();

        public GSM07000DTO loActivityApproval = null;

        public ObservableCollection<GSM07000DTO> loActivityApprovalList = new ObservableCollection<GSM07000DTO>();

        public ActivityApprovalListDTO loRtn = null;

        public ApprovalListDTO loApprovalRtn = null;

        public List<ApprovalDTO> _approvalRtn = null;
/*        {
            new ApprovalDTO(){ CCODE = "1", CDESCRIPTION = "Approval", ICODE = 1},
            new ApprovalDTO(){ CCODE = "2", CDESCRIPTION = "With Approval", ICODE = 2},
            new ApprovalDTO(){ CCODE = "3", CDESCRIPTION = "No Approval", ICODE = 3}
        };
*/
        public async Task GetActivityApprovalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loRtn = await loModel.GetActivityApprovalListStreamAsync();
                loActivityApprovalList = new ObservableCollection<GSM07000DTO>(new List<GSM07000DTO>(loRtn.Data));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetApprovalListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loApprovalRtn = await loModel.GetApprovalListStreamAsync();
                _approvalRtn = new List<ApprovalDTO>(loApprovalRtn.Data);
/*
                _approvalRtn = new List<ApprovalDTO>()
                                {
                                    new ApprovalDTO(){ CCODE = "1", CDESCRIPTION = "Approval", ICODE = 1},
                                    new ApprovalDTO(){ CCODE = "2", CDESCRIPTION = "With Approval", ICODE = 2},
                                    new ApprovalDTO(){ CCODE = "3", CDESCRIPTION = "No Approval", ICODE = 3}
                                };*/

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetActivityApprovalAsync(GSM07000DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loActivityApproval = loResult;

                loActivityApproval.CAPPROVAL_OPTION = loActivityApproval.IAPPROVAL_OPTION.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveActivityApprovalAsync(GSM07000DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM07000DTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loActivityApproval = loResult;

                loActivityApproval.CAPPROVAL_OPTION = loActivityApproval.IAPPROVAL_OPTION.ToString();
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
