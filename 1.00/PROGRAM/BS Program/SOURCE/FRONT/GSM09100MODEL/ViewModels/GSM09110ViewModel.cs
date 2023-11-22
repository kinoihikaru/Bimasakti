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
    public class GSM09110ViewModel : R_ViewModel<GSM09110DTO>
    {
        private GSM09110Model _GSM09110Model = new GSM09110Model();
        public ObservableCollection<GSM09110DTO> ProductGrid { get; set; } = new ObservableCollection<GSM09110DTO>();
        public GSM09110DTO ParamPopupMoveProduct { get; set; } = new GSM09110DTO();

        public string ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public async Task GetProductList(GSM09110DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM09110Model.GetProductListAsync(poParam);
                ProductGrid = new ObservableCollection<GSM09110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
