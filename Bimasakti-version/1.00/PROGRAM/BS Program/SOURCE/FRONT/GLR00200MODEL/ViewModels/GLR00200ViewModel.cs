using GLR00200COMMON;
using GLR00200FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml;

namespace GLR00200MODEL
{
    public class GLR00200ViewModel : R_ViewModel<GLR00200PrintParamDTO>
    {
        private GLR00200Model _GLR00200Model = new GLR00200Model();

        public GLR00200InitialDTO InitialVar = new GLR00200InitialDTO();
        public GLR00200GLSystemParamDTO SystemParam = new GLR00200GLSystemParamDTO();
        public GLR00200GetMinMaxAccount MinMaxAccount = new GLR00200GetMinMaxAccount();
        public List<GLR00200UniversalDTO> GetPrintMethodList = new List<GLR00200UniversalDTO>();
        public BindingList<GLR00200PeriodDTO> PeriodFromToList = new BindingList<GLR00200PeriodDTO>();

        public string FromAccountName = "";
        public string ToAccountName = "";
        public string FromCenterName = "";
        public string ToCenterName = "";
        public bool LPrintbyCenter = false;
        public int IYEAR;
        public DateTime? IFROMDATE;
        public DateTime? ITODATE;

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLR00200Model.GetInitialVarAsync();

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
                var loResult = await _GLR00200Model.GetSystemParamAsync();

                SystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetMinMaxAccount()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLR00200Model.GetMinMaxAccountAsync();
                FromAccountName = loResult.CMIN_GLACCOUNT_NAME;
                ToAccountName = loResult.CMAX_GLACCOUNT_NAME;

                MinMaxAccount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPrintMethod()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLR00200Model.GetPrintMethodListAsync();

                GetPrintMethodList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPriod(int poYear)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLR00200Model.GetPriodListAsync(poYear.ToString());
                loResult.Add(new GLR00200PeriodDTO { CPERIOD_NO = "99", CPERIOD_NO_DISPLAY = "Closing" });

                PeriodFromToList = new BindingList<GLR00200PeriodDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationGLAccountLedger(GLR00200PrintParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CFROM_ACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2001"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CTO_ACCOUNT_NO);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2002"));
                }

                if (LPrintbyCenter)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_CENTER_CODE) ;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2003"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_CENTER_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2004"));
                    }
                }

                if (poParam.CPERIOD_MODE == "P")
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_PERIOD_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2005"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_PERIOD_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2006"));
                    }
                }
                else if (poParam.CPERIOD_MODE == "D")
                {
                    lCancel = string.IsNullOrEmpty(poParam.CFROM_DATE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2007"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CTO_DATE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err2008"));
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // Current Type Radio Button
        public List<RadioModel> CurrentTypeList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "L", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_LocalCurr")},
            new RadioModel { Id = "B", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_BaseCurr") },
        };

        // Period Type Radio Button
        public List<RadioModel> PeriodTypeList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "P", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Period")},
            new RadioModel { Id = "D", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Date") },
        };
    }

    public class RadioModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
