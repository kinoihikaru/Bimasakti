using APM00300COMMON;
using APM00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace APM00300MODEL
{
    public class APM00300ViewModel :R_ViewModel<APM00300DTO>
    {
        private APM00300Model _APM00300Model = new APM00300Model();

        public ObservableCollection<APM00300DTO> SupplierGrid { get; set; } = new ObservableCollection<APM00300DTO>();
        public List<APM00300PropertyDTO> PropertyList { get; set; } = new List<APM00300PropertyDTO>();
        public List<APM00300LOBDTO> LOBList { get; set; } = new List<APM00300LOBDTO>();

        public APM00300InitialDTO Initial = new APM00300InitialDTO();

        public string PropertyValueContext { get; set; }
        public string LOBValueContext { get; set; } = "";
        public string SearchTextValueContext { get; set; } = "";
        public bool EnableTabDetail { get; set; } = false;

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APM00300Model.GetInitialVarAsync();

                Initial = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00300Model.GetGSPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetLOBList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00300Model.GetLOBListAsync();

                LOBList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task SearchSupplierList()
        {
            var loEx = new R_Exception();

            try
            {
                APM00300DTO loParam = new APM00300DTO();
                loParam.CPROPERTY_ID = PropertyValueContext;
                loParam.CLOB_CODE = LOBValueContext;
                loParam.CSEARCH_TEXT = SearchTextValueContext;

                var loResult = await _APM00300Model.GetAPSearchSupplierListAsync(loParam);
                EnableTabDetail = loResult.Count > 0;

                SupplierGrid = new ObservableCollection<APM00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationSearch()
        {
            var loEx = new R_Exception();
            bool llCancel = false;

            try
            {
                llCancel = string.IsNullOrWhiteSpace(PropertyValueContext);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "301"));
                }

                llCancel = string.IsNullOrWhiteSpace(SearchTextValueContext);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "302"));
                }
                else
                {
                    if (SearchTextValueContext.Length < 3)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "303"));
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
    }
}
