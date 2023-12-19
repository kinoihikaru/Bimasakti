namespace DesainGLForm
{
    partial class DesainGLReport
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
            GLR00200 = new Button();
            GLM00400 = new Button();
            BaseHeaderLandscapeCommon = new Button();
            BaseHeaderCommon = new Button();
            GLTR00100 = new Button();
            GLR00210 = new Button();
            SuspendLayout();
            // 
            // GLR00200
            // 
            GLR00200.Location = new Point(12, 12);
            GLR00200.Name = "GLR00200";
            GLR00200.Size = new Size(193, 23);
            GLR00200.TabIndex = 0;
            GLR00200.Text = "GLR00200";
            GLR00200.UseVisualStyleBackColor = true;
            GLR00200.Click += GLR00200_Click;
            // 
            // GLM00400
            // 
            GLM00400.Location = new Point(12, 41);
            GLM00400.Name = "GLM00400";
            GLM00400.Size = new Size(193, 23);
            GLM00400.TabIndex = 1;
            GLM00400.Text = "GLM00400";
            GLM00400.UseVisualStyleBackColor = true;
            GLM00400.Click += GLM00400_Click;
            // 
            // BaseHeaderLandscapeCommon
            // 
            BaseHeaderLandscapeCommon.Location = new Point(225, 41);
            BaseHeaderLandscapeCommon.Name = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.Size = new Size(193, 23);
            BaseHeaderLandscapeCommon.TabIndex = 3;
            BaseHeaderLandscapeCommon.Text = "BaseHeaderLandscapeCommon";
            BaseHeaderLandscapeCommon.UseVisualStyleBackColor = true;
            BaseHeaderLandscapeCommon.Click += BaseHeaderLandscapeCommon_Click;
            // 
            // BaseHeaderCommon
            // 
            BaseHeaderCommon.Location = new Point(225, 12);
            BaseHeaderCommon.Name = "BaseHeaderCommon";
            BaseHeaderCommon.Size = new Size(193, 23);
            BaseHeaderCommon.TabIndex = 2;
            BaseHeaderCommon.Text = "BaseHeaderCommon";
            BaseHeaderCommon.UseVisualStyleBackColor = true;
            BaseHeaderCommon.Click += BaseHeaderCommon_Click;
            // 
            // GLTR00100
            // 
            GLTR00100.Location = new Point(12, 70);
            GLTR00100.Name = "GLTR00100";
            GLTR00100.Size = new Size(193, 23);
            GLTR00100.TabIndex = 4;
            GLTR00100.Text = "GLTR00100";
            GLTR00100.UseVisualStyleBackColor = true;
            GLTR00100.Click += GLTR00100_Click;
            // 
            // GLR00210
            // 
            GLR00210.Location = new Point(12, 99);
            GLR00210.Name = "GLR00210";
            GLR00210.Size = new Size(193, 22);
            GLR00210.TabIndex = 5;
            GLR00210.Text = "GLR00210";
            GLR00210.UseVisualStyleBackColor = true;
            GLR00210.Click += GLR00210_Click;
            // 
            // DesainGLReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GLR00210);
            Controls.Add(GLTR00100);
            Controls.Add(BaseHeaderLandscapeCommon);
            Controls.Add(BaseHeaderCommon);
            Controls.Add(GLM00400);
            Controls.Add(GLR00200);
            Name = "DesainGLReport";
            Text = "DesainGLReport";
            Load += DesainGLReport_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button GLR00200;
        private Button GLM00400;
        private Button BaseHeaderLandscapeCommon;
        private Button BaseHeaderCommon;
        private Button GLTR00100;
        private Button GLR00210;
    }
}