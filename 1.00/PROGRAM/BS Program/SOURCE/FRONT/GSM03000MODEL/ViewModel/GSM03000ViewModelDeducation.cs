using GSM03000Common.DTOs;
using GSM03000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM03000MODEL.ViewModel
{
    public class GSM03000ViewModelDeducation : R_ViewModel<GSM03000DTO>
    {
        private GSM03000Model _GSM03000Model = new GSM03000Model();
        public ObservableCollection<GSM03000DTO> OtherChargeList { get; set; } = new ObservableCollection<GSM03000DTO>();
        public List<GSM03000PropertyDTO> PropertyList { get; set; } = new List<GSM03000PropertyDTO>();

        public GSM03000DTO OtherChargesDetail = new GSM03000DTO();

        public string PropertyValue = "";
        private string PropertyTypeContextDeducation = "D";

        public async Task GetOtherChargesList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM03000Model.GetOtherChargesListAsync(PropertyValue, PropertyTypeContextDeducation);

                OtherChargeList = new ObservableCollection<GSM03000DTO>(loResult.Data);
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
                var loResult = await _GSM03000Model.GetPropertyAsync();
                PropertyList = loResult.Data;
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
                var loResult = await _GSM03000Model.R_ServiceGetRecordAsync(poParam);

                OtherChargesDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_SaveValidation(GSM03000DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poEntity.CPROPERTY_ID = PropertyValue;
                    poEntity.CCHARGES_TYPE = PropertyTypeContextDeducation;
                    var loResult = await _GSM03000Model.R_ServiceGetRecordAsync(poEntity);
                    if (loResult != null)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "3002");
                        loEx.Add(loErr);

                    }
                }

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
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValue;
                    poNewEntity.CCHARGES_TYPE = PropertyTypeContextDeducation;
                }

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
                // Validation Before Delete
                if (poEntity.CSTATUS != "00")
                {
                    var loErr = R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "3003");

                    loEx.Add(loErr);
                }

                await _GSM03000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // Status Radio Button

        public List<RadioModelDecucation> OptionsStatus { get; set; } = new List<RadioModelDecucation>
        {
            new RadioModelDecucation { Id = "00", Text = "Draft"},
            new RadioModelDecucation { Id = "80", Text = "Active" },
            new RadioModelDecucation { Id = "90", Text = "Inactive" },
        };
    }

    public class RadioModelDecucation
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
