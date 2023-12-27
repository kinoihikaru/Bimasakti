using APT00300COMMON;
using APT00300FrontResources;
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

namespace APT00300MODEL
{
    public class APT00311ViewModel : R_ViewModel<APT00311DTO>
    {
        private APT00310Model _APT00310Model = new APT00310Model();
        private APT00300UniversalModel _APT00300UniversalModel = new APT00300UniversalModel();

        #region Property View Model
        public APT00310DTO HeaderData { get; set; } = new APT00310DTO();
        public APT00311DTO PurchaseDebitDT { get; set; } = new APT00311DTO();
        public ObservableCollection<APT00311DTO> PurchaseDebitDTGrid { get; set; } = new ObservableCollection<APT00311DTO>();
        public DateTime RefDate { get; set; } 
        #endregion

        #region Initial Process
        public APT00300GSCompanyInfoDTO GSCompanyInfo { get; set; } = new APT00300GSCompanyInfoDTO();
        public async Task GetAllInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00300UniversalModel.GetInitialGSMCompanyInfoAsync();

                GSCompanyInfo = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        public async Task GetPurchaseDebitDTList(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00310Model.GetDebitNoteDTListStreamAsync(poEntity);

                PurchaseDebitDTGrid = new ObservableCollection<APT00311DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseDebitDT(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00310Model.GetDebitNoteDTAsync(poEntity);
                PurchaseDebitDT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
       
    }
}
