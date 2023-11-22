using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03507;

namespace LMM03500MODEL
{
    public class LMM03507Model : R_BusinessObjectServiceClientBase<LMM03507ParameterDTO>, ILMM03507
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03507";
        private const string DEFAULT_MODULE = "lm";

        public LMM03507Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetIdTypeDTO> GetIdTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetIdTypeResultDTO> GetIdTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetIdTypeDTO> loResult = null;
            GetIdTypeResultDTO loRtn = new GetIdTypeResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetIdTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03507.GetIdTypeList),
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

        public IAsyncEnumerable<LMM03507DTO> GetMembersList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM03507ResultDTO> GetMembersListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<LMM03507DTO> loResult = null;
            LMM03507ResultDTO loRtn = new LMM03507ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03507DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03507.GetMembersList),
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