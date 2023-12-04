using GLB09900COMMON;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLB09900MODEL
{
    public class GLB09900ViewModel
    {
        private GLB09900Model _GLB09900Model = new GLB09900Model();

        public GLB09900InitialDTO InitialVar = new GLB09900InitialDTO();
        public GLB09900GLSystemParamDTO SystemParam = new GLB09900GLSystemParamDTO();
        public GLB09900ValidateDTO ValidateResultClose = new GLB09900ValidateDTO();
        public GLB09900DTO ResultClose = new GLB09900DTO();

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB09900Model.GetInitialVarAsync();

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB09900Model.GetSystemParamAsync();

                SystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> GetValidateResultClosing(GLB09900ValidateDTO poParam)
        {
            var loEx = new R_Exception();
            bool loRtn = false;
            try
            {
                var loResult = await _GLB09900Model.GetValidateResultCloseGlPeriodAsync(poParam);

                if (loResult.IERROR_COUNT > 0)
                {
                    List<string> ErrorMessage = new List<string>(loResult.CERROR_MESSAGE.Split('|'));

                    foreach (var item in ErrorMessage)
                    {
                        loEx.Add("", item);
                    }

                    loRtn = false;
                }
                else
                {
                    loRtn = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task GetResultClosing(GLB09900DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLB09900Model.GetResultCloseGlPeriodAsync(poParam);

                ResultClose = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
