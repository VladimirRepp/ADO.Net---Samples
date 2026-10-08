namespace ADO.Net___Sample_0
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
            dataGridView = new DataGridView();
            label1 = new Label();
            textBox_FirstName = new TextBox();
            textBox_LastName = new TextBox();
            label2 = new Label();
            button_Load = new Button();
            button_Insert = new Button();
            button_Update = new Button();
            button_Delete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 239);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(469, 291);
            dataGridView.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 1;
            label1.Text = "Имя:";
            // 
            // textBox_FirstName
            // 
            textBox_FirstName.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_FirstName.Location = new Point(12, 35);
            textBox_FirstName.Name = "textBox_FirstName";
            textBox_FirstName.Size = new Size(304, 33);
            textBox_FirstName.TabIndex = 2;
            // 
            // textBox_LastName
            // 
            textBox_LastName.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_LastName.Location = new Point(12, 103);
            textBox_LastName.Name = "textBox_LastName";
            textBox_LastName.Size = new Size(304, 33);
            textBox_LastName.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 85);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 3;
            label2.Text = "Фамилия:";
            // 
            // button_Load
            // 
            button_Load.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Load.Location = new Point(337, 27);
            button_Load.Name = "button_Load";
            button_Load.Size = new Size(144, 109);
            button_Load.TabIndex = 5;
            button_Load.Text = "Загрузить";
            button_Load.UseVisualStyleBackColor = true;
            button_Load.Click += button_Load_Click;
            // 
            // button_Insert
            // 
            button_Insert.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Insert.Location = new Point(12, 159);
            button_Insert.Name = "button_Insert";
            button_Insert.Size = new Size(144, 46);
            button_Insert.TabIndex = 6;
            button_Insert.Text = "Добавить";
            button_Insert.UseVisualStyleBackColor = true;
            button_Insert.Click += button_Insert_Click;
            // 
            // button_Update
            // 
            button_Update.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Update.Location = new Point(172, 159);
            button_Update.Name = "button_Update";
            button_Update.Size = new Size(144, 46);
            button_Update.TabIndex = 7;
            button_Update.Text = "Обновить";
            button_Update.UseVisualStyleBackColor = true;
            button_Update.Click += button_Update_Click;
            // 
            // button_Delete
            // 
            button_Delete.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Delete.Location = new Point(337, 159);
            button_Delete.Name = "button_Delete";
            button_Delete.Size = new Size(144, 46);
            button_Delete.TabIndex = 8;
            button_Delete.Text = "Удалить";
            button_Delete.UseVisualStyleBackColor = true;
            button_Delete.Click += button_Delete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 548);
            Controls.Add(button_Delete);
            Controls.Add(button_Update);
            Controls.Add(button_Insert);
            Controls.Add(button_Load);
            Controls.Add(textBox_LastName);
            Controls.Add(label2);
            Controls.Add(textBox_FirstName);
            Controls.Add(label1);
            Controls.Add(dataGridView);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private Label label1;
        private TextBox textBox_FirstName;
        private TextBox textBox_LastName;
        private Label label2;
        private Button button_Load;
        private Button button_Insert;
        private Button button_Update;
        private Button button_Delete;
    }
}
