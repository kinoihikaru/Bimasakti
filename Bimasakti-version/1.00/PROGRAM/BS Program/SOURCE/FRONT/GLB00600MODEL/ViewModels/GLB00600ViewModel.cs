using GLB00600COMMON;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLB00600MODEL
{
    public class GLB00600ViewModel
    {
        private GLB00600Model _GLB00600Model = new GLB00600Model();

        public GLB00600InitialDTO InitialVar = new GLB00600InitialDTO();
        public GLB00600GLSystemParamDTO SystemParam = new GLB00600GLSystemParamDTO();
        public GLB00600SuspenseAmountDTO SuspenseAmountDTO = new GLB00600SuspenseAmountDTO();
        public GLB00600GSMTransactionCodeDTO GSMTransactionCode = new GLB00600GSMTransactionCodeDTO();
        public List<GLB00600DTO> ResultClose = new List<GLB00600DTO>();

        public async Task GetInitialVar(GLB00600InitialDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB00600Model.GetInitialVarAsync(poParam);

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GLB00600GLSystemParamDTO> GetSystemParam()
        {
            var loEx = new R_Exception();
            GLB00600GLSystemParamDTO loRtn = null;
            try
            {
                var loResult = await _GLB00600Model.GetSystemParamAsync();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetSsuspenAmount(GLB00600SuspenseAmountDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB00600Model.GetInitialSupenseAmountAsync(poParam);

                SuspenseAmountDTO = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGSMTransactionCode()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB00600Model.GetInitialGSMTransactionCodeAsync();

                GSMTransactionCode = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGSMValidationResult()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB00600Model.GetValidationClosingResultAsync();

                ResultClose = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGSMResult(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _GLB00600Model.GetResultClosingEntriesAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
