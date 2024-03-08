using CBM00100COMMON;
using CBM00100FrontResources;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBM00100MODEL
{
    public class CBM00110ViewModel
    {
        private CBM00100Model _CBM00100Model = new CBM00100Model();

        #region Property Class
        public CBM00100DTO SystemParameterCB { get; set; } = new CBM00100DTO();
        #endregion

        #region Combo Box Helper List
        public List<KeyValuePair<string, string>> CBANK_IN_MODE_LIST { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("D",  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_RadioDBankInMode")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_RadioBBankInMode"))
        };
        #endregion

        #region Property ViewModel
        public DateTime? CBLinkDate { get; set; }
        #endregion
        public async Task<CBM00100DTO> CreateSystemParamCB()
        {
            var loEx = new R_Exception();
            CBM00100DTO loResult = null;

            try
            {
                SystemParameterCB.CCB_LINK_DATE = CBLinkDate.Value.ToString("yyyyMMdd");
                var loParam = new CBM00100SaveParameterDTO { Entity = SystemParameterCB, CRUDMode = eCRUDMode.AddMode };
                loResult = await _CBM00100Model.SaveSystemParamCBAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}
