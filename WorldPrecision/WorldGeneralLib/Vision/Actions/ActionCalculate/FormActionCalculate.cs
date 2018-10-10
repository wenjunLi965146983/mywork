using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Vision.Forms;

namespace WorldGeneralLib.Vision.Actions.Calculate
{
    public partial class FormActionCalculate : Form
    {

        private ActionCalculateData _actionCalculateData;
        private ActionCalculate _actionCalculate; 
        public FormActionCalculate(ActionCalculateData data, ActionCalculate actionCalculate)
        {
            InitializeComponent();
            _actionCalculate = actionCalculate;
            _actionCalculateData = data;
            FormVision.eventRun += new FormVision.formRefresh(init);

        }

        private void FormActionCalculate_Load(object sender, EventArgs e)
        {
            richTextBox1.Text= _actionCalculateData.strExpression ;
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex(@"^[0-9]*[+,\-,\*,\/,&,|,=,>,<,(,),.,;]*$");
            if (!rg.IsMatch(e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }
        public void init()
        {
            List<ActionBase> list = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction;
            int i = 0;
            ToolStripMenuItem topLvevel = new ToolStripMenuItem("添加变量");

            foreach (ActionBase action in list)
            {
                i++;
                Type T = action.GetType();
                PropertyInfo[] properList = T.GetProperties();



                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(i.ToString()+action.actionData.Name);
                int j = 0;
                foreach (var proper in properList)
                {
                    if(proper.Name[0]=='b'| proper.Name[0] == 'd')
                    {
                        toolStripMenuItem.DropDown.Items.Add(proper.Name);
                        toolStripMenuItem.DropDown.Items[j].Name = i.ToString() + "~" + action.actionData.Name + "~" + proper.Name;
                        j++;
                    }
                    
                }
                
                topLvevel.DropDown.Items.Add(toolStripMenuItem);
                toolStripMenuItem.DropDownItemClicked += (AddVar);


            }
            toolDataSrc.Items.Add(topLvevel); 

        }
        private void AddVar(object sender, ToolStripItemClickedEventArgs e)
        {
            String var = e.ClickedItem.Name;

            richTextBox1.Text += ("#" + var); 
        }
      
        

        private void button1_Click(object sender, EventArgs e)
        {
            _actionCalculateData.strExpression= richTextBox1.Text;
            String item=String.Empty;
            String tmpExp = String.Empty;
            label1.Text = String.Empty;

            bool IsVar = false;
            String strText = richTextBox1.Text;
            foreach (char ch in strText)
            {
                Regex regex = new Regex(@"[~]{1}");
                if (IsVar&&regex.IsMatch(item))
                {
                    Regex rg = new Regex(@"^[0-9]*[+,\-,\*,\/,&,|,=,>,<,(,),.,;]*$");
                    if (rg.IsMatch(ch.ToString()))
                    {
                        IsVar = false;
                        String strValue= _actionCalculate.GetValue(item);
                        tmpExp += strValue;


                        label1.Text += item+":"+ strValue+"\r\n";
                        item = String.Empty;
                    }
                }
                if (ch != '#'&&!IsVar)
                {
                    if (ch != ';')
                    {
                        tmpExp += ch.ToString();
                    } 
                }
                else
                {
                    IsVar = true;
                    if(ch!= '#')
                    {
                        item += ch.ToString();
                    }
                }   
            }
            if (tmpExp == String.Empty)
            {
                return;
            }
            else
            {
                _actionCalculate.ActionExcute();
                label1.Text += "Result:"+Convert.ToString(_actionCalculate.doubleValue)+ "\r\n";
                
            }
         

        }




    }
   
    }

