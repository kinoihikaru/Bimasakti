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
    public class GSM07000Model : R_BusinessObjectServiceClientBase<GSM07000DTO>, IGSM07000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM07000";
        private const string DEFAULT_MODULE = "gs";

        public GSM07000Model(string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM07000DTO> GetActivityApprovalList()
        {
            throw new NotImplementedException();
        }
        public async Task<ActivityApprovalListDTO> GetActivityApprovalListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM07000DTO> loResult = null;
            ActivityApprovalListDTO loRtn = new ActivityApprovalListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM07000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM07000.GetActivityApprovalList),
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

        public IAsyncEnumerable<ApprovalDTO> GetApprovalList()
        {
            throw new NotImplementedException();
        }
        public async Task<ApprovalListDTO> GetApprovalListStreamAsync()
        {
            var loEx = new R_Exception();
            List<ApprovalDTO> loResult = null;
            ApprovalListDTO loRtn = new ApprovalListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ApprovalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM07000.GetApprovalList),
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
