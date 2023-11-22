using GSM09100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using System.ComponentModel;
using R_BlazorFrontEnd.Helpers;
using GSM09100FrontResources;

namespace GSM09100MODEL
{
    public class GSM09100ViewModel : R_ViewModel<GSM09100DTO>
    {
        private GSM09100Model _GSM09100Model = new GSM09100Model();
        public List<GSM09100PropertyDTO> PropertyList { get; set; } = new List<GSM09100PropertyDTO>();
        public List<GSM09100DTO> ProductCategoryGrid { get; set; } = new List<GSM09100DTO>();
        public BindingList<GSM09100DTO> ProductCategoryList { get; set; } = new BindingList<GSM09100DTO>();
        public GSM09100InitialDTO InitialDTO { get; set; } = new GSM09100InitialDTO();
        public GSM09100DTO ProductCategory { get; set; } = new GSM09100DTO();

        public string PropertyValueContext = "";

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM09100Model.GetPropertyAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async  Task GetInitialDTO()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GSM09100InitialDTO();
                loParam.CPROPERTY_ID = PropertyValueContext;

                var loResult = await _GSM09100Model.GetInitialProductCategoryAsync(loParam);

                InitialDTO = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetProductCategoryList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM09100Model.GetProductCategoryListAsync(PropertyValueContext);
                ProductCategoryGrid = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetProductCategoryComboBoxList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM09100Model.GetProductCategoryListAsync(PropertyValueContext);
                ProductCategoryList = new BindingList<GSM09100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetProductCategory(GSM09100DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM09100Model.R_ServiceGetRecordAsync(poParam);

                ProductCategory = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationProductCategory(GSM09100DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CCATEGORY_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "901"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CCATEGORY_NAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "902"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveProductCategory(GSM09100DTO poParam, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poParam.CPROPERTY_ID = PropertyValueContext;
                }

                var loResult = await _GSM09100Model.R_ServiceSaveAsync(poParam, poCRUDMode);

                ProductCategory = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteProductCategory(GSM09100DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                 await _GSM09100Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
