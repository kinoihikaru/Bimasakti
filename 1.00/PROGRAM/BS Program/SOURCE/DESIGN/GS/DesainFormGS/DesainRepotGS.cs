using FastReport;
using System.Collections;

namespace DesainFormGS
{
    public partial class DesainRepotGS : Form
    {
        private Report loReport;

        public DesainRepotGS()
        {
            InitializeComponent();
        }

        private void DesainRepotGS_Load(object sender, EventArgs e)
        {
            loReport = new Report();
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

        private void GSM03000_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GSM03000Common.Models.GenerateDataModelGSM03000.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}