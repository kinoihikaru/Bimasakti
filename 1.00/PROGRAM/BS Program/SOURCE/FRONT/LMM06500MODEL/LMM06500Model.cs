using R_BusinessObjectFront;
using LMM06500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;

namespace LMM06500MODEL
{
    public class LMM06500Model : R_BusinessObjectServiceClientBase<LMM06500DTO>, ILMM06500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06500";
        private const string DEFAULT_MODULE = "LM";
        

        public LMM06500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM06500List<LMM06500DTOInitial> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500List<LMM06500DTOInitial>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500DTOInitial> loResult = new LMM06500List<LMM06500DTOInitial>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06500DTOInitial>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.GetProperty),
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

        public LMM06500List<LMM06500DTO> GetStaffList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM06500List<LMM06500DTO>> GetStaffListAsync()
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500DTO> loResult = new LMM06500List<LMM06500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06500DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.GetStaffList),
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

        public LMM06500DTO LMM06500ActiveInactive(LMM06500DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task LMM06500ActiveInactiveAsync(LMM06500DTO poParam)
        {
            var loEx = new R_Exception();
            LMM06500DTO loRtn = new LMM06500DTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500DTO, LMM06500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500.LMM06500ActiveInactive),
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
