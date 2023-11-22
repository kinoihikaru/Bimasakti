using FastReport;
using System.Collections;
using BaseHeaderReportCOMMON;

namespace DesignFormLM
{
    public partial class LMDesainReport : Form
    {
        private Report loReport;

        public LMDesainReport()
        {
            InitializeComponent();
        }
        private void LMDesainReport_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void LMM01000General_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModel.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void LMM01010RateEC_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModelRateEC.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void LMM01020RateWG_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModelRateWG.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void LMM01030RatePG_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModelRatePG.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void LMM01040RateGU_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModelRateGU.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void LMM01050RateOT_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(LMM01000COMMON.Models.GenerateDataModelRateOT.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void BaseHeaderCommon_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(BaseHeaderReportCOMMON.Models.GenerateDataModelHeader.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void BaseHeaderLandscapeCommon_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(BaseHeaderReportCOMMON.Models.GenerateDataModelHeader.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}