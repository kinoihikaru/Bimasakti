using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500MODEL
{
    public class GSM01510Model : R_BusinessObjectServiceClientBase<GSM01510DepartmentDTO>, IGSM01510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM01510";
        private const string DEFAULT_MODULE = "gs";

        public GSM01510Model(string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM01510DepartmentDTO> GetCenterDepartmentList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM01510DepartmentListDTO> GetCenterDepartmentListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM01510DepartmentDTO> loResult = null;
            GSM01510DepartmentListDTO loRtn = new GSM01510DepartmentListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM01510DepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM01510.GetCenterDepartmentList),
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
