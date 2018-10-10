using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WorldGeneralLib.IO;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.Table
{
    public partial class FormTableDriver : Form
    {
        private object _lock = new object();
        private int _iJogSpdPresent = 10;
        public TableDriver tableDriver = null;
        public FormTableDriver()
        {
            InitializeComponent();
        }
        public FormTableDriver(TableDriver driver) : this()
        {
            tableDriver = driver;
        }

        #region Table Status
        public void InitTableStatusView()
        {
            try
            {
                dataGridViewTableSta.Rows.Clear();
                dataGridViewTableSta.Rows.Add(new object[] { "Curr Pos", "0.000 mm", "0.000 mm", "0.000 mm", "0.000 mm", "0.000 mm", "0.000 mm", "0.000 mm", "0.000 mm" });
                dataGridViewTableSta.Rows.Add(new object[] { "Axis Alarm", " ", " ", " ", " ", " ", " ", " ", "" });
                dataGridViewTableSta.Rows.Add(new object[] { "CWL", " ", " ", " ", " ", " ", " ", " ", "" });
                dataGridViewTableSta.Rows.Add(new object[] { "Home Status", " ", " ", " ", " ", " ", " ", " ", "" });
                dataGridViewTableSta.Rows.Add(new object[] { "CCWL", " ", " ", " ", " ", " ", " ", " ", "" });
                dataGridViewTableSta.Rows.Add(new object[] { "Axis Moving", " ", " ", " ", " ", " ", " ", " ", "" });

                for (int row = 1; row < dataGridViewTableSta.Rows.Count; row++)
                {
                    for (int column = 1; column < dataGridViewTableSta.Columns.Count; column++)
                    {
                        dataGridViewTableSta.Rows[row].Cells[column].Style.ForeColor = Color.DarkRed;
                    }
                }
                dataGridViewTableSta.ClearSelection();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ThreadRefreshTableStatusHandler()
        {
            string strTable = "";
            try
            {
                if (this.InvokeRequired)
                {
                    Action action = () =>
                    {
                        if (cbTableSel.SelectedIndex < 0 || tableDriver == null)
                        {
                            return;
                        }
                        TableData table = tableDriver.tableData;
                        #region Row - 0
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[1].Value = table.dicTableAxisItem["X"].Active ? tableDriver.CurrentX.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[1].Style.ForeColor = table.dicTableAxisItem["X"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[1].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[2].Value = table.dicTableAxisItem["Y"].Active ? tableDriver.CurrentY.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[2].Style.ForeColor = table.dicTableAxisItem["Y"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[2].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[3].Value = table.dicTableAxisItem["Z"].Active ? tableDriver.CurrentZ.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[3].Style.ForeColor = table.dicTableAxisItem["Z"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[3].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[4].Value = table.dicTableAxisItem["U"].Active ? tableDriver.CurrentU.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[4].Style.ForeColor = table.dicTableAxisItem["U"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[4].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[5].Value = table.dicTableAxisItem["A"].Active ? tableDriver.CurrentA.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[5].Style.ForeColor = table.dicTableAxisItem["A"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[5].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[6].Value = table.dicTableAxisItem["B"].Active ? tableDriver.CurrentB.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[6].Style.ForeColor = table.dicTableAxisItem["B"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[6].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[7].Value = table.dicTableAxisItem["C"].Active ? tableDriver.CurrentC.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[7].Style.ForeColor = table.dicTableAxisItem["C"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[7].Style.ForeColor = Color.DarkRed;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            dataGridViewTableSta.Rows[0].Cells[8].Value = table.dicTableAxisItem["D"].Active ? tableDriver.CurrentD.ToString("0.000") + " mm" : "Disable";
                            dataGridViewTableSta.Rows[0].Cells[8].Style.ForeColor = table.dicTableAxisItem["D"].Active ? Color.Black : Color.DarkRed;
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[0].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[0].Cells[8].Style.ForeColor = Color.DarkRed;
                        }
                        #endregion
                        #region Row - 1
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            if (table.dicTableAxisItem["X"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[1].Value = " ";
                                dataGridViewTableSta.Rows[1].Cells[1].Style.BackColor = Color.White;
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[1].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[1].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[1].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            if (table.dicTableAxisItem["Y"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[2].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[2].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[2].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[2].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            if (table.dicTableAxisItem["Z"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[3].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[3].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[3].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[3].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            if (table.dicTableAxisItem["U"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[4].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[4].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[4].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[4].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            if (table.dicTableAxisItem["A"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[5].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[5].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[5].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[5].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            if (table.dicTableAxisItem["B"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[6].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[6].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[6].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[6].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            if (table.dicTableAxisItem["C"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[7].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[7].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[7].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[7].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            if (table.dicTableAxisItem["D"].Active)
                            {
                                dataGridViewTableSta.Rows[1].Cells[8].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[1].Cells[8].Value = "Disable";
                                dataGridViewTableSta.Rows[1].Cells[8].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[1].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[1].Cells[8].Style.BackColor = Color.White;
                        }
                        #endregion
                        #region Row - 2
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            if (table.dicTableAxisItem["X"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[1].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[1].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[1].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[1].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            if (table.dicTableAxisItem["Y"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[2].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[2].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[2].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[2].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            if (table.dicTableAxisItem["Z"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[3].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[3].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[3].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[3].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            if (table.dicTableAxisItem["U"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[4].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[4].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[4].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[4].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            if (table.dicTableAxisItem["A"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[5].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[5].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[5].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[5].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            if (table.dicTableAxisItem["B"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[6].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[6].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[6].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[6].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            if (table.dicTableAxisItem["C"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[7].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[7].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[7].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[7].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            if (table.dicTableAxisItem["D"].Active)
                            {
                                dataGridViewTableSta.Rows[2].Cells[8].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[2].Cells[8].Value = "Disable";
                                dataGridViewTableSta.Rows[2].Cells[8].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[2].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[2].Cells[8].Style.BackColor = Color.White;
                        }
                        #endregion
                        #region Row - 3
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            if (table.dicTableAxisItem["X"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[1].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[1].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[1].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[1].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            if (table.dicTableAxisItem["Y"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[2].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[2].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[2].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[2].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            if (table.dicTableAxisItem["Z"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[3].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[3].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[3].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[3].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            if (table.dicTableAxisItem["U"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[4].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[4].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[4].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[4].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            if (table.dicTableAxisItem["A"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[5].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[5].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[5].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[5].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            if (table.dicTableAxisItem["B"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[6].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[6].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[6].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[6].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            if (table.dicTableAxisItem["C"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[7].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[7].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[7].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[7].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            if (table.dicTableAxisItem["D"].Active)
                            {
                                dataGridViewTableSta.Rows[3].Cells[8].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[3].Cells[8].Value = "Disable";
                                dataGridViewTableSta.Rows[3].Cells[8].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[3].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[3].Cells[8].Style.BackColor = Color.White;
                        }
                        #endregion
                        #region Row - 4
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            if (table.dicTableAxisItem["X"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[1].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[1].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[1].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[1].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            if (table.dicTableAxisItem["Y"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[2].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[2].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[2].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[2].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            if (table.dicTableAxisItem["Z"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[3].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[3].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[3].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[3].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            if (table.dicTableAxisItem["U"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[4].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[4].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[4].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[4].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            if (table.dicTableAxisItem["A"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[5].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[5].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[5].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[5].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            if (table.dicTableAxisItem["B"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[6].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[6].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[6].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[6].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            if (table.dicTableAxisItem["C"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[7].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[7].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[7].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[7].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            if (table.dicTableAxisItem["D"].Active)
                            {
                                dataGridViewTableSta.Rows[4].Cells[8].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[4].Cells[8].Value = "Disable";
                                dataGridViewTableSta.Rows[4].Cells[8].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[4].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[4].Cells[8].Style.BackColor = Color.White;
                        }
                        #endregion
                        #region Row - 5
                        if (table.dicTableAxisItem.ContainsKey("X"))
                        {
                            if (table.dicTableAxisItem["X"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[1].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[1].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[1].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[1].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[1].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Y"))
                        {
                            if (table.dicTableAxisItem["Y"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[2].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[2].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[2].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[2].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[2].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("Z"))
                        {
                            if (table.dicTableAxisItem["Z"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[3].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[3].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[3].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[3].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[3].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("U"))
                        {
                            if (table.dicTableAxisItem["U"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[4].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[4].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[4].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[4].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[4].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("A"))
                        {
                            if (table.dicTableAxisItem["A"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[5].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[5].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[5].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[5].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[5].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("B"))
                        {
                            if (table.dicTableAxisItem["B"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[6].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[6].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[6].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[6].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[6].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("C"))
                        {
                            if (table.dicTableAxisItem["C"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[7].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[7].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[7].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[7].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[7].Style.BackColor = Color.White;
                        }
                        if (table.dicTableAxisItem.ContainsKey("D"))
                        {
                            if (table.dicTableAxisItem["D"].Active)
                            {
                                dataGridViewTableSta.Rows[5].Cells[8].Value = " ";
                                //
                            }
                            else
                            {
                                dataGridViewTableSta.Rows[5].Cells[8].Value = "Disable";
                                dataGridViewTableSta.Rows[5].Cells[8].Style.BackColor = Color.White;
                            }
                        }
                        else
                        {
                            dataGridViewTableSta.Rows[5].Cells[8].Value = "Disable";
                            dataGridViewTableSta.Rows[5].Cells[8].Style.BackColor = Color.White;
                        }
                        #endregion
                    };
                    this.Invoke(action);
                }
                else
                {
                    strTable = cbTableSel.SelectedItem.ToString();
                }
            }
            catch //(Exception)
            {
            }
        }
        private void ThreadRefreshTableStatus()
        {
            while (!MainModule.formMain.bExit)
            {
                ThreadRefreshTableStatusHandler();
                Thread.Sleep(10);
            }
        }
        #endregion

        #region Table Driver
        private void RefreshAxisCtrlView()
        {
            try
            {
                foreach (Control ctrl in groupBoxAxes.Controls)
                {
                    ctrl.Enabled = false;
                }
                foreach (Control ctrl in groupBoxMoveDist.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        ctrl.Visible = false;
                    }
                    if (ctrl is Label && !ctrl.Name.Equals("labelPointSelect") && !ctrl.Name.Equals("labelCurrTable"))
                    {
                        ctrl.Visible = false;
                    }
                }
                if (tableDriver == null)
                {
                    labelCurrTable.Text = "Please choose table first.";
                    labelCurrTable.ForeColor = Color.DarkRed;
                    return;
                }
                labelCurrTable.Text = tableDriver.strDriverName;
                labelCurrTable.ForeColor = Color.Blue;

                foreach (string strAxis in Enum.GetNames(typeof(DefaultAxis)))
                {
                    if (tableDriver.tableData.dicTableAxisItem.ContainsKey(strAxis))
                    {
                        if (tableDriver.tableData.dicTableAxisItem[strAxis].Active)
                        {
                            foreach (Control ctrl in groupBoxAxes.Controls)
                            {
                                if (ctrl.Text.Contains(strAxis))
                                {
                                    ctrl.Enabled = true;
                                }
                            }

                            if (strAxis == "X")
                            {
                                tbPointX.Visible = true;
                                labelAxisX.Visible = true;
                            }
                            if (strAxis == "Y")
                            {
                                tbPointY.Visible = true;
                                labelAxisY.Visible = true;
                            }
                            if (strAxis == "Z")
                            {
                                tbPointZ.Visible = true;
                                labelAxisZ.Visible = true;
                            }
                            if (strAxis == "U")
                            {
                                tbPointU.Visible = true;
                                labelAxisU.Visible = true;
                            }
                            if (strAxis == "A")
                            {
                                tbPointA.Visible = true;
                                labelAxisA.Visible = true;
                            }
                            if (strAxis == "B")
                            {
                                tbPointB.Visible = true;
                                labelAxisB.Visible = true;
                            }

                            if (strAxis == "C")
                            {
                                tbPointC.Visible = true;
                                labelAxisC.Visible = true;
                            }
                            if (strAxis == "D")
                            {
                                tbPointD.Visible = true;
                                labelAxisD.Visible = true;
                            }
                        }
                    }
                }

                cmbPointSel.Items.Clear();
                foreach (TablePosItem item in tableDriver.tableData.ListTablePosItems)
                {
                    cmbPointSel.Items.Add(item.Name);
                }

                btnJogSpdReduce.Enabled = true;
                btnJogSpdAdd.Enabled = true;
                btnAllAxisStop.Enabled = true;
            }
            catch //(Exception)
            {
            }
        }

        #region Axis Jog
        private double ConvertDoubleFromString(string strSrc)
        {
            try
            {
                double dValue = 0;
                string[] strArray = strSrc.Split(' ');
                if(strArray.Length >1)
                {
                    double.TryParse(strArray[0], out dValue);
                    return dValue;
                }
            }
            catch (Exception)
            {
            }
            return 0.00;
        }
        private void BtnAxisJog_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (rdBtnJog.Checked)
                {
                    //Jog Mode
                    if (null == cmbDist.SelectedItem)
                    {
                        MessageBox.Show("请先选择移动距离。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return;
                    }
                }
                Button btn = (Button)sender;
                string strBtnText = btn.Text;
                double dDist = ConvertDoubleFromString(cmbDist.SelectedItem.ToString());
                double dJogSpeed = 0.00;
                int iDir = -1;

                if(strBtnText.Contains("+"))
                {
                    iDir = 1;
                }
                if (_iJogSpdPresent > 100)
                {
                    _iJogSpdPresent = 10;
                }
                
                foreach (DefaultAxis axis in Enum.GetValues(typeof(DefaultAxis)))
                {
                    if (strBtnText.Contains(axis.ToString()))
                    {
                        dJogSpeed = tableDriver.tableData.dicTableAxisItem[axis.ToString()].JogSpeed * _iJogSpdPresent / 100.0 * iDir;
                        double dAcc = tableDriver.tableData.dicTableAxisItem[axis.ToString()].Acc;
                        double dDec = tableDriver.tableData.dicTableAxisItem[axis.ToString()].Dec;
                        tbCurrSpeed.Text = dJogSpeed.ToString("0.00") + " mm/s";

                        if(rdBtnContinue.Checked)
                        {
                            tableDriver.JobMove(axis, dAcc, dDec, dJogSpeed);
                        }
                        else
                        {
                            tableDriver.RelMove(axis, dAcc, dDec, dJogSpeed, dDist);
                        }
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void BtnAxisJog_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string strBtnText = btn.Text;
                foreach (DefaultAxis axis in Enum.GetValues(typeof(DefaultAxis)))
                {
                    if (strBtnText.Contains(axis.ToString()))
                    {
                        tableDriver.Stop(axis);
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void btnGetCurrPos_Click(object sender, EventArgs e)
        {
            try
            {
                tbPointA.Visible = false;
                tbPointB.Visible = false;
                tbPointC.Visible = false;
                tbPointD.Visible = false;
                tbPointX.Visible = false;
                tbPointY.Visible = false;
                tbPointZ.Visible = false;
                tbPointU.Visible = false;
                cmbPointSel.SelectedIndex = -1;
                foreach (string strAxis in Enum.GetNames(typeof(DefaultAxis)))
                {
                    if (tableDriver.tableData.dicTableAxisItem.ContainsKey(strAxis))
                    {
                        if (tableDriver.tableData.dicTableAxisItem[strAxis].Active)
                        {
                            if (strAxis == "X")
                            {
                                tbPointX.Visible = true;
                            }
                            if (strAxis == "Y")
                            {
                                tbPointY.Visible = true;
                            }
                            if (strAxis == "Z")
                            {
                                tbPointZ.Visible = true;
                            }
                            if (strAxis == "U")
                            {
                                tbPointU.Visible = true;
                            }
                            if (strAxis == "A")
                            {
                                tbPointA.Visible = true;
                            }
                            if (strAxis == "B")
                            {
                                tbPointB.Visible = true;
                            }

                            if (strAxis == "C")
                            {
                                tbPointC.Visible = true;
                            }
                            if (strAxis == "D")
                            {
                                tbPointD.Visible = true;
                            }
                        }
                    }
                }
            }
            catch //(Exception)
            {
            }
        }
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                tbPointA.Visible = false;
                tbPointB.Visible = false;
                tbPointC.Visible = false;
                tbPointD.Visible = false;
                tbPointX.Visible = false;
                tbPointY.Visible = false;
                tbPointZ.Visible = false;
                tbPointU.Visible = false;

                cmbPointSel.SelectedIndex = -1;
                foreach (string strAxis in Enum.GetNames(typeof(DefaultAxis)))
                {
                    if (tableDriver.tableData.dicTableAxisItem.ContainsKey(strAxis))
                    {
                        if (tableDriver.tableData.dicTableAxisItem[strAxis].Active)
                        {
                            if (strAxis == "X")
                            {
                                tbPointX.Visible = true;
                                tbPointX.Text = "0.000 mm";
                            }
                            if (strAxis == "Y")
                            {
                                tbPointY.Visible = true;
                                tbPointY.Text = "0.000 mm";
                            }
                            if (strAxis == "Z")
                            {
                                tbPointZ.Visible = true;
                                tbPointZ.Text = "0.000 mm";
                            }
                            if (strAxis == "U")
                            {
                                tbPointU.Visible = true;
                                tbPointU.Text = "0.000 mm";
                            }
                            if (strAxis == "A")
                            {
                                tbPointA.Visible = true;
                                tbPointA.Text = "0.000 mm";
                            }
                            if (strAxis == "B")
                            {
                                tbPointB.Visible = true;
                                tbPointB.Text = "0.000 mm";
                            }

                            if (strAxis == "C")
                            {
                                tbPointC.Visible = true;
                                tbPointC.Text = "0.000 mm";
                            }
                            if (strAxis == "D")
                            {
                                tbPointD.Visible = true;
                                tbPointD.Text = "0.000 mm";
                            }
                        }
                    }
                }
            }
            catch //(Exception)
            {
            }
        }
        #endregion

        #endregion

        #region Events
        private void dataGridViewTableSta_SizeChanged(object sender, EventArgs e)
        {
            dataGridViewTableSta.Columns[0].Width = dataGridViewTableSta.Width / 9 - 3;
            dataGridViewTableSta.Columns[1].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[2].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[3].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[4].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[5].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[6].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[7].Width = dataGridViewTableSta.Width / 9;
            dataGridViewTableSta.Columns[8].Width = dataGridViewTableSta.Width / 9;
        }
        private void rdBtnJog_CheckedChanged(object sender, EventArgs e)
        {
            cmbDist.Enabled = true;
        }
        private void rdBtnContinue_CheckedChanged(object sender, EventArgs e)
        {
            cmbDist.Enabled = false;
        }
        public void RefreshTableComboboxSelc()
        {
            cbTableSel.Items.Clear();
            foreach (TableData item in TableManage.docTable.listTableData)
            {
                cbTableSel.Items.Add(item.Name);
            }
            if (cbTableSel.Items.Count > 0)
            {
                cbTableSel.SelectedIndex = 0;
                groupBoxAxes.Enabled = true;
                groupBoxMoveDist.Enabled = true;
            }
            else
            {
                groupBoxAxes.Enabled = false;
                groupBoxMoveDist.Enabled = false;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTableComboboxSelc();
        }
        private void FormTableDriver_Load(object sender, EventArgs e)
        {
            InitTableStatusView();
            RefreshTableComboboxSelc();
            cmbDist.SelectedIndex = 0;

            Thread threadRefresh = new Thread(ThreadRefreshTableStatus);
            threadRefresh.IsBackground = true;
            threadRefresh.Start();
        }
        private void cbTableSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!TableManage.tableDrivers.dicDrivers.ContainsKey(cbTableSel.SelectedItem.ToString()))
                {
                    tableDriver = null;
                }
                else
                {
                    tableDriver = TableManage.tableDrivers.dicDrivers[cbTableSel.SelectedItem.ToString()];
                }

                if (null != tableDriver)
                {
                    foreach (string strAxis in Enum.GetNames(typeof(DefaultAxis)))
                    {
                        if (tableDriver.tableData.dicTableAxisItem.ContainsKey(strAxis))
                        {
                            if (strAxis == "X") dataGridViewTableSta.Columns[1].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "Y") dataGridViewTableSta.Columns[2].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "Z") dataGridViewTableSta.Columns[3].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "U") dataGridViewTableSta.Columns[4].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "A") dataGridViewTableSta.Columns[5].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "B") dataGridViewTableSta.Columns[6].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "C") dataGridViewTableSta.Columns[7].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                            if (strAxis == "D") dataGridViewTableSta.Columns[8].Visible = tableDriver.tableData.dicTableAxisItem[strAxis].Active;
                        }
                    }
                }

                RefreshAxisCtrlView();
                cmbPointSel.Items.Clear();
                foreach (TablePosItem posItem in tableDriver.tableData.ListTablePosItems)
                {
                    cmbPointSel.Items.Add(posItem.Name);
                }
            }
            catch (Exception)
            {
            }

        }
        private void cmbPointSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == cmbPointSel.SelectedItem)
                {
                    return;
                }
                if (null == cbTableSel.SelectedItem)
                {
                    return;
                }
                if (!tableDriver.tableData.dicTablePosItem.ContainsKey(cmbPointSel.SelectedItem.ToString()))
                {
                    return;
                }

                tbPointA.Visible = false;
                tbPointB.Visible = false;
                tbPointC.Visible = false;
                tbPointD.Visible = false;
                tbPointX.Visible = false;
                tbPointY.Visible = false;
                tbPointZ.Visible = false;
                tbPointU.Visible = false;

                string strPointName = cmbPointSel.SelectedItem.ToString();
                TableData table = tableDriver.tableData;
                TablePosItem tablePosItem = table.dicTablePosItem[strPointName];
                if (table.dicTableAxisItem.ContainsKey("X"))
                {
                    if (table.dicTableAxisItem["X"].Active && tablePosItem.ActiveX)
                    {
                        tbPointX.Visible = true;
                        tbPointX.Text = tablePosItem.PosX.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("Y"))
                {
                    if (table.dicTableAxisItem["Y"].Active && tablePosItem.ActiveY)
                    {
                        tbPointY.Visible = true;
                        tbPointY.Text = tablePosItem.PosY.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("Z"))
                {
                    if (table.dicTableAxisItem["Z"].Active && tablePosItem.ActiveZ)
                    {
                        tbPointZ.Visible = true;
                        tbPointZ.Text = tablePosItem.PosZ.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("U"))
                {
                    if (table.dicTableAxisItem["U"].Active && tablePosItem.ActiveU)
                    {
                        tbPointU.Visible = true;
                        tbPointU.Text = tablePosItem.PosU.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("A"))
                {
                    if (table.dicTableAxisItem["A"].Active && tablePosItem.ActiveA)
                    {
                        tbPointA.Visible = true;
                        tbPointA.Text = tablePosItem.PosA.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("B"))
                {
                    if (table.dicTableAxisItem["B"].Active && tablePosItem.ActiveB)
                    {
                        tbPointB.Visible = true;
                        tbPointB.Text = tablePosItem.PosB.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("C"))
                {
                    if (table.dicTableAxisItem["C"].Active && tablePosItem.ActiveC)
                    {
                        tbPointC.Visible = true;
                        tbPointC.Text = tablePosItem.PosC.ToString("0.000") + " mm";
                    }
                }
                if (table.dicTableAxisItem.ContainsKey("D"))
                {
                    if (table.dicTableAxisItem["D"].Active && tablePosItem.ActiveD)
                    {
                        tbPointD.Visible = true;
                        tbPointD.Text = tablePosItem.PosD.ToString("0.000") + " mm";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnJogSpdAdd_Click(object sender, EventArgs e)
        {
            if (_iJogSpdPresent <= 90)
            {
                _iJogSpdPresent += 10;
            }
            tbSpeedPrecent.Text = "JogSpeed * " + _iJogSpdPresent.ToString() + "%";
        }
        private void btnJogSpdReduce_Click(object sender, EventArgs e)
        {
            if (_iJogSpdPresent > 10)
            {
                _iJogSpdPresent -= 10;
            }
            tbSpeedPrecent.Text = "JogSpeed * " + _iJogSpdPresent.ToString() + "%";
        }
        private void tbPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private bool CheckLimit(string strAxis, double dValue)
        {
            if (!TableManage.docTable.dicTableData.ContainsKey(tableDriver.strDriverName))
                return false;
            if (!TableManage.docTable.dicTableData[tableDriver.strDriverName].dicTableAxisItem.ContainsKey(strAxis.ToString()))
                return false;

            double dPosLimit = TableManage.docTable.dicTableData[tableDriver.strDriverName].dicTableAxisItem[strAxis.ToString()].SoftLimitPos;
            double dNegLimit = TableManage.docTable.dicTableData[tableDriver.strDriverName].dicTableAxisItem[strAxis.ToString()].SoftLimitNeg;
            if (dValue < dPosLimit && dValue > dNegLimit)
                return true;
            return false;
        }
        private void tbPoint_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string strValue = textBox.Text;

            try
            {
                string strAxis = string.Empty;
                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("Input invalid !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    textBox.Text = "0.000 mm";
                    textBox.Focus();
                    return;
                }
                strValue = strValue.Replace('m', ' ');
                strValue = strValue.Replace('M', ' ');
                if (!double.TryParse(strValue, out double dValue))
                {
                    throw new Exception();
                }

                if (textBox.Name.Equals("tbPointX")) strAxis = "X";
                if (textBox.Name.Equals("tbPointY")) strAxis = "Y";
                if (textBox.Name.Equals("tbPointZ")) strAxis = "Z";
                if (textBox.Name.Equals("tbPointU")) strAxis = "U";
                if (textBox.Name.Equals("tbPointA")) strAxis = "A";
                if (textBox.Name.Equals("tbPointB")) strAxis = "B";
                if (textBox.Name.Equals("tbPointC")) strAxis = "C";
                if (textBox.Name.Equals("tbPointD")) strAxis = "D";
                if (string.IsNullOrEmpty(strAxis))
                {
                    throw new Exception();
                }

                if (!CheckLimit(strAxis, dValue))
                {
                    MessageBox.Show("Input outside the axis's software limit value !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    textBox.Text = "0.000 mm";
                    textBox.Focus();
                    return;
                }

                textBox.Text = dValue.ToString("0.000") + " mm";
            }
            catch (Exception)
            {
                MessageBox.Show("Input invalid !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                textBox.Text = "0.000 mm";
                textBox.Focus();
            }

        }

        public void EventTableDataReLoadHandler()
        {
            try
            {
                if(this.InvokeRequired)
                {
                    Action action = () =>
                    {
                        RefreshTableComboboxSelc();
                    };
                    this.Invoke(action);
                }
                else
                {
                    RefreshTableComboboxSelc();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void panelExternView_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelExternView.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Size = new Size(panelExternView.Width, panelExternView.Height);
                }
            }
        }

        private void btnMoveAbs_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tableDriver.tableData.dicTableAxisItem["X"].Active || !tableDriver.tableData.dicTableAxisItem["Y"].Active || 
                    !tableDriver.tableData.dicTableAxisItem["Z"].Active || !tableDriver.tableData.dicTableAxisItem["U"].Active)
                {
                    throw new Exception();
                }
                string strValueX = tbPointX.Text;
                string strValueY = tbPointY.Text;
                string strValueZ = tbPointZ.Text;
                string strValueU = tbPointU.Text;

                strValueX = strValueX.Replace('m', ' ');
                strValueX = strValueX.Replace('M', ' ');
                strValueY = strValueY.Replace('m', ' ');
                strValueY = strValueY.Replace('M', ' ');
                strValueZ = strValueZ.Replace('m', ' ');
                strValueZ = strValueZ.Replace('M', ' ');
                strValueU = strValueU.Replace('m', ' ');
                strValueU = strValueU.Replace('M', ' ');

                double dPosX = Convert.ToDouble(strValueX);
                double dPosY = Convert.ToDouble(strValueY);
                double dPosZ = Convert.ToDouble(strValueZ);
                double dPosU = Convert.ToDouble(strValueU);

                double dJogSpeed = tableDriver.tableData.dicTableAxisItem["X"].JogSpeed * _iJogSpdPresent / 100.0;

                if (tableDriver.AbsMoveXYZU(dPosX,dPosY,dPosZ,dPosU,dJogSpeed))
                {
                    MessageBox.Show("Start Move.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

        }
    }
}
