using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON;
using System.Linq;

namespace GSM09300MODEL.ViewModel
{
    public class MoveSupplierModalViewModel : R_ViewModel<SupplierListForMoveProcessDTO>
    {
        private GSM09310Model loModel = new GSM09310Model();

        public GSM09310ResultDTO loRtn = new GSM09310ResultDTO();

        public GetSupplierCategoryDTO loFromSupplierCategory = new GetSupplierCategoryDTO();

        public GetSupplierCategoryDTO loToSupplierCategory = new GetSupplierCategoryDTO();

        public GetSupplierCategoryResultDTO loSupplierCategoryRtn = new GetSupplierCategoryResultDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public ObservableCollection<SupplierListForMoveProcessDTO> loSupplierList = new ObservableCollection<SupplierListForMoveProcessDTO>();

        public List<SupplierListForMoveProcessDTO> loGetSupplierBatchList = new List<SupplierListForMoveProcessDTO>();

        public string lcSupplierId = "";


        public async Task GetSupplierListForMoveProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09310_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09310_FROM_SUPPLIER_CATEGORY_ID_STREAMING_CONTEXT, loFromSupplierCategory.CCATEGORY_ID);
                loRtn = await loModel.GetSupplierListStreamAsync();

                loSupplierList.Clear();
                foreach (var item in loRtn.Data)
                {
                    loSupplierList.Add(new SupplierListForMoveProcessDTO() { CSUPPLIER_ID = item.CSUPPLIER_ID, CSUPPLIER_NAME = item.CSUPPLIER_NAME });
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task MoveSupplierCategory()
        {
            R_Exception loException = new R_Exception();
            MoveSupplierCategoryParameterDTO loParam = null;
            List<SupplierListForMoveProcessDTO> loSelectedSupplier = new List<SupplierListForMoveProcessDTO>();

            try
            {
                loSelectedSupplier = loGetSupplierBatchList.Where(x => x.LSELECTED == true).ToList();
                if (loSelectedSupplier.Count == 0)
                {
                    return;
                }
                lcSupplierId = "";
                foreach (SupplierListForMoveProcessDTO item in loSelectedSupplier)
                {
                    lcSupplierId = lcSupplierId + item.CSUPPLIER_ID + ",";
                }
                if (!string.IsNullOrEmpty(lcSupplierId))
                {
                    lcSupplierId = lcSupplierId.Substring(0, lcSupplierId.Length - 1); // Remove the last comma and space
                }

                loParam = new MoveSupplierCategoryParameterDTO()
                {
                    CSUPPLIER_ID = lcSupplierId,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID,
                    CFROM_SUPPLIER_CATEGORY_ID = loFromSupplierCategory.CCATEGORY_ID,
                    CTO_SUPPLIER_CATEGORY_ID = loToSupplierCategory.CCATEGORY_ID
                };
                await loModel.MoveSupplierCategoryAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
