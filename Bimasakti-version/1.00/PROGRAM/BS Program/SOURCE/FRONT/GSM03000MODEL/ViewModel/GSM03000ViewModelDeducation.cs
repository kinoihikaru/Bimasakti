﻿using GSM03000Common.DTOs;
using GSM03000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM03000MODEL.ViewModel
{
    public class GSM03000ViewModelDeducation : R_ViewModel<GSM03000DTO>
    {
        private GSM03000Model _GSM03000Model = new GSM03000Model();
        public ObservableCollection<GSM03000DTO> OtherChargeList { get; set; } = new ObservableCollection<GSM03000DTO>();

        public GSM03000DTO OtherChargesDetail = new GSM03000DTO();

        public string PropertyValueContext = "";
        public bool StatusChange;
        private const string PropertyTypeContextDeducation = "D";

        public async Task GetOtherChargesListDeducation()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM03000Model.GetOtherChargesListAsync(PropertyValueContext, PropertyTypeContextDeducation);

                OtherChargeList = new ObservableCollection<GSM03000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetOtherChargesDetail(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = PropertyValueContext;
                poParam.CCHARGES_TYPE = PropertyTypeContextDeducation;
                var loResult = await _GSM03000Model.R_ServiceGetRecordAsync(poParam);

                OtherChargesDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationOtherChargesDeducation(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "3009"));
                }
                else
                {
                    lCancel = poParam.CCHARGES_ID.Length > 20;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "3012"));
                    }
                }
                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_NAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "3010"));
                }
                else
                {
                    lCancel = poParam.CCHARGES_NAME.Length > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "3013"));
                    }
                }
                lCancel = string.IsNullOrEmpty(poParam.CGLACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "3011"));
                }
                
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveOtherCharges(GSM03000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPROPERTY_ID = PropertyValueContext;
                poNewEntity.CCHARGES_TYPE = PropertyTypeContextDeducation;
                var loResult = await _GSM03000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                OtherChargesDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteOtherCharges(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM03000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSM03000DTO> ActiveInactiveProcessAsync(GSM03000ActiveParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            GSM03000DTO loRtn = null;

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.CCHARGES_TYPE = PropertyTypeContextDeducation;
                poParameter.LACTIVE = StatusChange;

                await _GSM03000Model.GSM03000OtherChargesChangeStatusAsync(poParameter);

                var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000DTO>(poParameter);

                loRtn = await _GSM03000Model.R_ServiceGetRecordAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        // Status Radio Button
        public List<RadioModelDecucation> OptionsStatus { get; set; } = new List<RadioModelDecucation>
        {
            new RadioModelDecucation { Id = "00", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ValidationDeducationChargesIdReq")},
            new RadioModelDecucation { Id = "80", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ValidationDeducationChargesIdReq") },
            new RadioModelDecucation { Id = "90", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ValidationDeducationChargesIdReq") },
        };
    }

    public class RadioModelDecucation
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
