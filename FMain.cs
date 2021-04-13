using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Praktika
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
            ConnectionString = "Data Source=LAPTOP-9G03HRQJ;Integrated Security=SSPI;Initial Catalog=Praktika";
            conn(ConnectionString, PCK, dataGridView3);
            conn(ConnectionString, Prepodavatel, dataGridView1);
            conn(ConnectionString, Obucenie, dataGridView2);

            conn2(ConnectionString, PCK, comboBox1, "название", "Код_ПЦК");
            conn2(ConnectionString, Prepodavatel, comboBox2, "ФИО", "Код_преподаватель");
        }
        string ConnectionString = "";
        string PCK = "SELECT        Kod_PCK AS Код_ПЦК, Nazvanie AS название, FIOPredPCK AS [ФИО председателя ПЦК] FROM PCK";
        string Prepodavatel = "SELECT Prepodavatel.Kod_Prepod AS [Код_преподаватель], Prepodavatel.FIO AS [ФИО], Prepodavatel.Doljnost AS [должность], Prepodavatel.DataPriem AS [дата приема], Prepodavatel.DataUvol AS [дата увольнения], Prepodavatel.DataPosledObuc AS [дата последнего обучения], Prepodavatel.Kod_PCK AS [Код_ПЦК] FROM            Prepodavatel INNER JOIN PCK ON Prepodavatel.Kod_PCK = PCK.Kod_PCK";
        string Obucenie = "SELECT        Obucenie.Kod_Obuc AS Код_Обучение, Obucenie.VidObuc AS [вид обучения], Obucenie.KolCas AS [кол часов], Obucenie.VidDokument AS [вид документа], Obucenie.DataObuc AS [дата обучения],  Obucenie.MestObuc AS[Место обучение], Obucenie.Kod_Prepod AS Код_Преподаватель FROM            Obucenie INNER JOIN Prepodavatel ON Obucenie.Kod_Prepod = Prepodavatel.Kod_Prepod";
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public void conn(string CS, string cmdT, DataGridView dgv)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);

            //создание объекта DataSet(набор данных)
            DataSet ds = new DataSet();

            //заполнение набора данных результатами запроса select
            Adapter.Fill(ds, "Table");

            //привязка таблицы к набору данных DataSet
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //новое подключенние
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //теперь можно установить соединение, вызывая метод open объекта
            connect.Open();
            //создаем новый экземпляр sqlcommand
            SqlCommand cmd = connect.CreateCommand();
            //определяем тип sqlcommand=stopedprocedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[T1]";
            //создаем параметр
            cmd.Parameters.Add("@Nazvanie", SqlDbType.VarChar, 50);
            cmd.Parameters["@Nazvanie"].Value = textBox1.Text;

            cmd.Parameters.Add("@FIOPredPCK", SqlDbType.VarChar, 50);
            cmd.Parameters["@FIOPredPCK"].Value = textBox2.Text;


            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись изменена!");
            //обновление записей в таблице
            conn(ConnectionString, PCK, dataGridView3);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //новое подключенние
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //теперь можно установить соединение, вызывая метод open объекта
            connect.Open();
            //создаем новый экземпляр sqlcommand
            SqlCommand cmd = connect.CreateCommand();
            //определяем тип sqlcommand=stopedprocedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[T2]";
            //создаем параметр
            cmd.Parameters.Add("@FIO", SqlDbType.VarChar, 50);
            cmd.Parameters["@FIO"].Value = textBox3.Text;

            cmd.Parameters.Add("@Doljnost", SqlDbType.VarChar, 50);
            cmd.Parameters["@Doljnost"].Value = textBox4.Text;

            cmd.Parameters.Add("@DataPriem", SqlDbType.Date);
            cmd.Parameters["@DataPriem"].Value = dateTimePicker1.Value;

            cmd.Parameters.Add("@DataUvol", SqlDbType.Date);
            cmd.Parameters["@DataUvol"].Value = dateTimePicker2.Value;

            cmd.Parameters.Add("@DataPosledObuc", SqlDbType.Date);
            cmd.Parameters["@DataPosledObuc"].Value = dateTimePicker3.Value;

            cmd.Parameters.Add("@Kod_PCK", SqlDbType.Int);
            cmd.Parameters["@Kod_PCK"].Value = comboBox1.SelectedValue;
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись изменена!");
            //обновление записей в таблице
            conn(ConnectionString, PCK, dataGridView1);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //новое подключенние
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //теперь можно установить соединение, вызывая метод open объекта
            connect.Open();
            //создаем новый экземпляр sqlcommand
            SqlCommand cmd = connect.CreateCommand();
            //определяем тип sqlcommand=stopedprocedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызываемой процедуры
            cmd.CommandText = "[T3]";
            //создаем параметр
            cmd.Parameters.Add("@VidObuc", SqlDbType.VarChar, 30);
            cmd.Parameters["@VidObuc"].Value = textBox6.Text;

            cmd.Parameters.Add("@KolCas", SqlDbType.Int);
            cmd.Parameters["@KolCas"].Value = textBox7.Text;

            cmd.Parameters.Add("@VidDokument", SqlDbType.VarChar, 30);
            cmd.Parameters["@VidDokument"].Value = textBox8.Text;

            cmd.Parameters.Add("@DataObuc", SqlDbType.Date);
            cmd.Parameters["@DataObuc"].Value = dateTimePicker4.Value;

            cmd.Parameters.Add("@MestObuc", SqlDbType.VarChar, 30);
            cmd.Parameters["@MestObuc"].Value = textBox9.Text;

            cmd.Parameters.Add("@Kod_Prepod", SqlDbType.Int);
            cmd.Parameters["@Kod_Prepod"].Value = comboBox2.SelectedValue;
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись изменена!");
            //обновление записей в таблице
            conn(ConnectionString, PCK, dataGridView2);
        }
        public void conn2(string CS, string cmdT, ComboBox CB,
            string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);

            //создание объекта DataSet(набор данных)
            DataSet ds = new DataSet();

            //заполнение набора данных результатами запроса select
            Adapter.Fill(ds, "Table");

            //привязка combobox к таблице БД
            CB.DataSource = ds.Tables["table"];
            CB.DisplayMember = field1; // установка отображаемого в списке поля
            CB.ValueMember = field2;// установка ключевого поля
        }
    }
}
