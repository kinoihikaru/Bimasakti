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

        public GSM03000DTO OtherChargesDetail = new GSM03000DTO();

        public string PropertyValueContext = "";
        public string StatusChange = "";
        private string PropertyTypeContextDeducation = "D";

        public async Task GetOtherChargesListDeducation()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, PropertyTypeContextDeducation);

                var loResult = await _GSM03000Model.GetOtherChargesListAsync();

                OtherChargeList = new ObservableCollection<GSM03000DTO>(loResult.Data);
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
                if (string.IsNullOrEmpty(poParam.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                }
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, PropertyTypeContextDeducation);

                var loResult = await _GSM03000Model.R_ServiceGetRecordAsync(poParam);

                OtherChargesDetail = loResult;
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
        public async Task ActiveInactiveProcessAsync(GSM03000ActiveParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.CSTATUS = StatusChange;
                poParameter.CCHARGES_TYPE = PropertyTypeContextDeducation;

                await _GSM03000Model.GSM03000OtherChargesChangeStatusAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
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
