using APT00500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APT00500MODEL
{
    public class APT00510ViewModel : R_ViewModel<APT00500DTO>
    {
        private APT00500Model _APT00500Model = new APT00500Model();
        private APT00500InitModel _APT00500InitModel = new APT00500InitModel();
        private APT00510Model _APT00510Model = new APT00510Model();

        #region Property Class
        public APT00500DTO PurchaseAdjusment { get; set; } = new APT00500DTO();
        public List<APT00500PropertyDTO> PropertyList { get; set; } = new List<APT00500PropertyDTO>();
        public ObservableCollection<APT00510DTO> PurchaseAdjustmentAllocGrid { get; set; } = new ObservableCollection<APT00510DTO>();
        public APT00500CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; } = new APT00500CompanyInfoGSDTO();
        public APT00500TransCodeInfoGSDTO VAR_ADJUSTMENT_TRANS_CODE { get; set; } = new APT00500TransCodeInfoGSDTO();
        public APT00500SystemParamGLDTO VAR_GL_SYSTEM_PARAM { get; set; } = new APT00500SystemParamGLDTO();
        public APT00500PeriodDTInfoGSDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new APT00500PeriodDTInfoGSDTO();
        public APT00500TodayDateDBDTO VAR_TODAY { get; set; } = new APT00500TodayDateDBDTO();
        #endregion

        #region Property ViewModel
        public DateTime? RefDate { get; set; }
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00500InitModel.GetAllInitialProcessTabEntryAsync();

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
        public async Task GetPurchaseAdjustment(APT00500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00500Model.R_ServiceGetRecordAsync(poEntity);
                RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                PurchaseAdjusment = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseAdjustmentAllocList(APT00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00510Model.GetPurhcaseAdjustmentAllocStreamAsync(poEntity);

                PurchaseAdjustmentAllocGrid = new ObservableCollection<APT00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SavePurchaseAdjustment(APT00500DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CREF_DATE = RefDate.Value.ToString("yyyyMMdd");
                var loResult = await _APT00500Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                PurchaseAdjusment = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeletePurchaseAdjustment(APT00500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SubmitPurchaseAdjustment(APT00500SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00500Model.SubmitPurchaseAdjAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RedraftPurchaseAdjustment(APT00500SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00500Model.RedraftPurchaseAdjAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
