﻿namespace CS_WinForms_Ctrl_Air_Speed_Indicator
{
    partial class AirSpeedIndicator
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
            this.SuspendLayout();
            // 
            // AirSpeedIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AirSpeedIndicator";
            this.Size = new System.Drawing.Size(640, 591);
            this.Load += new System.EventHandler(this.AirSpeedIndicator_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AirSpeedIndicator_Paint_1);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
