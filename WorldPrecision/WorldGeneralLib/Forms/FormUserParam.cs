using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Data;
using WorldGeneralLib.Table;
using WorldGeneralLib.Functions;
using WorldGeneralLib.Forms.ToolForms;
namespace WorldGeneralLib.Forms
{
    public partial class FormUserParam : Form
    {
        public FormBase formDataUserView;
        public FormBase formTableUserView;

        public FormUserParam()
        {
            InitializeComponent();
        }
        #region Load
        private void Init()
        {
            TreeViewDataInit();
            TableTreeViewInit();

            if (null == DataManage.formDataUserView)
            {
                DataManage.formDataUserView = new FormDataUserView();
            }
            DataManage.formDataUserView.TopLevel = false;
            DataManage.formDataUserView.Dock = DockStyle.Fill;
            formDataUserView.panelMain.Controls.Add(DataManage.formDataUserView);
            DataManage.formDataUserView.Show();

            if (null == TableManage.formTableUserView)
            {
                TableManage.formTableUserView = new FormTableUserView();
            }
            TableManage.formTableUserView.TopLevel = false;
            TableManage.formTableUserView.Dock = DockStyle.Fill;
            formTableUserView.panelMain.Controls.Add(TableManage.formTableUserView);
            TableManage.formTableUserView.Show();
        }
        private void FormUserParam_Load(object sender, EventArgs e)
        {
            formDataUserView = new FormBase();
            formDataUserView.TopLevel = false;
            formDataUserView.Dock = DockStyle.Fill;
            panelMain.Controls.Add(formDataUserView);

            formTableUserView = new FormBase();
            formTableUserView.TopLevel = false;
            formTableUserView.Dock = DockStyle.Fill;
            panelMain.Controls.Add(formTableUserView);

            formDataUserView.Show();
            Init();
        }

        #endregion

        #region ContexMenuStrip
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewDataGroup.SelectedNode != null && treeViewDataGroup.SelectedNode.Level == 0)
            {
                treeViewDataGroup.Nodes[0].Nodes.Clear();
                TreeViewDataInit();
            }
            if (treeViewTable.SelectedNode != null && treeViewTable.SelectedNode.Level == 0)
            {
                treeViewTable.Nodes[0].Nodes.Clear();
                TableTreeViewInit();
            }
        }
        #endregion

        #region Data
        private bool TreeViewDataInit()
        {
            try
            {
                foreach (DataGroup item in DataManage.docData.listDataGroup)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.strGroupName;
                    newNode.Name = item.strGroupName;

                    if (!treeViewDataGroup.Nodes["DataGroup"].Nodes.ContainsKey(newNode.Name))
                    {
                        treeViewDataGroup.Nodes["DataGroup"].Nodes.Add(newNode);
                    }
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
        private void treeViewDataGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (null != treeViewDataGroup.SelectedNode && treeViewDataGroup.SelectedNode.Level == 1)
                {
                    DataManage.formDataUserView.ShowDataItems(treeViewDataGroup.SelectedNode.Name);
                }
            }
            catch
            {
            }
        }
        private void xPanderPanelParameter_ExpandClick(object sender, EventArgs e)
        {
            formDataUserView.Hide();

            treeViewDataGroup.ExpandAll();
            formDataUserView.Show();
        }
        private void btnDataGroupRefresh_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Table
        private void TableTreeViewInit()
        {
            try
            {
                treeViewTable.Nodes[0].Nodes.Clear();
                foreach (TableData data in TableManage.docTable.listTableData)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = data.Name;
                    newNode.Name = data.Name;
                    treeViewTable.Nodes[0].Nodes.Add(newNode);
                }
                if (treeViewTable.Nodes[0].Nodes.Count > 0)
                {
                    treeViewTable.SelectedNode = treeViewTable.Nodes[0].Nodes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void treeViewTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewTable.SelectedNode == null)
                return;
            if (1 != treeViewTable.SelectedNode.Level)
                return;
            try
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(treeViewTable.SelectedNode.Name))
                    return;
                TableManage.formTableUserView.ShowTable(treeViewTable.SelectedNode.Name);
            }
            catch (Exception)
            {
            }
        }
        private void xPanderPanelTable_ExpandClick(object sender, EventArgs e)
        {
            treeViewDataGroup.SelectedNode = null;
            formDataUserView.Hide();

            formTableUserView.Show();
            treeViewTable.ExpandAll();
            if (treeViewTable.SelectedNode == null && treeViewTable.Nodes[0].Nodes.Count > 0)
            {
                treeViewTable.SelectedNode = treeViewTable.Nodes[0].Nodes[0];
            }
        }

        public void EventTableDataReLoadHandler()
        {
            try
            {
                TableTreeViewInit();
            }
            catch (Exception)
            {
            }

        }
        #endregion
    }
}
