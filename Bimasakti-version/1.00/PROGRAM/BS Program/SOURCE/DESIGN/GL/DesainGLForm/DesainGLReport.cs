using FastReport;
using System.Collections;

namespace DesainGLForm
{
    public partial class DesainGLReport : Form
    {
        private Report loReport;

        public DesainGLReport()
        {
            InitializeComponent();
        }

        private void DesainGLReport_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void GLR00200_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GLR00200COMMON.Models.GenerateDataModelGLR00200.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLM00400_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GLM00400COMMON.Models.GenerateDataModelGLM00400.DefaultDataWithHeader());
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

        private void GLTR00100_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GLTR00100COMMON.Models.GenerateDataModelGLTR00100.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLR00210_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GLR00200COMMON.Models.GenerateDataModelGLR00210.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLM00200_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GLM00200Common.Models.GenerateDataModelGLR00200.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}