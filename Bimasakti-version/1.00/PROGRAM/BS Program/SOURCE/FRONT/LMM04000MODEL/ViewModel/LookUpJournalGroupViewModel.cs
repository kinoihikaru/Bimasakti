using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON;
using System.Collections.ObjectModel;

namespace LMM04000MODEL.ViewModel
{
    public class LookUpJournalGroupViewModel
    {
        public LMM04010Model loModel = new LMM04010Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetJournalGroupResultDTO loJournalGroupRtn = null;

        public ObservableCollection<GetJournalGroupDTO> loJournalGroupList = new ObservableCollection<GetJournalGroupDTO>();

        public async Task GetJournalGroupListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loJournalGroupRtn = await loModel.GetJournalGroupListAsync();
                loJournalGroupList = new ObservableCollection<GetJournalGroupDTO>(loJournalGroupRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
