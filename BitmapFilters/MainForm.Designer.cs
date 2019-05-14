namespace BitmapFilters
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbInput = new System.Windows.Forms.GroupBox();
            this.picSource = new System.Windows.Forms.Panel();
            this.grbOutput = new System.Windows.Forms.GroupBox();
            this.picOutput = new System.Windows.Forms.Panel();
            this.btnLoadSource = new System.Windows.Forms.Button();
            this.grbResult = new System.Windows.Forms.GroupBox();
            this.picResult = new System.Windows.Forms.Panel();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.btnSetResultAsOutput = new System.Windows.Forms.Button();
            this.btnSetSourceAsOutput = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cmbEdgeDetection = new System.Windows.Forms.ComboBox();
            this.lblSettingsMask = new System.Windows.Forms.Label();
            this.txbCValue = new System.Windows.Forms.TextBox();
            this.txbSettingsDivider = new System.Windows.Forms.TextBox();
            this.dgvMask = new System.Windows.Forms.DataGridView();
            this.labelMaxMinCValue = new System.Windows.Forms.Label();
            this.labelDivider = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.grbFilterSettings = new System.Windows.Forms.GroupBox();
            this.grbInput.SuspendLayout();
            this.grbOutput.SuspendLayout();
            this.grbResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMask)).BeginInit();
            this.grbFilterSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbInput
            // 
            this.grbInput.Controls.Add(this.picSource);
            this.grbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grbInput.Location = new System.Drawing.Point(12, 9);
            this.grbInput.Name = "grbInput";
            this.grbInput.Size = new System.Drawing.Size(270, 224);
            this.grbInput.TabIndex = 0;
            this.grbInput.TabStop = false;
            this.grbInput.Text = "Исходное изображение";
            // 
            // picSource
            // 
            this.picSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSource.Location = new System.Drawing.Point(3, 21);
            this.picSource.Name = "picSource";
            this.picSource.Size = new System.Drawing.Size(264, 200);
            this.picSource.TabIndex = 0;
            this.picSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picSource_MouseClick);
            // 
            // grbOutput
            // 
            this.grbOutput.Controls.Add(this.picOutput);
            this.grbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grbOutput.Location = new System.Drawing.Point(288, 9);
            this.grbOutput.Name = "grbOutput";
            this.grbOutput.Size = new System.Drawing.Size(270, 224);
            this.grbOutput.TabIndex = 1;
            this.grbOutput.TabStop = false;
            this.grbOutput.Text = "Полутоновое изображение";
            // 
            // picOutput
            // 
            this.picOutput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOutput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOutput.Location = new System.Drawing.Point(3, 21);
            this.picOutput.Name = "picOutput";
            this.picOutput.Size = new System.Drawing.Size(264, 200);
            this.picOutput.TabIndex = 0;
            this.picOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picOutput_MouseClick);
            // 
            // btnLoadSource
            // 
            this.btnLoadSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadSource.Location = new System.Drawing.Point(9, 280);
            this.btnLoadSource.Name = "btnLoadSource";
            this.btnLoadSource.Size = new System.Drawing.Size(270, 30);
            this.btnLoadSource.TabIndex = 2;
            this.btnLoadSource.Text = "Открыть";
            this.btnLoadSource.UseVisualStyleBackColor = true;
            this.btnLoadSource.Click += new System.EventHandler(this.btnLoadSource_Click);
            // 
            // grbResult
            // 
            this.grbResult.Controls.Add(this.picResult);
            this.grbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grbResult.Location = new System.Drawing.Point(564, 9);
            this.grbResult.Name = "grbResult";
            this.grbResult.Size = new System.Drawing.Size(270, 224);
            this.grbResult.TabIndex = 1;
            this.grbResult.TabStop = false;
            this.grbResult.Text = "Результат";
            // 
            // picResult
            // 
            this.picResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picResult.Location = new System.Drawing.Point(3, 21);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(264, 200);
            this.picResult.TabIndex = 0;
            this.picResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picResult_MouseClick);
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveResult.Location = new System.Drawing.Point(9, 321);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(270, 30);
            this.btnSaveResult.TabIndex = 21;
            this.btnSaveResult.Text = "Сохранить";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);
            // 
            // btnSetResultAsOutput
            // 
            this.btnSetResultAsOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetResultAsOutput.Location = new System.Drawing.Point(9, 360);
            this.btnSetResultAsOutput.Name = "btnSetResultAsOutput";
            this.btnSetResultAsOutput.Size = new System.Drawing.Size(270, 30);
            this.btnSetResultAsOutput.TabIndex = 23;
            this.btnSetResultAsOutput.Text = "Обрабатывать результат";
            this.btnSetResultAsOutput.UseVisualStyleBackColor = true;
            this.btnSetResultAsOutput.Click += new System.EventHandler(this.btnSetResultAsOutput_Click);
            // 
            // btnSetSourceAsOutput
            // 
            this.btnSetSourceAsOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetSourceAsOutput.Location = new System.Drawing.Point(9, 399);
            this.btnSetSourceAsOutput.Name = "btnSetSourceAsOutput";
            this.btnSetSourceAsOutput.Size = new System.Drawing.Size(270, 30);
            this.btnSetSourceAsOutput.TabIndex = 25;
            this.btnSetSourceAsOutput.Text = "Обрабатывать цветное";
            this.btnSetSourceAsOutput.UseVisualStyleBackColor = true;
            this.btnSetSourceAsOutput.Click += new System.EventHandler(this.btnSetSourceAsOutput_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReset.Location = new System.Drawing.Point(9, 439);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(270, 30);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbEdgeDetection
            // 
            this.cmbEdgeDetection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdgeDetection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbEdgeDetection.FormattingEnabled = true;
            this.cmbEdgeDetection.Items.AddRange(new object[] {
            "Не выбрано",
            "Модуль градиента",
            "Градиент Робертса",
            "∇I = | I(i, j) - I(i+1, j) | + | I(i, j) - I(i, j+1) |",
            "∇I = max(| I(i, j) - I(i +1, j) | , | I(i, j) - I(i, j +1) | )",
            "∇I = | I(i, j) - I(i + 1, j + 1) | + | I(i + 1, j) - I(i, j + 1) |",
            "∇I = Σ |I(i, j) - I(i+k, j+L)|",
            "∇I = max |I(i, j) - I(i+k, j+L)|",
            "Аппроксимация по оси X",
            "Аппроксимация по оси Y",
            "Повышение резкости 1",
            "Повышение резкости 2",
            "Повышение резкости 3",
            "Повышение резкости 4",
            "Согласование с плоскостью в окне 2x2",
            "Оператор Превитта",
            "Маска согласования с поверхностью второго порядка для оператора Лапласа",
            "Лапласиан Гауссиана в окне 5x5",
            "Проверка точки на локальный максимум в окне 3x3",
            "Проверка точки на локальный максимум по оси X",
            "Проверка точки на локальный максимум по оси Y"});
            this.cmbEdgeDetection.Location = new System.Drawing.Point(9, 242);
            this.cmbEdgeDetection.Name = "cmbEdgeDetection";
            this.cmbEdgeDetection.Size = new System.Drawing.Size(825, 26);
            this.cmbEdgeDetection.TabIndex = 27;
            this.cmbEdgeDetection.SelectedIndexChanged += new System.EventHandler(this.OnCheckChangedEventHandler);
            // 
            // lblSettingsMask
            // 
            this.lblSettingsMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSettingsMask.Location = new System.Drawing.Point(6, 15);
            this.lblSettingsMask.Name = "lblSettingsMask";
            this.lblSettingsMask.Size = new System.Drawing.Size(57, 26);
            this.lblSettingsMask.TabIndex = 33;
            this.lblSettingsMask.Text = "Маска:";
            this.lblSettingsMask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txbCValue
            // 
            this.txbCValue.Location = new System.Drawing.Point(365, 20);
            this.txbCValue.Name = "txbCValue";
            this.txbCValue.Size = new System.Drawing.Size(38, 24);
            this.txbCValue.TabIndex = 62;
            this.txbCValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txbCValue.TextChanged += new System.EventHandler(this.OnCheckChangedEventHandler);
            // 
            // txbSettingsDivider
            // 
            this.txbSettingsDivider.Enabled = false;
            this.txbSettingsDivider.Location = new System.Drawing.Point(139, 166);
            this.txbSettingsDivider.Name = "txbSettingsDivider";
            this.txbSettingsDivider.Size = new System.Drawing.Size(69, 24);
            this.txbSettingsDivider.TabIndex = 60;
            this.txbSettingsDivider.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvMask
            // 
            this.dgvMask.AllowUserToAddRows = false;
            this.dgvMask.AllowUserToDeleteRows = false;
            this.dgvMask.AllowUserToResizeColumns = false;
            this.dgvMask.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvMask.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMask.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMask.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMask.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMask.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgvMask.Location = new System.Drawing.Point(65, 15);
            this.dgvMask.MultiSelect = false;
            this.dgvMask.Name = "dgvMask";
            this.dgvMask.ReadOnly = true;
            this.dgvMask.RowHeadersVisible = false;
            this.dgvMask.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvMask.Size = new System.Drawing.Size(223, 143);
            this.dgvMask.TabIndex = 25;
            // 
            // labelMaxMinCValue
            // 
            this.labelMaxMinCValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMaxMinCValue.Location = new System.Drawing.Point(407, 18);
            this.labelMaxMinCValue.Name = "labelMaxMinCValue";
            this.labelMaxMinCValue.Size = new System.Drawing.Size(107, 26);
            this.labelMaxMinCValue.TabIndex = 70;
            this.labelMaxMinCValue.Text = "(1 =< c <= 10)";
            this.labelMaxMinCValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDivider
            // 
            this.labelDivider.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDivider.Location = new System.Drawing.Point(6, 164);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(84, 26);
            this.labelDivider.TabIndex = 61;
            this.labelDivider.Text = "Делитель:";
            this.labelDivider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelC
            // 
            this.labelC.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelC.Location = new System.Drawing.Point(340, 18);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(21, 26);
            this.labelC.TabIndex = 71;
            this.labelC.Text = "c:";
            this.labelC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grbFilterSettings
            // 
            this.grbFilterSettings.Controls.Add(this.labelC);
            this.grbFilterSettings.Controls.Add(this.labelDivider);
            this.grbFilterSettings.Controls.Add(this.labelMaxMinCValue);
            this.grbFilterSettings.Controls.Add(this.dgvMask);
            this.grbFilterSettings.Controls.Add(this.txbSettingsDivider);
            this.grbFilterSettings.Controls.Add(this.txbCValue);
            this.grbFilterSettings.Controls.Add(this.lblSettingsMask);
            this.grbFilterSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grbFilterSettings.Location = new System.Drawing.Point(291, 273);
            this.grbFilterSettings.Name = "grbFilterSettings";
            this.grbFilterSettings.Size = new System.Drawing.Size(543, 196);
            this.grbFilterSettings.TabIndex = 24;
            this.grbFilterSettings.TabStop = false;
            this.grbFilterSettings.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 480);
            this.Controls.Add(this.cmbEdgeDetection);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSetSourceAsOutput);
            this.Controls.Add(this.btnLoadSource);
            this.Controls.Add(this.grbFilterSettings);
            this.Controls.Add(this.btnSetResultAsOutput);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.grbResult);
            this.Controls.Add(this.grbOutput);
            this.Controls.Add(this.grbInput);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная работа №6 Определение границ на изображении";
            this.grbInput.ResumeLayout(false);
            this.grbOutput.ResumeLayout(false);
            this.grbResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMask)).EndInit();
            this.grbFilterSettings.ResumeLayout(false);
            this.grbFilterSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbInput;
        private System.Windows.Forms.Panel picSource;
        private System.Windows.Forms.GroupBox grbOutput;
        private System.Windows.Forms.Panel picOutput;
        private System.Windows.Forms.Button btnLoadSource;
        private System.Windows.Forms.GroupBox grbResult;
        private System.Windows.Forms.Panel picResult;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Button btnSetResultAsOutput;
        private System.Windows.Forms.Button btnSetSourceAsOutput;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cmbEdgeDetection;
        private System.Windows.Forms.Label lblSettingsMask;
        private System.Windows.Forms.TextBox txbCValue;
        private System.Windows.Forms.TextBox txbSettingsDivider;
        private System.Windows.Forms.DataGridView dgvMask;
        private System.Windows.Forms.Label labelMaxMinCValue;
        private System.Windows.Forms.Label labelDivider;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.GroupBox grbFilterSettings;
    }
}

