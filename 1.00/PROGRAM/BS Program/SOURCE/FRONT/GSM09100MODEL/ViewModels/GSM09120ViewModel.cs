using GSM09100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;

namespace GSM09100MODEL
{
    public class GSM09120ViewModel : R_ViewModel<GSM09120DTO>
    {
        private GSM09110Model _GSM09110Model = new GSM09110Model();
        public GSM09120DTO MoveProduct { get; set; } = new GSM09120DTO();

        public async Task MoveProductSelected(string poCIDList)
        {
            var loEx = new R_Exception();

            try
            {
                MoveProduct.CID_LIST = poCIDList;  
                await _GSM09110Model.MoveProductAsync(MoveProduct);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
