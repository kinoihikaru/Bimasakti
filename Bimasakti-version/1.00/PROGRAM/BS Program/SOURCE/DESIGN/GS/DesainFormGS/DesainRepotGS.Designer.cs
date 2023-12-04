namespace DesainFormGS
{
    partial class DesainRepotGS
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
            BaseHeaderLandscapeCommon = new Button();
            BaseHeaderCommon = new Button();
            GSM03000 = new Button();
            SuspendLayout();
            // 
            // BaseHeaderLandscapeCommon
            // 
            BaseHeaderLandscapeCommon.Location = new Point(214, 41);
            BaseHeaderLandscapeCommon.Name = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.Size = new Size(193, 23);
            BaseHeaderLandscapeCommon.TabIndex = 9;
            BaseHeaderLandscapeCommon.Text = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.UseVisualStyleBackColor = true;
            BaseHeaderLandscapeCommon.Click += BaseHeaderLandscapeCommon_Click;
            // 
            // BaseHeaderCommon
            // 
            BaseHeaderCommon.Location = new Point(214, 12);
            BaseHeaderCommon.Name = "BaseHeaderCommon";
            BaseHeaderCommon.Size = new Size(193, 23);
            BaseHeaderCommon.TabIndex = 8;
            BaseHeaderCommon.Text = "BaseHeaderCommon";
            BaseHeaderCommon.UseVisualStyleBackColor = true;
            BaseHeaderCommon.Click += BaseHeaderCommon_Click;
            // 
            // GSM03000
            // 
            GSM03000.Location = new Point(12, 12);
            GSM03000.Name = "GSM03000";
            GSM03000.Size = new Size(193, 23);
            GSM03000.TabIndex = 10;
            GSM03000.Text = "GSM03000";
            GSM03000.UseVisualStyleBackColor = true;
            GSM03000.Click += GSM03000_Click;
            // 
            // DesainRepotGS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GSM03000);
            Controls.Add(BaseHeaderLandscapeCommon);
            Controls.Add(BaseHeaderCommon);
            Name = "DesainRepotGS";
            Text = "DesainRepotGS";
            Load += DesainRepotGS_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button BaseHeaderLandscapeCommon;
        private Button BaseHeaderCommon;
        private Button GSM03000;
    }
}