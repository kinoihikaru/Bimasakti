using GSM09200COMMON;
using GSM09200COMMON.DTOs.GSM09210;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09200MODEL
{
    public class GSM09210Model : R_BusinessObjectServiceClientBase<GSM09210DTO>, IGSM09210
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09210";
        private const string DEFAULT_MODULE = "GS";

        public GSM09210Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetExpenditureCategoryResultDTO GetExpenditureCategory()
        {
            throw new NotImplementedException();
        }
        public async Task<GetExpenditureCategoryResultDTO> GetExpenditureCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            GetExpenditureCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetExpenditureCategoryResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09210.GetExpenditureCategory),
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
            return loRtn;
        }

        public IAsyncEnumerable<GSM09210DTO> GetExpenditureList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09210ResultDTO> GetExpenditureListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09210DTO> loResult = null;
            GSM09210ResultDTO loRtn = new GSM09210ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09210DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09210.GetExpenditureList),
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

        public MoveExpenditureCategoryResultDTO MoveExpenditureCategory(MoveExpenditureCategoryParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task MoveExpenditureCategoryAsync(MoveExpenditureCategoryParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            MoveExpenditureCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<MoveExpenditureCategoryResultDTO, MoveExpenditureCategoryParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09210.MoveExpenditureCategory),
                    poParam,
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
