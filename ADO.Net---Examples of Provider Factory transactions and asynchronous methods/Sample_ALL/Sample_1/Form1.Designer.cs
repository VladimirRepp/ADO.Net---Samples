namespace Sample_1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.главноеМенюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_Providers = new System.Windows.Forms.ComboBox();
            this.button_GetAllProviders = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ConnectionString = new System.Windows.Forms.TextBox();
            this.textBox_Request = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Request = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.главноеМенюToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1064, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // главноеМенюToolStripMenuItem
            // 
            this.главноеМенюToolStripMenuItem.Name = "главноеМенюToolStripMenuItem";
            this.главноеМенюToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.главноеМенюToolStripMenuItem.Text = "Главное меню";
            this.главноеМенюToolStripMenuItem.Click += new System.EventHandler(this.главноеМенюToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // comboBox_Providers
            // 
            this.comboBox_Providers.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_Providers.FormattingEnabled = true;
            this.comboBox_Providers.Location = new System.Drawing.Point(23, 59);
            this.comboBox_Providers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_Providers.Name = "comboBox_Providers";
            this.comboBox_Providers.Size = new System.Drawing.Size(639, 37);
            this.comboBox_Providers.TabIndex = 1;
            this.comboBox_Providers.SelectedIndexChanged += new System.EventHandler(this.comboBox_Providers_SelectedIndexChanged);
            // 
            // button_GetAllProviders
            // 
            this.button_GetAllProviders.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_GetAllProviders.Location = new System.Drawing.Point(689, 53);
            this.button_GetAllProviders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_GetAllProviders.Name = "button_GetAllProviders";
            this.button_GetAllProviders.Size = new System.Drawing.Size(353, 46);
            this.button_GetAllProviders.TabIndex = 2;
            this.button_GetAllProviders.Text = "Получить всех провайдеров";
            this.button_GetAllProviders.UseVisualStyleBackColor = true;
            this.button_GetAllProviders.Click += new System.EventHandler(this.button_GetAllProviders_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Строка подключения:";
            // 
            // textBox_ConnectionString
            // 
            this.textBox_ConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_ConnectionString.Location = new System.Drawing.Point(24, 130);
            this.textBox_ConnectionString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ConnectionString.Name = "textBox_ConnectionString";
            this.textBox_ConnectionString.ReadOnly = true;
            this.textBox_ConnectionString.Size = new System.Drawing.Size(1019, 34);
            this.textBox_ConnectionString.TabIndex = 4;
            // 
            // textBox_Request
            // 
            this.textBox_Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Request.Location = new System.Drawing.Point(23, 199);
            this.textBox_Request.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Request.Name = "textBox_Request";
            this.textBox_Request.Size = new System.Drawing.Size(639, 34);
            this.textBox_Request.TabIndex = 6;
            this.textBox_Request.TextChanged += new System.EventHandler(this.textBox_Request_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "SQL запрос:";
            // 
            // button_Request
            // 
            this.button_Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Request.Location = new System.Drawing.Point(689, 193);
            this.button_Request.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Request.Name = "button_Request";
            this.button_Request.Size = new System.Drawing.Size(353, 46);
            this.button_Request.TabIndex = 7;
            this.button_Request.Text = "Выполнить запрос";
            this.button_Request.UseVisualStyleBackColor = true;
            this.button_Request.Click += new System.EventHandler(this.button_Request_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(23, 262);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1019, 427);
            this.dataGridView.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Доступные провайдеры:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 703);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button_Request);
            this.Controls.Add(this.textBox_Request);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_ConnectionString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_GetAllProviders);
            this.Controls.Add(this.comboBox_Providers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1. Фабрика подключений";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox_Providers;
        private System.Windows.Forms.Button button_GetAllProviders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_ConnectionString;
        private System.Windows.Forms.TextBox textBox_Request;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Request;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripMenuItem главноеМенюToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}

