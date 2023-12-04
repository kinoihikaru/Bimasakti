using GSM07000COMMON.DTOs;
using GSM07000COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace GSM07000MODEL
{
    public class GSM07011Model : R_BusinessObjectServiceClientBase<GSM07011ParameterDTO>, IGSM07011
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM07011";
        private const string DEFAULT_MODULE = "gs";

        public GSM07011Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM07011DTO> GetMultipleUserAssignmentList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM07011ResultDTO> GetMultipleUserAssignmentListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM07011DTO> loResult = null;
            GSM07011ResultDTO loRtn = new GSM07011ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM07011DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM07011.GetMultipleUserAssignmentList),
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

        public SaveMultipleUserAssignmentDTO SaveMultipleUserAssignment(SaveMultipleUserAssignmentParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task SaveMultipleUserAssignmentAsync(SaveMultipleUserAssignmentParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            SaveMultipleUserAssignmentDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SaveMultipleUserAssignmentDTO, SaveMultipleUserAssignmentParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM07011.SaveMultipleUserAssignment),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
