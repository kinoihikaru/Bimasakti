using APT00300COMMON;
using APT00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APT00300MODEL
{
    public class APT00320ViewModel : R_ViewModel<APT00311DTO>
    {
        private APT00310Model _APT00310Model = new APT00310Model();
        private APT00320Model _APT00320Model = new APT00320Model();
        private APT00300UniversalModel _APT00300UniversalModel = new APT00300UniversalModel();

        #region Property View Model
        public string Parent_ID { get; set; } = "";
        public string Dept_Code { get; set; } = "";
        public string Ref_Id { get; set; } = "";
        public APT00311DTO PurchaseDebitDT { get; set; } = new APT00311DTO();
        #endregion

        #region Initial Process
        public List<APT00300GSBCodeDTO> GSBCodeList { get; set; } = new List<APT00300GSBCodeDTO>();
        public async Task GetAllInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00300UniversalModel.GetInitialGSBCodeListStreamAsync();

                GSBCodeList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Combo Box or Radio Group Helper List
        public List<KeyValuePair<string, string>> DiscountTypeOptions { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("P", "%"),
            new KeyValuePair<string, string>("V", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Amount"))
        };

        public LinkedList<KeyValuePair<int, string>> PurchaseUnitOptions { get; set; } = new LinkedList<KeyValuePair<int, string>>();
        #endregion

        public async Task GetPurchaseDebitDT(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00320Model.R_ServiceGetRecordAsync(poEntity);

                var loDataListUnit = new List<KeyValuePair<int, string>>();
                if (!string.IsNullOrWhiteSpace(loResult.CUNIT1))
                    loDataListUnit.Add(new KeyValuePair<int, string>(1, loResult.CUNIT1));

                if (!string.IsNullOrWhiteSpace(loResult.CUNIT2))
                    loDataListUnit.Add(new KeyValuePair<int, string>(2, loResult.CUNIT2));

                if (!string.IsNullOrWhiteSpace(loResult.CUNIT3))
                    loDataListUnit.Add(new KeyValuePair<int, string>(3, loResult.CUNIT3));

                loDataListUnit.Add(new KeyValuePair<int, string>(4, R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Other")));

                PurchaseUnitOptions = new LinkedList<KeyValuePair<int, string>>(loDataListUnit);

                PurchaseDebitDT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeletePurchaseDebitDT(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00320Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task InvoiceItemValidation(APT00311DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_DEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err19"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_TYPE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err20"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err21"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "E";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err22"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CALLOC_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err23"));
                }

                llCancel = poParam.NTRANS_QTY <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err24"));
                }

                llCancel = poParam.IBILL_UNIT == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err25"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CBILL_UNIT) && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err26"));
                }

                llCancel = poParam.NSUPP_CONV_FACTOR <= 0 && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err27"));
                }

                llCancel = poParam.NUNIT_PRICE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err28"));
                }

                llCancel = poParam.CDISC_TYPE == "P" && (poParam.NDISC_PCT < 0 || poParam.NDISC_PCT > 100);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err29"));
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT < 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err30"));
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT > poParam.NAMOUNT;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err31"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SavePurchaseDebitDT(APT00311DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CPARENT_ID = Parent_ID;
                    poEntity.CDEPT_CODE = Dept_Code;
                    poEntity.CREF_NO = Ref_Id;
                }
                var loResult = await _APT00320Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                PurchaseDebitDT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
