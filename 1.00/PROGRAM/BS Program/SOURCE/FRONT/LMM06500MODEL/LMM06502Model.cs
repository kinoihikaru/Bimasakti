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
    public class LMM06502Model : R_BusinessObjectServiceClientBase<LMM06502DTO>, ILMM06502
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06502";
        private const string DEFAULT_MODULE = "LM";

        public LMM06502Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM06500List<LMM06502DetailDTO> GetStaffMoveList(LMM06502DetailDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public async Task<LMM06500List<LMM06502DetailDTO>> GetStaffMoveListAsync(LMM06502DetailDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06502DetailDTO> loRtn = new LMM06500List<LMM06502DetailDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06502DetailDTO>, LMM06502DetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06502.GetStaffMoveList),
                    poEntity,
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

        public LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity)
        {
            throw new NotImplementedException();
        }
        public async Task SaveNewMoveStaffAsync(LMM06502DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM06502DTO loRtn = new LMM06502DTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06502DTO, LMM06502DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06502.SaveNewMoveStaff),
                    poEntity,
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
