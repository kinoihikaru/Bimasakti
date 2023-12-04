using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL
{
    public class UploadUnitPromotionTypeModel : R_BusinessObjectServiceClientBase<UploadUnitPromotionTypeDTO>, IUploadUnitPromotionType
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnitPromotionType";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitPromotionTypeModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public IAsyncEnumerable<UploadUnitPromotionTypeErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitPromotionTypeErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitPromotionTypeErrorDTO> loResult = null;
            UploadUnitPromotionTypeErrorResultDTO loRtn = new UploadUnitPromotionTypeErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitPromotionTypeErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitPromotionType.GetErrorProcessList),
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


        public IAsyncEnumerable<UploadUnitPromotionTypeDTO> GetUploadUnitPromotionTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitPromotionTypeResultDTO> GetUploadUnitPromotionTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitPromotionTypeDTO> loResult = null;
            UploadUnitPromotionTypeResultDTO loRtn = new UploadUnitPromotionTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitPromotionTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitPromotionType.GetUploadUnitPromotionTypeList),
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