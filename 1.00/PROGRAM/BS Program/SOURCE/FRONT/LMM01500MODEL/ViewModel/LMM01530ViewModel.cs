using LMM01500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01530ViewModel : R_ViewModel<LMM01530DTO>
    {
        private LMM01530Model _LMM01530Model = new LMM01530Model();

        public ObservableCollection<LMM01530DTO> OtherChargesGrid { get; set; } = new ObservableCollection<LMM01530DTO>();

        public LMM01530DTO OtherCharges = new LMM01530DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode = "";
        public string InvGrpName = "";

        public async Task GetOtherChargesList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01530Model.GetAllOtherChargerListAsync(PropertyValueContext, InvGrpCode);

                OtherChargesGrid = new ObservableCollection<LMM01530DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOtherCharges(LMM01530DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                var loResult = await _LMM01530Model.R_ServiceGetRecordAsync(poParam);

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOtherCharges(LMM01530DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01530Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                OtherCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteOtherCharges(LMM01530DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01530Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
