namespace DesignFormLM
{
    partial class LMDesainReport
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LMM01000General = new Button();
            LMM01010RateEC = new Button();
            LMM01020RateWG = new Button();
            LMM01030RatePG = new Button();
            LMM01040RateGU = new Button();
            LMM01050RateOT = new Button();
            BaseHeaderCommon = new Button();
            BaseHeaderLandscapeCommon = new Button();
            SuspendLayout();
            // 
            // LMM01000General
            // 
            LMM01000General.Location = new Point(12, 12);
            LMM01000General.Name = "LMM01000General";
            LMM01000General.Size = new Size(196, 23);
            LMM01000General.TabIndex = 0;
            LMM01000General.Text = "LMM01000General";
            LMM01000General.UseVisualStyleBackColor = true;
            LMM01000General.Click += LMM01000General_Click;
            // 
            // LMM01010RateEC
            // 
            LMM01010RateEC.Location = new Point(12, 41);
            LMM01010RateEC.Name = "LMM01010RateEC";
            LMM01010RateEC.Size = new Size(196, 23);
            LMM01010RateEC.TabIndex = 1;
            LMM01010RateEC.Text = "LMM01010RateEC";
            LMM01010RateEC.UseVisualStyleBackColor = true;
            LMM01010RateEC.Click += LMM01010RateEC_Click;
            // 
            // LMM01020RateWG
            // 
            LMM01020RateWG.Location = new Point(12, 70);
            LMM01020RateWG.Name = "LMM01020RateWG";
            LMM01020RateWG.Size = new Size(196, 23);
            LMM01020RateWG.TabIndex = 2;
            LMM01020RateWG.Text = "LMM01020RateWG";
            LMM01020RateWG.UseVisualStyleBackColor = true;
            LMM01020RateWG.Click += LMM01020RateWG_Click;
            // 
            // LMM01030RatePG
            // 
            LMM01030RatePG.Location = new Point(12, 99);
            LMM01030RatePG.Name = "LMM01030RatePG";
            LMM01030RatePG.Size = new Size(196, 23);
            LMM01030RatePG.TabIndex = 3;
            LMM01030RatePG.Text = "LMM01030RatePG";
            LMM01030RatePG.UseVisualStyleBackColor = true;
            LMM01030RatePG.Click += LMM01030RatePG_Click;
            // 
            // LMM01040RateGU
            // 
            LMM01040RateGU.Location = new Point(12, 128);
            LMM01040RateGU.Name = "LMM01040RateGU";
            LMM01040RateGU.Size = new Size(196, 23);
            LMM01040RateGU.TabIndex = 4;
            LMM01040RateGU.Text = "LMM01040RateGU";
            LMM01040RateGU.UseVisualStyleBackColor = true;
            LMM01040RateGU.Click += LMM01040RateGU_Click;
            // 
            // LMM01050RateOT
            // 
            LMM01050RateOT.Location = new Point(12, 157);
            LMM01050RateOT.Name = "LMM01050RateOT";
            LMM01050RateOT.Size = new Size(196, 23);
            LMM01050RateOT.TabIndex = 5;
            LMM01050RateOT.Text = "LMM01050RateOT";
            LMM01050RateOT.UseVisualStyleBackColor = true;
            LMM01050RateOT.Click += LMM01050RateOT_Click;
            // 
            // BaseHeaderCommon
            // 
            BaseHeaderCommon.Location = new Point(234, 12);
            BaseHeaderCommon.Name = "BaseHeaderCommon";
            BaseHeaderCommon.Size = new Size(193, 23);
            BaseHeaderCommon.TabIndex = 6;
            BaseHeaderCommon.Text = "BaseHeaderCommon";
            BaseHeaderCommon.UseVisualStyleBackColor = true;
            BaseHeaderCommon.Click += BaseHeaderCommon_Click;
            // 
            // BaseHeaderLandscapeCommon
            // 
            BaseHeaderLandscapeCommon.Location = new Point(234, 41);
            BaseHeaderLandscapeCommon.Name = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.Size = new Size(193, 23);
            BaseHeaderLandscapeCommon.TabIndex = 7;
            BaseHeaderLandscapeCommon.Text = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.UseVisualStyleBackColor = true;
            BaseHeaderLandscapeCommon.Click += BaseHeaderLandscapeCommon_Click;
            // 
            // LMDesainReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BaseHeaderLandscapeCommon);
            Controls.Add(BaseHeaderCommon);
            Controls.Add(LMM01050RateOT);
            Controls.Add(LMM01040RateGU);
            Controls.Add(LMM01030RatePG);
            Controls.Add(LMM01020RateWG);
            Controls.Add(LMM01010RateEC);
            Controls.Add(LMM01000General);
            Name = "LMDesainReport";
            Text = "LMDesainReport";
            Load += LMDesainReport_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button LMM01000General;
        private Button LMM01010RateEC;
        private Button LMM01020RateWG;
        private Button LMM01030RatePG;
        private Button LMM01040RateGU;
        private Button LMM01050RateOT;
        private Button BaseHeaderCommon;
        private Button BaseHeaderLandscapeCommon;
    }
}