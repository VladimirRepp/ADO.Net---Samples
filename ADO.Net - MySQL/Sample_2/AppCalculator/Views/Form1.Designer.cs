namespace AppCalculator
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
            label1 = new Label();
            textBox_FisrtNum = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            textBox_CalculateHistory = new TextBox();
            label4 = new Label();
            textBox_Result = new TextBox();
            label3 = new Label();
            button_Div = new Button();
            button_Mult = new Button();
            button_Minus = new Button();
            button_Plus = new Button();
            textBox_SecondNum = new TextBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            textBox_TranslateTemperatureHistory = new TextBox();
            button_TranslateTemperature = new Button();
            groupBox1 = new GroupBox();
            radioButton_Fahrenheit = new RadioButton();
            radioButton_Kelvins = new RadioButton();
            radioButton_Celsius = new RadioButton();
            textBox_Kelvins = new TextBox();
            label7 = new Label();
            textBox_Faringate = new TextBox();
            label6 = new Label();
            textBox_Celsius = new TextBox();
            label5 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 15);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 0;
            label1.Text = "Число №1:";
            // 
            // textBox_FisrtNum
            // 
            textBox_FisrtNum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_FisrtNum.Location = new Point(6, 33);
            textBox_FisrtNum.Name = "textBox_FisrtNum";
            textBox_FisrtNum.Size = new Size(139, 33);
            textBox_FisrtNum.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 426);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(textBox_CalculateHistory);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(textBox_Result);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(button_Div);
            tabPage1.Controls.Add(button_Mult);
            tabPage1.Controls.Add(button_Minus);
            tabPage1.Controls.Add(button_Plus);
            tabPage1.Controls.Add(textBox_SecondNum);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(textBox_FisrtNum);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 398);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "1. Калькулятор";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox_CalculateHistory
            // 
            textBox_CalculateHistory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_CalculateHistory.Location = new Point(330, 33);
            textBox_CalculateHistory.Multiline = true;
            textBox_CalculateHistory.Name = "textBox_CalculateHistory";
            textBox_CalculateHistory.Size = new Size(432, 245);
            textBox_CalculateHistory.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(330, 15);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 10;
            label4.Text = "История:";
            // 
            // textBox_Result
            // 
            textBox_Result.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_Result.Location = new Point(9, 245);
            textBox_Result.Name = "textBox_Result";
            textBox_Result.Size = new Size(306, 33);
            textBox_Result.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 227);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 8;
            label3.Text = "Результат:";
            // 
            // button_Div
            // 
            button_Div.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Div.Location = new Point(80, 150);
            button_Div.Name = "button_Div";
            button_Div.Size = new Size(65, 63);
            button_Div.TabIndex = 7;
            button_Div.Text = "/";
            button_Div.UseVisualStyleBackColor = true;
            button_Div.Click += button_Dif_Click;
            // 
            // button_Mult
            // 
            button_Mult.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Mult.Location = new Point(9, 150);
            button_Mult.Name = "button_Mult";
            button_Mult.Size = new Size(65, 63);
            button_Mult.TabIndex = 6;
            button_Mult.Text = "*";
            button_Mult.UseVisualStyleBackColor = true;
            button_Mult.Click += button_Mult_Click;
            // 
            // button_Minus
            // 
            button_Minus.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Minus.Location = new Point(80, 81);
            button_Minus.Name = "button_Minus";
            button_Minus.Size = new Size(65, 63);
            button_Minus.TabIndex = 5;
            button_Minus.Text = "-";
            button_Minus.UseVisualStyleBackColor = true;
            button_Minus.Click += button_Minus_Click;
            // 
            // button_Plus
            // 
            button_Plus.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_Plus.Location = new Point(8, 81);
            button_Plus.Name = "button_Plus";
            button_Plus.Size = new Size(65, 63);
            button_Plus.TabIndex = 4;
            button_Plus.Text = "+";
            button_Plus.UseVisualStyleBackColor = true;
            button_Plus.Click += button_Plus_Click;
            // 
            // textBox_SecondNum
            // 
            textBox_SecondNum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_SecondNum.Location = new Point(176, 33);
            textBox_SecondNum.Name = "textBox_SecondNum";
            textBox_SecondNum.Size = new Size(139, 33);
            textBox_SecondNum.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(176, 15);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Число №2:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(button_TranslateTemperature);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(textBox_Kelvins);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(textBox_Faringate);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(textBox_Celsius);
            tabPage2.Controls.Add(label5);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 398);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "2. Конвертер Температуры";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox_TranslateTemperatureHistory);
            groupBox2.Location = new Point(332, 11);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(430, 381);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "История операций";
            // 
            // textBox_TranslateTemperatureHistory
            // 
            textBox_TranslateTemperatureHistory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_TranslateTemperatureHistory.Location = new Point(6, 19);
            textBox_TranslateTemperatureHistory.Multiline = true;
            textBox_TranslateTemperatureHistory.Name = "textBox_TranslateTemperatureHistory";
            textBox_TranslateTemperatureHistory.Size = new Size(418, 343);
            textBox_TranslateTemperatureHistory.TabIndex = 12;
            // 
            // button_TranslateTemperature
            // 
            button_TranslateTemperature.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_TranslateTemperature.Location = new Point(161, 149);
            button_TranslateTemperature.Name = "button_TranslateTemperature";
            button_TranslateTemperature.Size = new Size(153, 51);
            button_TranslateTemperature.TabIndex = 9;
            button_TranslateTemperature.Text = "Перевести";
            button_TranslateTemperature.UseVisualStyleBackColor = true;
            button_TranslateTemperature.Click += this.button_TranslateTemperature_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton_Fahrenheit);
            groupBox1.Controls.Add(radioButton_Kelvins);
            groupBox1.Controls.Add(radioButton_Celsius);
            groupBox1.Location = new Point(161, 11);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(153, 116);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Выберите начальную величину";
            // 
            // radioButton_Fahrenheit
            // 
            radioButton_Fahrenheit.AutoSize = true;
            radioButton_Fahrenheit.Location = new Point(21, 57);
            radioButton_Fahrenheit.Name = "radioButton_Fahrenheit";
            radioButton_Fahrenheit.Size = new Size(86, 19);
            radioButton_Fahrenheit.TabIndex = 2;
            radioButton_Fahrenheit.TabStop = true;
            radioButton_Fahrenheit.Text = "Фаренгейт ";
            radioButton_Fahrenheit.UseVisualStyleBackColor = true;
            // 
            // radioButton_Kelvins
            // 
            radioButton_Kelvins.AutoSize = true;
            radioButton_Kelvins.Location = new Point(21, 82);
            radioButton_Kelvins.Name = "radioButton_Kelvins";
            radioButton_Kelvins.Size = new Size(80, 19);
            radioButton_Kelvins.TabIndex = 1;
            radioButton_Kelvins.TabStop = true;
            radioButton_Kelvins.Text = "Кельвины";
            radioButton_Kelvins.UseVisualStyleBackColor = true;
            // 
            // radioButton_Celsius
            // 
            radioButton_Celsius.AutoSize = true;
            radioButton_Celsius.Location = new Point(21, 32);
            radioButton_Celsius.Name = "radioButton_Celsius";
            radioButton_Celsius.Size = new Size(72, 19);
            radioButton_Celsius.TabIndex = 0;
            radioButton_Celsius.TabStop = true;
            radioButton_Celsius.Text = "Цельсия";
            radioButton_Celsius.UseVisualStyleBackColor = true;
            // 
            // textBox_Kelvins
            // 
            textBox_Kelvins.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_Kelvins.Location = new Point(6, 167);
            textBox_Kelvins.Name = "textBox_Kelvins";
            textBox_Kelvins.Size = new Size(139, 33);
            textBox_Kelvins.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 149);
            label7.Name = "label7";
            label7.Size = new Size(14, 15);
            label7.TabIndex = 6;
            label7.Text = "К";
            // 
            // textBox_Faringate
            // 
            textBox_Faringate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_Faringate.Location = new Point(6, 94);
            textBox_Faringate.Name = "textBox_Faringate";
            textBox_Faringate.Size = new Size(139, 33);
            textBox_Faringate.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 76);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 4;
            label6.Text = "℉";
            // 
            // textBox_Celsius
            // 
            textBox_Celsius.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_Celsius.Location = new Point(6, 29);
            textBox_Celsius.Name = "textBox_Celsius";
            textBox_Celsius.Size = new Size(139, 33);
            textBox_Celsius.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 11);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 2;
            label5.Text = "℃";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox textBox_FisrtNum;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox_Result;
        private Label label3;
        private Button button_Div;
        private Button button_Mult;
        private Button button_Minus;
        private Button button_Plus;
        private TextBox textBox_SecondNum;
        private Label label2;
        private TextBox textBox_CalculateHistory;
        private Label label4;
        private TextBox textBox_Kelvins;
        private Label label7;
        private TextBox textBox_Faringate;
        private Label label6;
        private TextBox textBox_Celsius;
        private Label label5;
        private Button button_TranslateTemperature;
        private GroupBox groupBox1;
        private RadioButton radioButton_Fahrenheit;
        private RadioButton radioButton_Kelvins;
        private RadioButton radioButton_Celsius;
        private GroupBox groupBox2;
        private TextBox textBox_TranslateTemperatureHistory;
    }
}
