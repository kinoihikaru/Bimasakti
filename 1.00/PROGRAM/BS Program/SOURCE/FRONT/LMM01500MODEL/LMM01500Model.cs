using R_BusinessObjectFront;
using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;

namespace LMM01500MODEL
{
    public class LMM01500Model : R_BusinessObjectServiceClientBase<LMM01500DTO>, ILMM01500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01500";
        private const string DEFAULT_MODULE = "LM";

        public LMM01500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM01500List<LMM01501DTO> GetInvoiceGrpList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500List<LMM01501DTO>> GetInvoiceGrpListAsync()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01501DTO> loResult = new LMM01500List<LMM01501DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01501DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetInvoiceGrpList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public LMM01500List<LMM01500DTOPropety> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500List<LMM01500DTOPropety>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01500DTOPropety> loResult = new LMM01500List<LMM01500DTOPropety>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01500DTOPropety>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.GetProperty),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM01500ActiveInactiveAsync(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01500DTO loRtn = new LMM01500DTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500DTO, LMM01500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.LMM01500ActiveInactive),
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

        public LMM01500List<LMM01502DTO> LMM01500LookupBank()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500List<LMM01502DTO>> LMM01500LookupBankAsync()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01502DTO> loResult = new LMM01500List<LMM01502DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01502DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500.LMM01500LookupBank),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
