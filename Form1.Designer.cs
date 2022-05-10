namespace CustomControls
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customGroupBox1 = new CustomControls.CustomGroupBox();
            this.customDataGridView1 = new CustomControls.CustomDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customPanel1 = new CustomControls.CustomPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.customCheckBox1 = new CustomControls.CustomCheckBox();
            this.customCheckBox2 = new CustomControls.CustomCheckBox();
            this.customTextBox1 = new CustomControls.CustomTextBox();
            this.customTextBox2 = new CustomControls.CustomTextBox();
            this.customButton1 = new CustomControls.CustomButton();
            this.customButton2 = new CustomControls.CustomButton();
            this.customProgressBar1 = new CustomControls.CustomProgressBar();
            this.customButton3 = new CustomControls.CustomButton();
            this.customButton4 = new CustomControls.CustomButton();
            this.customButton5 = new CustomControls.CustomButton();
            this.customComboBox1 = new CustomControls.CustomComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customGroupBox1
            // 
            this.customGroupBox1.BorderColor = System.Drawing.Color.Red;
            this.customGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.customGroupBox1.Name = "customGroupBox1";
            this.customGroupBox1.Size = new System.Drawing.Size(200, 100);
            this.customGroupBox1.TabIndex = 0;
            this.customGroupBox1.TabStop = false;
            this.customGroupBox1.Text = "customGroupBox1";
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.BorderColor = System.Drawing.Color.Red;
            this.customDataGridView1.CheckColor = System.Drawing.Color.Blue;
            this.customDataGridView1.ColumnHeadersBorder = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(227)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridView1.GridColor = System.Drawing.Color.LightBlue;
            this.customDataGridView1.Location = new System.Drawing.Point(424, 12);
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 25;
            this.customDataGridView1.SelectionColor = System.Drawing.Color.LightBlue;
            this.customDataGridView1.Size = new System.Drawing.Size(309, 146);
            this.customDataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column1.HeaderText = "CheckBox";
            this.Column1.Name = "Column1";
            this.Column1.Width = 65;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ComboBox";
            this.Column2.Items.AddRange(new object[] {
            "Test 1",
            "Test 2"});
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Text";
            this.Column3.Name = "Column3";
            // 
            // customPanel1
            // 
            this.customPanel1.Border = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.BorderColor = System.Drawing.Color.Red;
            this.customPanel1.ButtonBorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Location = new System.Drawing.Point(218, 12);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(200, 100);
            this.customPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Panel";
            // 
            // customCheckBox1
            // 
            this.customCheckBox1.BorderColor = System.Drawing.Color.Red;
            this.customCheckBox1.CheckColor = System.Drawing.Color.Blue;
            this.customCheckBox1.Checked = true;
            this.customCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.customCheckBox1.FlatAppearance.BorderSize = 0;
            this.customCheckBox1.Location = new System.Drawing.Point(18, 157);
            this.customCheckBox1.Name = "customCheckBox1";
            this.customCheckBox1.SelectionColor = System.Drawing.Color.Blue;
            this.customCheckBox1.Size = new System.Drawing.Size(124, 17);
            this.customCheckBox1.TabIndex = 3;
            this.customCheckBox1.Text = "customCheckBox1";
            this.customCheckBox1.UseVisualStyleBackColor = false;
            // 
            // customCheckBox2
            // 
            this.customCheckBox2.BorderColor = System.Drawing.Color.Red;
            this.customCheckBox2.CheckColor = System.Drawing.Color.Blue;
            this.customCheckBox2.Checked = true;
            this.customCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.customCheckBox2.FlatAppearance.BorderSize = 0;
            this.customCheckBox2.Location = new System.Drawing.Point(18, 180);
            this.customCheckBox2.Name = "customCheckBox2";
            this.customCheckBox2.SelectionColor = System.Drawing.Color.Blue;
            this.customCheckBox2.Size = new System.Drawing.Size(124, 17);
            this.customCheckBox2.TabIndex = 4;
            this.customCheckBox2.Text = "customCheckBox2";
            this.customCheckBox2.UseVisualStyleBackColor = false;
            // 
            // customTextBox1
            // 
            this.customTextBox1.AcceptsReturn = false;
            this.customTextBox1.AcceptsTab = false;
            this.customTextBox1.BorderColor = System.Drawing.Color.Red;
            this.customTextBox1.BorderSize = 1;
            this.customTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.customTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.customTextBox1.HideSelection = true;
            this.customTextBox1.Location = new System.Drawing.Point(179, 159);
            this.customTextBox1.MaxLength = 32767;
            this.customTextBox1.Multiline = false;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.ReadOnly = false;
            this.customTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.customTextBox1.ShortcutsEnabled = true;
            this.customTextBox1.Size = new System.Drawing.Size(153, 23);
            this.customTextBox1.TabIndex = 0;
            this.customTextBox1.Texts = "Text Box";
            this.customTextBox1.TextsAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.customTextBox1.UnderlinedStyle = false;
            this.customTextBox1.UsePasswordChar = false;
            this.customTextBox1.WordWrap = true;
            // 
            // customTextBox2
            // 
            this.customTextBox2.AcceptsReturn = false;
            this.customTextBox2.AcceptsTab = false;
            this.customTextBox2.BorderColor = System.Drawing.Color.Red;
            this.customTextBox2.BorderSize = 1;
            this.customTextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.customTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.customTextBox2.HideSelection = true;
            this.customTextBox2.Location = new System.Drawing.Point(179, 188);
            this.customTextBox2.MaxLength = 32767;
            this.customTextBox2.MinimumSize = new System.Drawing.Size(0, 23);
            this.customTextBox2.Multiline = true;
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.ReadOnly = false;
            this.customTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.customTextBox2.ShortcutsEnabled = true;
            this.customTextBox2.Size = new System.Drawing.Size(153, 59);
            this.customTextBox2.TabIndex = 0;
            this.customTextBox2.Texts = "Multiline\r\nText\r\nBox";
            this.customTextBox2.TextsAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.customTextBox2.UnderlinedStyle = false;
            this.customTextBox2.UsePasswordChar = false;
            this.customTextBox2.WordWrap = true;
            // 
            // customButton1
            // 
            this.customButton1.BorderColor = System.Drawing.Color.Red;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Location = new System.Drawing.Point(368, 188);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundedCorners = 0;
            this.customButton1.SelectionColor = System.Drawing.Color.DarkSalmon;
            this.customButton1.Size = new System.Drawing.Size(75, 23);
            this.customButton1.TabIndex = 7;
            this.customButton1.Text = "Button1";
            this.customButton1.UseVisualStyleBackColor = true;
            // 
            // customButton2
            // 
            this.customButton2.BorderColor = System.Drawing.Color.Red;
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Location = new System.Drawing.Point(368, 217);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundedCorners = 0;
            this.customButton2.SelectionColor = System.Drawing.Color.BurlyWood;
            this.customButton2.Size = new System.Drawing.Size(95, 23);
            this.customButton2.TabIndex = 8;
            this.customButton2.Text = "Message Box";
            this.customButton2.UseVisualStyleBackColor = true;
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // customProgressBar1
            // 
            this.customProgressBar1.BorderColor = System.Drawing.Color.Red;
            this.customProgressBar1.ChunksColor = System.Drawing.Color.LightPink;
            this.customProgressBar1.CustomText = "Custom Text";
            this.customProgressBar1.ForeColor = System.Drawing.Color.Black;
            this.customProgressBar1.Location = new System.Drawing.Point(62, 281);
            this.customProgressBar1.Name = "customProgressBar1";
            this.customProgressBar1.Size = new System.Drawing.Size(630, 23);
            this.customProgressBar1.StartTime = null;
            this.customProgressBar1.TabIndex = 9;
            this.customProgressBar1.Value = 50;
            // 
            // customButton3
            // 
            this.customButton3.BorderColor = System.Drawing.Color.Red;
            this.customButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton3.Location = new System.Drawing.Point(487, 188);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundedCorners = 8;
            this.customButton3.SelectionColor = System.Drawing.Color.Blue;
            this.customButton3.Size = new System.Drawing.Size(75, 23);
            this.customButton3.TabIndex = 12;
            this.customButton3.Text = "Button3";
            this.customButton3.UseVisualStyleBackColor = true;
            // 
            // customButton4
            // 
            this.customButton4.BorderColor = System.Drawing.Color.Red;
            this.customButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton4.Location = new System.Drawing.Point(504, 217);
            this.customButton4.Name = "customButton4";
            this.customButton4.RoundedCorners = 19;
            this.customButton4.SelectionColor = System.Drawing.Color.DimGray;
            this.customButton4.Size = new System.Drawing.Size(40, 40);
            this.customButton4.TabIndex = 13;
            this.customButton4.Text = "Ok";
            this.customButton4.UseVisualStyleBackColor = true;
            // 
            // customButton5
            // 
            this.customButton5.BorderColor = System.Drawing.Color.Red;
            this.customButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton5.Location = new System.Drawing.Point(318, 319);
            this.customButton5.Name = "customButton5";
            this.customButton5.RoundedCorners = 0;
            this.customButton5.SelectionColor = System.Drawing.Color.Blue;
            this.customButton5.Size = new System.Drawing.Size(100, 23);
            this.customButton5.TabIndex = 16;
            this.customButton5.Text = "Start Progress";
            this.customButton5.UseVisualStyleBackColor = true;
            this.customButton5.Click += new System.EventHandler(this.customButton5_Click);
            // 
            // customComboBox1
            // 
            this.customComboBox1.BorderColor = System.Drawing.Color.Red;
            this.customComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.customComboBox1.FormattingEnabled = true;
            this.customComboBox1.ItemHeight = 17;
            this.customComboBox1.Items.AddRange(new object[] {
            "Test 1",
            "Test 2",
            "Test 3",
            "Test 4",
            "Test 5"});
            this.customComboBox1.Location = new System.Drawing.Point(18, 218);
            this.customComboBox1.Name = "customComboBox1";
            this.customComboBox1.SelectionColor = System.Drawing.Color.Blue;
            this.customComboBox1.Size = new System.Drawing.Size(121, 23);
            this.customComboBox1.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(743, 354);
            this.Controls.Add(this.customComboBox1);
            this.Controls.Add(this.customButton5);
            this.Controls.Add(this.customButton4);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customProgressBar1);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customTextBox2);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.customCheckBox2);
            this.Controls.Add(this.customCheckBox1);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.customGroupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGroupBox customGroupBox1;
        private CustomDataGridView customDataGridView1;
        private DataGridViewCheckBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private CustomPanel customPanel1;
        private Label label1;
        private CustomCheckBox customCheckBox1;
        private CustomCheckBox customCheckBox2;
        private CustomTextBox customTextBox1;
        private CustomTextBox customTextBox2;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomProgressBar customProgressBar1;
        private CustomButton customButton3;
        private CustomButton customButton4;
        private CustomButton customButton5;
        private CustomComboBox customComboBox1;
    }
}