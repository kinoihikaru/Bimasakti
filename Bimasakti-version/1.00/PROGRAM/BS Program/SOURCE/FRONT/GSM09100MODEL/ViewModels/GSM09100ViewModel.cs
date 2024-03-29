﻿using GSM09100COMMON;
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
using System.Linq;

namespace GSM09100MODEL
{
    public class GSM09100ViewModel : R_ViewModel<GSM09100DTO>
    {
        private GSM09100Model _GSM09100Model = new GSM09100Model();
        public List<GSM09100PropertyDTO> PropertyList { get; set; } = new List<GSM09100PropertyDTO>();
        public List<GSM09100TreeDTO> ProductCategoryGrid { get; set; } = new List<GSM09100TreeDTO>();
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

                var loGridData = loResult.Select(x =>
               new GSM09100TreeDTO
               {
                   ParentId = x.CPARENT,
                   ParentName = x.CPARENT_NAME,
                   Id = x.CCATEGORY_ID,
                   CCATEGORY_ID = x.CCATEGORY_ID,
                   Name = x.CCATEGORY_NAME,
                   Type = x.CCATEGORY_TYPE,
                   TypeDesc = x.CCATEGORY_TYPE_DESCR,
                   DisplayTree = string.Format("[{0}] {1} - {2}", x.ILEVEL, x.CCATEGORY_ID, x.CCATEGORY_NAME),
                   Description = x.CNOTE,
                   Level = x.ILEVEL
               }).ToList();

                ProductCategoryGrid = loGridData;
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
                poParam.CPROPERTY_ID = PropertyValueContext;
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
                else
                {
                    lCancel = poParam.CCATEGORY_ID.Length > 20;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "903"));
                    }
                }

                lCancel = string.IsNullOrEmpty(poParam.CCATEGORY_NAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "902"));
                }
                else
                {
                    lCancel = poParam.CCATEGORY_NAME.Length > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "904"));
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
