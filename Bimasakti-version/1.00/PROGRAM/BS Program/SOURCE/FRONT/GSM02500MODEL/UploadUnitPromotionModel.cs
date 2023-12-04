using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL
{
    public class UploadUnitPromotionModel : R_BusinessObjectServiceClientBase<UploadUnitPromotionDTO>, IUploadUnitPromotion
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnitPromotion";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitPromotionModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<UploadUnitPromotionErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitPromotionErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitPromotionErrorDTO> loResult = null;
            UploadUnitPromotionErrorResultDTO loRtn = new UploadUnitPromotionErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitPromotionErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitPromotion.GetErrorProcessList),
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

        public IAsyncEnumerable<UploadUnitPromotionDTO> GetUploadUnitPromotionList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitPromotionResultDTO> GetUploadUnitPromotionListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitPromotionDTO> loResult = null;
            UploadUnitPromotionResultDTO loRtn = new UploadUnitPromotionResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitPromotionDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitPromotion.GetUploadUnitPromotionList),
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