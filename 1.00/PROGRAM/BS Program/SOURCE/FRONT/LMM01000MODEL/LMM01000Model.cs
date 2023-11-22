using LMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01000Model : R_BusinessObjectServiceClientBase<LMM01000DTO>, ILMM01000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01000";
        private const string DEFAULT_MODULE = "LM";

        public LMM01000Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public LMM01000UniversalDTO GetChargesType()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM01000UniversalDTO> GetChargesTypeAsync()
        {
            var loEx = new R_Exception();
            LMM01000UniversalDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000.GetChargesType),
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

        public IAsyncEnumerable<LMM01002DTO> GetChargesUtilityList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01002DTO>> GetChargesUtilityListAsync()
        {
            var loEx = new R_Exception();
            List<LMM01002DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01002DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000.GetChargesUtilityList),
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

        public IAsyncEnumerable<LMM01000DTOPropety> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM01000DTOPropety>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<LMM01000DTOPropety> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000DTOPropety>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000.GetProperty),
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

        public LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM01000ActiveInactiveAsync(LMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000DTO, LMM01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000.LMM01000ActiveInactive),
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

        public LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM01000CopyNewChargesAsync(LMM01003DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01003DTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01003DTO, LMM01003DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000.LMM01000CopyNewCharges),
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
