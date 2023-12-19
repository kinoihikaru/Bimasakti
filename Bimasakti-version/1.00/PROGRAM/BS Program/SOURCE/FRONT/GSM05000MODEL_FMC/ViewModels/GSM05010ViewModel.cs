using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM05000COMMON_FMC;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05010ViewModel : R_ViewModel<GSM05010DTO>
    {
        private GSM05010Model _GSM05010Model = new GSM05010Model();
        public ObservableCollection<GSM05010DTO> GridList = new ObservableCollection<GSM05010DTO>();
        public GSM05010DTO Entity = new GSM05010DTO();
        public GSM05011DTO HeaderEntity = new GSM05011DTO();

        public async Task GetNumberingHeader(GSM05011DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05010Model.GetHeaderTransCodeNumberAsync(poParam);
                HeaderEntity = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetNumberingList()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05010DTO>(HeaderEntity);
                var loReturn = await _GSM05010Model.GetHeaderTransCodeNumberListStreamAsync(loParam);
                loReturn.ForEach(x =>
                    x.CPERIOD_DISPLAY = HeaderEntity.CPERIOD_MODE == "P" ? string.Format("{0} - {1}", x.CCYEAR, x.CPERIOD_NO) : x.CCYEAR);

                GridList = new ObservableCollection<GSM05010DTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntityNumbering(GSM05010DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM05010Model.R_ServiceGetRecordAsync(poEntity);

                Entity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveEntity(GSM05010DTO poNewEntity, eCRUDMode peCrudMode)
        {
            var loEx = new R_Exception();
            try
            {
                if (eCRUDMode.AddMode == peCrudMode)
                {
                    var loPeriod = GeneratePeriod(poNewEntity);
                    poNewEntity.CTRANSACTION_CODE = HeaderEntity.CTRANSACTION_CODE;
                    poNewEntity.CCYEAR = loPeriod.CCYEAR;
                    poNewEntity.CPERIOD_NO = loPeriod.CPERIOD_NO;
                }

                Entity = await _GSM05010Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteEntity(GSM05010DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05010Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private GSM05010DTO GeneratePeriod(GSM05010DTO poParam)
        {
            var lcYear = DateTime.Now;
            var lnPeriod = 1;

            if (GridList.Count > 0)
            {
                var lnLastPeriod = GridList.OrderByDescending(x => x.CPERIOD_NO).FirstOrDefault();
                lnPeriod = Convert.ToInt32(lnLastPeriod.CPERIOD_NO) + 1;
            }

            poParam.CCYEAR = HeaderEntity.CYEAR_FORMAT == "1" ? lcYear.Year.ToString("D2") : lcYear.Year.ToString("D4");
            poParam.CPERIOD_NO = lnPeriod.ToString("D2");

            return poParam;
        }
    }
}

