using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.DataBaseApplication
{
    public partial class DataBaseForm : Form
    {
        DataSet ds;
        int FromID;
        string DatabaseString;
        public DataBaseForm()
        {
            InitializeComponent();


        }
        //加载窗体将数据表加载到datagridview上
        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadData("select loginid from TblUser");
            //LoadData("select * from TblUser");
            LoadData(DatabaseString);
            //   LoadData("select count(*) from ProductionStatistics");
            //LoadData("select *form  Description");


        }
        public DataBaseForm(Panel panel, int FormIndex, string DataName = "DataName", string DatabaseStr = "select count(*) from ProductionStatistics")
        {
            InitializeComponent();
            this.TopLevel = false;
            panel.Controls.Add(this);
            this.Size = panel.Size;
            FromID = FormIndex;
            labelTableName.Text = DataName + "  Table";
            DatabaseString = DatabaseStr;
            this.Show();

        }



        private void LoadData(string sql)
        {


            DataBaseHelper dbH = new DataBaseHelper();
            ds = dbH.Query(sql);
            dataGridView1.DataSource = ds.Tables[0];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (dataGridView1.Columns[i].ValueType == typeof(DateTime))
                    {
                        dataGridView1.Columns[i].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    }
                }
            }
        }

        // 添加数据
        private void buttonAdd_Click(object sender, EventArgs e)
        {

            Add("insert into StatusDescription(StatusID,StatusName,Description) values (7,'你猜','管不了那么多啊')", "select* from  StatusDescription");
        }

        private void Add(string sqlQuery, string sqlLoad)
        {
            DataBaseHelper dbH = new DataBaseHelper();
            dbH.ExecuteSql(sqlQuery);
            MessageBox.Show("Succeed Add");
            LoadData(sqlLoad);
        }
        //删除数据
        private void buttonDel_Click(object sender, EventArgs e)
        {
            Delete("delete from StatusDescription where StatusDescription.StatusID=2", "select *from  StatusDescription");
        }

        private void Delete(string sqlQuery, string sqlLoad)
        {
            DataBaseHelper dbH = new DataBaseHelper();
            dbH.ExecuteSql(sqlQuery);
            MessageBox.Show("Succeed Delete");
            LoadData(sqlLoad);
        }
        //修改数据
        private void buttonModify_Click(object sender, EventArgs e)
        {
            Modify("update BooksRate set BooksName='知道'where Count=27", "select *from  BooksRate");
        }

        private void Modify(string sqlQuery, string sqlLoad)
        {
            DataBaseHelper dbH = new DataBaseHelper();
            dbH.ExecuteSql(sqlQuery);
            MessageBox.Show("Succeed Modify");
            LoadData(sqlLoad);
        }
        //根据时间查询
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            TimeQuery("select * from ProductionStatistics where ProductionStatistics.DataTime between # " + dateTimePickerStart.Value + "# and #" + dateTimePickerEnd.Value + "#");
        }

        private void TimeQuery(string sqlQuery)
        {
            DataBaseHelper dbH = new DataBaseHelper();
            DataTable dt = dbH.ExecuteQuery(sqlQuery);
            MessageBox.Show("Successd Query");
            dataGridView1.DataSource = dt;
        }

        private void button1SELECT_Click(object sender, EventArgs e)
        {
            LoadData(textBoxDatabaseStr.Text);
        }

    }
}
