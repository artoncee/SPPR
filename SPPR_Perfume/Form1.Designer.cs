namespace SPPR_Perfume
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtNumberOfColumns = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mathModelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.button_reference = new System.Windows.Forms.Button();
            this.button_savePDF = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(56, 68);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(823, 296);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnLoad.Location = new System.Drawing.Point(56, 18);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(166, 33);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Загрузить файл";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtNumberOfColumns
            // 
            this.txtNumberOfColumns.Location = new System.Drawing.Point(9, 82);
            this.txtNumberOfColumns.Name = "txtNumberOfColumns";
            this.txtNumberOfColumns.Size = new System.Drawing.Size(110, 22);
            this.txtNumberOfColumns.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(143, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите количество рассматриваемых";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = " парюфмерных изделий:";
            // 
            // mathModelButton
            // 
            this.mathModelButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mathModelButton.Location = new System.Drawing.Point(702, 12);
            this.mathModelButton.Name = "mathModelButton";
            this.mathModelButton.Size = new System.Drawing.Size(177, 36);
            this.mathModelButton.TabIndex = 9;
            this.mathModelButton.Text = "Рассчитать опт. план";
            this.mathModelButton.UseVisualStyleBackColor = false;
            this.mathModelButton.Click += new System.EventHandler(this.mathModelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNumberOfColumns);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(56, 383);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 139);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка:";
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(379, 402);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(500, 117);
            this.richTextBoxResult.TabIndex = 11;
            this.richTextBoxResult.Text = "";
            // 
            // button_reference
            // 
            this.button_reference.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_reference.Location = new System.Drawing.Point(379, 534);
            this.button_reference.Name = "button_reference";
            this.button_reference.Size = new System.Drawing.Size(246, 36);
            this.button_reference.TabIndex = 12;
            this.button_reference.Text = "Справка";
            this.button_reference.UseVisualStyleBackColor = false;
            this.button_reference.Visible = false;
            this.button_reference.Click += new System.EventHandler(this.button_reference_Click);
            // 
            // button_savePDF
            // 
            this.button_savePDF.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_savePDF.Location = new System.Drawing.Point(631, 534);
            this.button_savePDF.Name = "button_savePDF";
            this.button_savePDF.Size = new System.Drawing.Size(248, 36);
            this.button_savePDF.TabIndex = 13;
            this.button_savePDF.Text = "Загрузить отчет";
            this.button_savePDF.UseVisualStyleBackColor = false;
            this.button_savePDF.Visible = false;
            this.button_savePDF.Click += new System.EventHandler(this.button_savePDF_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Результат";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(942, 589);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_savePDF);
            this.Controls.Add(this.button_reference);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mathModelButton);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView);
            this.Name = "Form1";
            this.Text = "Парфюмерная лаборатория (СППР)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtNumberOfColumns;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mathModelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Button button_reference;
        private System.Windows.Forms.Button button_savePDF;
        private System.Windows.Forms.Label label3;
    }
}

