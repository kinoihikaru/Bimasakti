using GSM07000COMMON;
using GSM07000COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM07000MODEL
{
    public class GSM07010Model : R_BusinessObjectServiceClientBase<GSM07010ParameterDTO>, IGSM07010
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM07010";
        private const string DEFAULT_MODULE = "gs";

        public GSM07010Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM07010DTO> GetActivityApprovalUserList()
        {
            throw new NotImplementedException();
        }

        public async Task<ActivityApprovalUserListDTO> GetActivityApprovalUserListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM07010DTO> loResult = null;
            ActivityApprovalUserListDTO loRtn = new ActivityApprovalUserListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM07010DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM07010.GetActivityApprovalUserList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<GSM07010UserDTO> GetLookUpUserList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM07010UserListDTO> GetLookUpUserListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM07010UserDTO> loResult = null;
            GSM07010UserListDTO loRtn = new GSM07010UserListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM07010UserDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM07010.GetLookUpUserList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

    }
}
