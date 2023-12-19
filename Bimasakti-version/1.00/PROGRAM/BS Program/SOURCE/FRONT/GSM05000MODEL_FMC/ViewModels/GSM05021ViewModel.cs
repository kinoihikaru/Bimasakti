using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GSM05000COMMON_FMC;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05021ViewModel : R_ViewModel<GSM05021DTO>
    {
        private GSM05021Model _GSM05021Model = new GSM05021Model();
        public ObservableCollection<GSM05021DTO> GridList = new ObservableCollection<GSM05021DTO>();
        public GSM05021DTO Entity = new GSM05021DTO();
        public async Task GetApprovalReplacementList(GSM05021DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05021Model.GetApprovalReplacementListStreamAsync(poParam);
                loReturn.ForEach(x => 
                {
                    x.DVALID_TO = DateTime.ParseExact(x.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
                    x.DVALID_FROM = DateTime.ParseExact(x.CVALID_FROM, "yyyyMMdd", CultureInfo.InvariantCulture);
                });

                GridList = new ObservableCollection<GSM05021DTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntityApprovalReplacement(GSM05021DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM05021Model.R_ServiceGetRecordAsync(poEntity);
                loResult.DVALID_TO = DateTime.ParseExact(loResult.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
                loResult.DVALID_FROM = DateTime.ParseExact(loResult.CVALID_FROM, "yyyyMMdd", CultureInfo.InvariantCulture);

                Entity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveEntity(GSM05021DTO poNewEntity, eCRUDMode peCrudMode)
        {
            var loEx = new R_Exception();
            try
            {
                Entity = await _GSM05021Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteEntity(GSM05021DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05021Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}

