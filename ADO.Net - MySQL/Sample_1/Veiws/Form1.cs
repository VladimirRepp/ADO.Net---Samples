using ADO.Net___Sample_0.Controllers;

namespace ADO.Net___Sample_0
{
    public partial class Form1 : Form
    {
        private DbAutorsController _dbAutorsController;

        public Form1()
        {
            InitializeComponent();
            _dbAutorsController = new();
        }

        private void LoadAndVeiw()
        {
            try
            {
                dataGridView.DataSource = _dbAutorsController.Loading();
                dataGridView.Columns["Id"].Visible = false;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            LoadAndVeiw();
        }

        private void button_Insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Не все полян введены!");
                return;
            }

            try
            {
                if (_dbAutorsController.TryInsert(textBox_FirstName.Text, textBox_LastName.Text))
                    LoadAndVeiw();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Ошибка");            
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите строку данных!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
            {
                MessageBox.Show("Не все данные введены!");
                return;
            }

            int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);

            try
            {
                if (_dbAutorsController.TryUpdate(id, textBox_FirstName.Text, textBox_LastName.Text))
                    LoadAndVeiw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");

            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите даныне.");
                return;
            }

            int id = Convert.ToInt32(dataGridView.CurrentRow.Cells["id"].Value);

            try
            {
                if (_dbAutorsController.TryDelete(id))
                    LoadAndVeiw();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }            
        }
    }
}
