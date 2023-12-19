using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM05000COMMON_FMC;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05020DeptViewModel : R_ViewModel<GSM05020DeptDTO>
    {
        private PublicLookupModel _LookupGSModel = new PublicLookupModel();
        public ObservableCollection<GSM05020DeptDTO> GridList = new ObservableCollection<GSM05020DeptDTO>();
        public GSM05020DeptDTO FromDept { get; set; } = new GSM05020DeptDTO();
        public GSM05020DeptDTO ToDept { get; set; } = new GSM05020DeptDTO();

        public async Task GetDeptList()
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = new GSL00700ParameterDTO();
                var loReturn = await _LookupGSModel.GSL00700GetDepartmentListAsync(loTempParam);
                List<GSM05020DeptDTO> loResult = R_FrontUtility.ConvertCollectionToCollection<GSM05020DeptDTO>(loReturn).ToList();
                loResult.ForEach(x => x.CDEPT_DISPLAY = string.Format("{0} ({1})", x.CDEPT_NAME, x.CDEPT_CODE));

                GridList = new ObservableCollection<GSM05020DeptDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}

