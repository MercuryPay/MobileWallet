namespace MpsWidgetHostingControl
{
    partial class MpsWidgetControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbControl = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbControl
            // 
            this.wbControl.AllowWebBrowserDrop = false;
            this.wbControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbControl.Location = new System.Drawing.Point(0, 0);
            this.wbControl.Margin = new System.Windows.Forms.Padding(0);
            this.wbControl.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbControl.Name = "wbControl";
            this.wbControl.ScrollBarsEnabled = false;
            this.wbControl.Size = new System.Drawing.Size(150, 150);
            this.wbControl.TabIndex = 0;
            // 
            // MpsWidgetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wbControl);
            this.Name = "MpsWidgetControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbControl;
    }
}
