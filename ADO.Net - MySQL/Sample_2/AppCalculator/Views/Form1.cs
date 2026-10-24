using AppCalculator.Controllers;
using AppCalculator.Database;
using AppCalculator.Logic;

namespace AppCalculator
{
    public partial class Form1 : Form
    {
        private Calculator _calculator;
        private TranslatorTemperature _translatorTemperature;

        private DbHistoryController _dbHistoryCalculatorController;
        private DbConnector _dbConnector;

        private string _tableNameCalculator = "history_calculator";
        private string _tableNameTranslatorTemperature = "history_translator_temperature";

        public Form1()
        {
            InitializeComponent();
            _calculator = new();
            _translatorTemperature = new();

            _dbConnector = new();
            _dbHistoryCalculatorController = new(_dbConnector);

            textBox_CalculateHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameCalculator);
            textBox_TranslateTemperatureHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameTranslatorTemperature);
        }

        #region === Page #1. Calculator ===
        private void button_Plus_Click(object sender, EventArgs e)
        {
            float a = 0, b = 0;

            if(!float.TryParse(textBox_FisrtNum.Text, out a))
            {
                MessageBox.Show("Неверный ввод первого числа!", "Ошибка!");
                return;
            }

            if (!float.TryParse(textBox_SecondNum.Text, out b))
            {
                MessageBox.Show("Неверный ввод второго числа!", "Ошибка!");
                return;
            }

            try
            {
                float result = _calculator.Sum(a, b);
                textBox_Result.Text = result.ToString();

                _dbHistoryCalculatorController.InsertHistoryCalulator(_tableNameCalculator, $"{a} + {b} = {result}");
                textBox_CalculateHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameCalculator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void button_Minus_Click(object sender, EventArgs e)
        {
            float a = 0, b = 0;

            if (!float.TryParse(textBox_FisrtNum.Text, out a))
            {
                MessageBox.Show("Неверный ввод первого числа!", "Ошибка!");
                return;
            }

            if (!float.TryParse(textBox_SecondNum.Text, out b))
            {
                MessageBox.Show("Неверный ввод второго числа!", "Ошибка!");
                return;
            }

            try
            {
                float result = _calculator.Subtract(a, b);
                textBox_Result.Text = result.ToString();

                _dbHistoryCalculatorController.InsertHistoryCalulator(_tableNameCalculator, $"{a} + {b} = {result}");
                textBox_CalculateHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameCalculator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void button_Mult_Click(object sender, EventArgs e)
        {
            float a = 0, b = 0;

            if (!float.TryParse(textBox_FisrtNum.Text, out a))
            {
                MessageBox.Show("Неверный ввод первого числа!", "Ошибка!");
                return;
            }

            if (!float.TryParse(textBox_SecondNum.Text, out b))
            {
                MessageBox.Show("Неверный ввод второго числа!", "Ошибка!");
                return;
            }

            try
            {
                float result = _calculator.Mult(a, b);
                textBox_Result.Text = result.ToString();

                _dbHistoryCalculatorController.InsertHistoryCalulator(_tableNameCalculator, $"{a} + {b} = {result}");
                textBox_CalculateHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameCalculator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void button_Dif_Click(object sender, EventArgs e)
        {
            float a = 0, b = 0;

            if (!float.TryParse(textBox_FisrtNum.Text, out a))
            {
                MessageBox.Show("Неверный ввод первого числа!", "Ошибка!");
                return;
            }

            if (!float.TryParse(textBox_SecondNum.Text, out b))
            {
                MessageBox.Show("Неверный ввод второго числа!", "Ошибка!");
                return;
            }

            try
            {
                float result = _calculator.Difference(a, b);
                textBox_Result.Text = result.ToString();

                _dbHistoryCalculatorController.InsertHistoryCalulator(_tableNameCalculator, $"{a} + {b} = {result}");
                textBox_CalculateHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameCalculator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        #endregion

        #region === Page #2. Temperature Converter ===
        private void button_TranslateTemperature_Click(object sender, EventArgs e)
        {
            if(!radioButton_Celsius.Checked && 
                !radioButton_Fahrenheit.Checked &&
                !radioButton_Kelvins.Checked)
            {
                MessageBox.Show("Выберите из какой величины переводите!","Внимание!");
                return;
            }

            double value;
            double result_Celsius = 0;
            double result_Fahrenheit = 0;
            double result_Kelvins = 0;

            EStateTemperature from;
            EStateTemperature to_1;
            EStateTemperature to_2;

            if (radioButton_Celsius.Checked)
            {
                if(!double.TryParse(textBox_Celsius.Text, out value))
                {
                    MessageBox.Show("Неверное значение поля ввода!","Ошибка!");
                    return;
                }

                result_Celsius = value;

                from = EStateTemperature.Celsius;
                to_1 = EStateTemperature.Fahrenheit;
                to_2 = EStateTemperature.Kelvin;

                try
                {
                    if (!_translatorTemperature.TryConvert(value,
                        from, to_1,
                        out result_Fahrenheit))
                        MessageBox.Show("Ошибка в переводе!", "Ошибка!");

                    if (!_translatorTemperature.TryConvert(value,
                        from, to_2,
                        out result_Kelvins))
                        MessageBox.Show("Ошибка в переводе!", "Ошибка!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (radioButton_Fahrenheit.Checked)
            {
                if (!double.TryParse(textBox_Faringate.Text, out value))
                {
                    MessageBox.Show("Неверное значение поля ввода!", "Ошибка!");
                    return;
                }

                result_Fahrenheit = value;

                from = EStateTemperature.Fahrenheit;
                to_1 = EStateTemperature.Celsius;
                to_2 = EStateTemperature.Kelvin;

                try
                {
                    if (!_translatorTemperature.TryConvert(value,
                        from, to_1,
                        out result_Celsius))
                        MessageBox.Show("Ошибка в переводе!", "Ошибка!");

                    if (!_translatorTemperature.TryConvert(value,
                        from, to_2,
                        out result_Kelvins))
                        MessageBox.Show("Ошибка в переводе!", "Ошибка!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else
            {
                if (!double.TryParse(textBox_Kelvins.Text, out value))
                {
                    MessageBox.Show("Неверное значение поля ввода!", "Ошибка!");
                    return;
                }

                result_Kelvins = value;

                from = EStateTemperature.Kelvin;
                to_1 = EStateTemperature.Fahrenheit;
                to_2 = EStateTemperature.Celsius;

                try
                {
                    if (!_translatorTemperature.TryConvert(value,
                        from, to_1,
                        out result_Fahrenheit))
                        MessageBox.Show("Ошибка в переводе!","Ошибка!");

                    if (!_translatorTemperature.TryConvert(value,
                        from, to_2,
                        out result_Celsius))
                        MessageBox.Show("Ошибка в переводе!", "Ошибка!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }

            textBox_Celsius.Text = result_Celsius.ToString();
            textBox_Faringate.Text = result_Fahrenheit.ToString();
            textBox_Kelvins.Text = result_Kelvins.ToString();

            _dbHistoryCalculatorController.InsertHistoryCalulator(_tableNameTranslatorTemperature, 
                $"From = {from.ToString()}; С = {result_Celsius}; F = {result_Fahrenheit}; K = {result_Kelvins}");
            textBox_TranslateTemperatureHistory.Text = _dbHistoryCalculatorController.LoadingHistoryCalculator(_tableNameTranslatorTemperature);
        }
        #endregion
    }
}
