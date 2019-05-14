namespace BitmapFilters
{
    partial class ShowImageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picScale = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // picScale
            // 
            this.picScale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picScale.Cursor = System.Windows.Forms.Cursors.Default;
            this.picScale.Location = new System.Drawing.Point(12, 12);
            this.picScale.Name = "picScale";
            this.picScale.Size = new System.Drawing.Size(500, 500);
            this.picScale.TabIndex = 0;
            // 
            // ShowImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 521);
            this.Controls.Add(this.picScale);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ShowImageForm";
            this.Text = "Увеличенное изображение";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel picScale;
    }
}