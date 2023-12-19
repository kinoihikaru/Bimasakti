using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using GSM05000COMMON_FMC;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05020Model : R_BusinessObjectServiceClientBase<GSM05020DTO>, IGSM05020
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05020";
        private const string DEFAULT_MODULE = "GS";

        public GSM05020Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM05020DTO>> GetApprovalListStreamAsync(string poTransactionCodeParam, string poDeptCode = "")
        {
            var loEx = new R_Exception();
            List<GSM05020DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANSACTION_CODE, poTransactionCodeParam);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poDeptCode);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05020DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.GetApprovalListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<GSM05020DeptDTO>> GetDeptTransListStreamAsync(string poTransactionCodeParam)
        {
            var loEx = new R_Exception();
            List<GSM05020DeptDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANSACTION_CODE, poTransactionCodeParam);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05020DeptDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.GetDeptTransListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<GSM05020ValidateDTO> ValidateApprovalReplacementAsync(GSM05020ValidateDTO poEntity)
        {
            var loEx = new R_Exception();
            GSM05020ValidateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05020ValidateDTO>, GSM05020ValidateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.ValidateApprovalReplacement),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task CopyFromApprovalAsync(GSM05020ParamFromToDeptDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05020ParamFromToDeptDTO>, GSM05020ParamFromToDeptDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.CopyFromApproval),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task CopyToApprovalAsync(GSM05020ParamFromToDeptDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05020ParamFromToDeptDTO>, GSM05020ParamFromToDeptDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.CopyToApproval),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task BatchSeqApprovalAsync(GSM05000ParameterWithCRUDMode<GSM05020DTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05020DTO>, GSM05000ParameterWithCRUDMode<GSM05020DTO>> (
                    _RequestServiceEndPoint,
                    nameof(IGSM05020.BatchSeqApproval),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Not Implement
        public IAsyncEnumerable<GSM05020DTO> GetApprovalListStream()
        {
            throw new NotImplementedException();
        }

        public GSM05000SingleResult<GSM05020ValidateDTO> ValidateApprovalReplacement(GSM05020ValidateDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyToApproval(GSM05020ParamFromToDeptDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyFromApproval(GSM05020ParamFromToDeptDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM05020DeptDTO> GetDeptTransListStream()
        {
            throw new NotImplementedException();
        }

        public GSM05000SingleResult<GSM05020DTO> BatchSeqApproval(GSM05000ParameterWithCRUDMode<GSM05020DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}