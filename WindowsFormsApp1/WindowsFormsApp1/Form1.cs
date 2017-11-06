using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bounds = Screen.PrimaryScreen.Bounds;
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\P104\Documents\Task.mdf;Integrated Security=True;Connect Timeout=30";
            var query = "select * from Teacher";
            var obj = new Task();
            obj.Connection(conString);
            obj.Command(query);
            obj.Reader();
            foreach (var item in Task.myList)
            {
                TeacherNameBox.Items.Add(item);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < Task.taskName.Count; i++)
            {
                Task.taskName.Remove(Task.taskName[i]);
                
            
            }
          
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\P104\Documents\Task.mdf;Integrated Security=True;Connect Timeout=30";
            var query = "select Id from Teacher where name='"+TeacherNameBox.SelectedItem+"'";
            int a;
            var obj = new Task();
            obj.Connection(conString);
            obj.Command(query);
            obj.Rdr();

            for (int i = 0; i < Task.teacherId.Count; i++)
            {
                 a=Task.teacherId[Task.teacherId.Count - 1];
                var q = "select TaskName from TaskName where teacher_id=" + a + "";
                obj.Command(q);
                obj.TaskNameRead();
                foreach (var item in Task.taskName)
                {
                    TaskBox.Items.Add(item);
                }
                break;
            }
                     
        }
    }
    class Task
    {
        public string connectionString;
        public string Query;
        public string TeacherName;
        public int TeacherId;
        public string TaskName;
        public static List<string> myList = new List<string>();
        public static List<int> teacherId = new List<int>();
        public static List<string> taskName = new List<string>();

        public SqlConnection Connection(string _connectionString)
        {
            connectionString = _connectionString;
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
        public SqlCommand Command(string _Query)
        {
            Query = _Query;
            SqlCommand cmd = new SqlCommand(_Query, Connection(connectionString));
            cmd.ExecuteNonQuery();
            return cmd;
        }
        public SqlDataReader Reader()
        {
            Connection(connectionString);
            SqlDataReader dataReader = Command(Query).ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read()) {
                    TeacherName = dataReader.GetString(1);                                 
                     myList.Add(TeacherName);
                };
            }
            return dataReader;
        }
        public SqlDataReader Rdr()
        {
            Connection(connectionString);
            SqlDataReader dataReader = Command(Query).ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                   
                    TeacherId = dataReader.GetInt32(0);
                    teacherId.Add(TeacherId);
                 
                };
            }
            return dataReader;
        }
        public SqlDataReader TaskNameRead()
        {
            Connection(connectionString);
            SqlDataReader dataReader = Command(Query).ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    TaskName = dataReader.GetString(0);
                    taskName.Add(TaskName);
                };
            }
            return dataReader;
        }
    }
}
