using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using LMM01000COMMON;

namespace LMM01000MODEL
{
    public class LMM01000ViewModel : R_ViewModel<LMM01000DTO>
    {
        private LMM01000Model _LMM01000Model = new LMM01000Model();
        private LMM01000UniversalModel _LMM01000UniversalModel = new LMM01000UniversalModel();

        public ObservableCollection<LMM01000UniversalDTO> ChargesTypeGrid { get; set; } = new ObservableCollection<LMM01000UniversalDTO>();
        public ObservableCollection<LMM01002DTO> ChargesUtilityGrid { get; set; } = new ObservableCollection<LMM01002DTO>();
        public List<LMM01000DTOPropety> PropertyList { get; set; } = new List<LMM01000DTOPropety>();

        public LMM01000DTO UtilityCharges = new LMM01000DTO();
        public LMM01000UniversalDTO ChargesType = new LMM01000UniversalDTO();

        public string PropertyValueContext = "";
        public string StatusChange = "00";

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000Model.GetPropertyAsync();
                PropertyList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetChargesTypeListAsync(poParam);

                ChargesTypeGrid = new ObservableCollection<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesType(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000Model.GetChargesTypeAsync(poParam);

                ChargesType = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtilityList(LMM01002DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);

                var loResult = await _LMM01000Model.GetChargesUtilityListAsync();

                ChargesUtilityGrid = new ObservableCollection<LMM01002DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtility(LMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM01000Model.R_ServiceGetRecordAsync(poParam);

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChargesUtility(LMM01000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteChargesUtility(LMM01000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync(LMM01000DTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.CSTATUS = StatusChange;

                await _LMM01000Model.LMM01000ActiveInactiveAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
