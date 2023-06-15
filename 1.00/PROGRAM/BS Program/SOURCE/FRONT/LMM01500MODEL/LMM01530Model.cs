using R_BusinessObjectFront;
using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;
using System.Data.Common;

namespace LMM01500MODEL
{
    public class LMM01530Model : R_BusinessObjectServiceClientBase<LMM01530DTO>, ILMM01530
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01530";
        private const string DEFAULT_MODULE = "LM";

        public LMM01530Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

       
        public LMM01500List<LMM01530DTO> GetAllOtherChargerList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM01500List<LMM01530DTO>> GetAllOtherChargerListAsync()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01530DTO> loResult = new LMM01500List<LMM01530DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01530DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01530.GetAllOtherChargerList),
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


        public LMM01500List<LMM01531DTO> GetOtherChargesLookup(LMM01531DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500List<LMM01531DTO>> GetOtherChargesLookupAsync(LMM01531DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01531DTO> loResult = new LMM01500List<LMM01531DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01531DTO>, LMM01531DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01530.GetOtherChargesLookup),
                    poParam,
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
