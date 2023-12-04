using APM00200COMMON;
using APM00200COMMON.DTOs.APM00200;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APM00200MODEL
{
    public class APM00200Model : R_BusinessObjectServiceClientBase<APM00200ParameterDTO>, IAPM00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APM00200";
        private const string DEFAULT_MODULE = "AP";

        public APM00200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateExpenditureDTO DownloadTemplateExpenditure()
        {
            throw new NotImplementedException();
        }
        public async Task<TemplateExpenditureDTO> DownloadTemplateExpenditureAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateExpenditureDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateExpenditureDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.DownloadTemplateExpenditure),
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

        public IAsyncEnumerable<APM00200DTO> GetExpenditureList()
        {
            throw new NotImplementedException();
        }

        public async Task<APM00200ResultDTO> GetExpenditureListStreamAsync()
        {
            var loEx = new R_Exception();
            List<APM00200DTO> loResult = null;
            APM00200ResultDTO loRtn = new APM00200ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.GetExpenditureList),
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

        public IAsyncEnumerable<GetPropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPropertyResultDTO> GetPropertyListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetPropertyDTO> loResult = null;
            GetPropertyResultDTO loRtn = new GetPropertyResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.GetPropertyList),
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

        public GetSelectedExpenditureResultDTO GetSelectedExpenditure(GetSelectedExpenditureParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<GetSelectedExpenditureResultDTO> GetSelectedExpenditureAsync(GetSelectedExpenditureParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            GetSelectedExpenditureResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetSelectedExpenditureResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.GetSelectedExpenditure),
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

        public IAsyncEnumerable<GetWithholdingTaxTypeDTO> GetWithholdingTaxTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetWithholdingTaxTypeResultDTO> GetWithholdingTaxTypeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetWithholdingTaxTypeDTO> loResult = null;
            GetWithholdingTaxTypeResultDTO loRtn = new GetWithholdingTaxTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetWithholdingTaxTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.GetWithholdingTaxTypeList),
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


        public UpdateExpenditureActiveFlagResultDTO UpdateExpenditureActiveFlag(UpdateExpenditureActiveFlagParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<UpdateExpenditureActiveFlagResultDTO> UpdateExpenditureActiveFlagAsync(UpdateExpenditureActiveFlagParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            UpdateExpenditureActiveFlagResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<UpdateExpenditureActiveFlagResultDTO, UpdateExpenditureActiveFlagParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00200.UpdateExpenditureActiveFlag),
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
            return loRtn;
        }

    }
}
