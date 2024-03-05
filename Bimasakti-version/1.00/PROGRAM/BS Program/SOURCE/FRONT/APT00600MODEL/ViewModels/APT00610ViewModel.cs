using APT00600COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace APT00600MODEL
{
    public class APT00610ViewModel : R_ViewModel<APT00600DTO>
    {
        private APT00600Model _APT00600Model = new APT00600Model();
        private APT00600InitModel _APT00600InitModel = new APT00600InitModel();
        private APT00610Model _APT00610Model = new APT00610Model();

        #region Property Class
        public APT00600DTO PurchaseAdjusment { get; set; } = new APT00600DTO();
        public List<APT00600PropertyDTO> PropertyList { get; set; } = new List<APT00600PropertyDTO>();
        public ObservableCollection<APT00610DTO> PurchaseAdjustmentAllocGrid { get; set; } = new ObservableCollection<APT00610DTO>();
        public APT00600CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; } = new APT00600CompanyInfoGSDTO();
        public APT00600TransCodeInfoGSDTO VAR_ADJUSTMENT_TRANS_CODE { get; set; } = new APT00600TransCodeInfoGSDTO();
        public APT00600SystemParamGLDTO VAR_GL_SYSTEM_PARAM { get; set; } = new APT00600SystemParamGLDTO();
        public APT00600PeriodDTInfoGSDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new APT00600PeriodDTInfoGSDTO();
        public APT00600TodayDateDBDTO VAR_TODAY { get; set; } = new APT00600TodayDateDBDTO();
        #endregion

        #region Property ViewModel
        public DateTime? RefDate { get; set; }
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00600InitModel.GetAllInitialProcessTabEntryAsync();

                VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                VAR_ADJUSTMENT_TRANS_CODE = loResult.VAR_ADJUSTMENT_TRANS_CODE;
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_SOFT_PERIOD_START_DATE = loResult.VAR_SOFT_PERIOD_START_DATE;
                PropertyList = loResult.VAR_PROPERTY_LIST;
                VAR_TODAY = loResult.VAR_TODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseAdjustment(APT00600DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00600Model.R_ServiceGetRecordAsync(poEntity);
                RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                PurchaseAdjusment = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseAdjustmentAllocList(APT00610DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00610Model.GetPurhcaseAdjustmentAllocStreamAsync(poEntity);

                PurchaseAdjustmentAllocGrid = new ObservableCollection<APT00610DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SavePurchaseAdjustment(APT00600DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CREF_DATE = RefDate.Value.ToString("yyyyMMdd");
                var loResult = await _APT00600Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                PurchaseAdjusment = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeletePurchaseAdjustment(APT00600DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00600Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SubmitPurchaseAdjustment(APT00600SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00600Model.SubmitPurchaseAdjAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RedraftPurchaseAdjustment(APT00600SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00600Model.RedraftPurchaseAdjAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
