using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500MODEL.ViewModel;
using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.DTOs;
using System.Globalization;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMM03500FRONT
{
    public partial class LMM03507 : R_Page, R_ITabPage
    {
        private LMM03507ViewModel loMembersViewModel = new LMM03507ViewModel();

        private R_Conductor _conductorMembersRef;

        private R_Grid<LMM03507DTO> _gridMembersRef;

        private R_TextBox MembersIdRef;

        private R_TextBox MembersNameRef;

        private DateTime BirthDateTemp;

        private List<Gender> loGenderList = new List<Gender>()
        {
            new Gender(){GenderId = "M", GenderName = "Male"},
            new Gender(){GenderId = "F", GenderName = "Female"},
        };

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loMembersViewModel.loTabParameter = (TabParameterDTO)poParameter;

                await loMembersViewModel.GetIdTypeListStreamAsync();

                await loMembersViewModel.GetTenantAsync();

                await _gridMembersRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Members_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loMembersViewModel.loTabParameter = (TabParameterDTO)poParam;

            if (loMembersViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
            {
                await loMembersViewModel.GetMembersListStreamAsync();
            }
            else
            {
                loMembersViewModel.loMembersList.Clear();
                loMembersViewModel.Data.Clear();
            }
        }

        private async Task Grid_DisplayMembers(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMM03507DetailDTO)eventArgs.Data;
                loMembersViewModel.loMembers = loParam;
            }
        }

        private void R_ConvertToGridMembersEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            LMM03507DetailDTO loData = (LMM03507DetailDTO)eventArgs.Data;
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<LMM03507DTO>(eventArgs.Data);
            LMM03507DTO loGridData = (LMM03507DTO)eventArgs.GridData;
            if (loData.CGENDER == "M")
            {
                loGridData.CGENDER_NAME = "Male";
            }
            else if (loData.CGENDER == "F")
            {
                loGridData.CGENDER_NAME = "Female";
            }
            loGridData.DDATE_BIRTH = DateTime.ParseExact(loGridData.CDATE_BIRTH, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        private async Task Grid_R_ServiceGetMembersListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loMembersViewModel.GetMembersListStreamAsync();
                eventArgs.ListEntityResult = loMembersViewModel.loMembersList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetMembersRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                LMM03507DetailDTO loParam = new LMM03507DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<LMM03507DetailDTO>(eventArgs.Data);
                loParam.CTENANT_ID = loMembersViewModel.loTabParameter.CSELECTED_TENANT_ID;
                await loMembersViewModel.GetMembersAsync(loParam);

                BirthDateTemp = DateTime.ParseExact(loMembersViewModel.loMembers.CDATE_BIRTH, "yyyyMMdd", CultureInfo.InvariantCulture);
                
                eventArgs.Result = loMembersViewModel.loMembers;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_MembersValidation(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                LMM03507DetailDTO loData = (LMM03507DetailDTO)eventArgs.Data;
                loData.CDATE_BIRTH = BirthDateTemp.ToString("yyyyMMdd");
                loMembersViewModel.MembersValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
/*
        private void Grid_BeforeAddMembers(R_BeforeAddEventArgs eventArgs)
        {
            MembersIdRef.FocusAsync();
        }

        private void Grid_BeforeEditMembers(R_BeforeEditEventArgs eventArgs)
        {
            MembersNameRef.FocusAsync();
        }
*/
        private async Task Grid_ServiceSaveMembers(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM03507DetailDTO loData = (LMM03507DetailDTO)eventArgs.Data;

                loData.CTENANT_ID = loMembersViewModel.loTenant.CTENANT_ID;
                loData.CTENANT_NAME = loMembersViewModel.loTenant.CTENANT_NAME;

                loData.CDATE_BIRTH = BirthDateTemp.ToString("yyyyMMdd");
                await loMembersViewModel.SaveMembersAsync(
                    loData,
                    (eCRUDMode)eventArgs.ConductorMode);

                BirthDateTemp = DateTime.ParseExact(loMembersViewModel.loMembers.CDATE_BIRTH, "yyyyMMdd", CultureInfo.InvariantCulture);

                eventArgs.Result = loMembersViewModel.loMembers;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_AfterAddMembers(R_AfterAddEventArgs eventArgs)
        {
            LMM03507DetailDTO loData = (LMM03507DetailDTO)eventArgs.Data;
            if (loMembersViewModel.loMembersList.Count() == 0)
            {
                loData.CMEMBER_SEQ_NO = "1";
            }
            else
            {
                int temp = int.Parse(loMembersViewModel.loMembersList.Last().CMEMBER_SEQ_NO);
                int SeqNo = temp + 1;
                loData.CMEMBER_SEQ_NO = Convert.ToString(SeqNo);
            }
        }

        private void OnBirthDateValueChanged(DateTime poEntity)
        {
            DateTime birthDate = poEntity;
            int age = DateTime.Today.Year - birthDate.Year;
            if (DateTime.Today < birthDate.AddYears(age))
            {
                age--; // Subtract 1 year if the current date is before the birth date in the current year
            }
            loMembersViewModel.Data.IAGE = age;
            BirthDateTemp = poEntity;
        }

        private async Task Grid_ServiceDeleteMembers(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM03507DetailDTO loData = (LMM03507DetailDTO)eventArgs.Data;

                loData.CTENANT_ID = loMembersViewModel.loTenant.CTENANT_ID;
                loData.CTENANT_NAME = loMembersViewModel.loTenant.CTENANT_NAME;

                await loMembersViewModel.DeleteMembersAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
    public class Gender
    {
        public string GenderId { get; set; }
        public string GenderName { get; set; }
    }
}