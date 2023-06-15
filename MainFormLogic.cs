using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Kurs
{
    public partial class MainForm
    {
        private RegistryTree registryTree;
        private int currentIndex = -1;

        public MainForm()
        {
            RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"AxControls.PMFActiveX");
            RegistrySecurity rs = new RegistrySecurity();
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            rs.AddAccessRule(
                new RegistryAccessRule(
                    new SecurityIdentifier(
                        WellKnownSidType.AccountDomainUsersSid, id.User.AccountDomainSid),
                    RegistryRights.FullControl, AccessControlType.Allow
                    ));
            key.SetAccessControl(rs);
            InitializeComponent();
            registryTree = new RegistryTree(new RegistryNode(null, treeView.Nodes.Add("Starting Node"), null));
        }

        private void FormLoad(object sender, EventArgs e)
        {
            dataGridView.AllowUserToAddRows = false;
            registryTree.BaseNode.childNodes.Add(
                Registry.ClassesRoot.Name, new RegistryNode(
                    registryTree.BaseNode, treeView.Nodes[0].Nodes.Add(Registry.ClassesRoot.Name), Registry.ClassesRoot));
            registryTree.BaseNode.childNodes.Add(
                Registry.CurrentUser.Name, new RegistryNode(
                    registryTree.BaseNode, treeView.Nodes[0].Nodes.Add(Registry.CurrentUser.Name), Registry.CurrentUser));
            registryTree.BaseNode.childNodes.Add(
                Registry.LocalMachine.Name, new RegistryNode(
                    registryTree.BaseNode, treeView.Nodes[0].Nodes.Add(Registry.LocalMachine.Name), Registry.LocalMachine));
            registryTree.BaseNode.childNodes.Add(
                Registry.Users.Name, new RegistryNode(
                    registryTree.BaseNode, treeView.Nodes[0].Nodes.Add(Registry.Users.Name), Registry.Users));
            registryTree.BaseNode.childNodes.Add(
                Registry.CurrentConfig.Name, new RegistryNode(
                    registryTree.BaseNode, treeView.Nodes[0].Nodes.Add(Registry.CurrentConfig.Name), Registry.CurrentConfig));
            treeView.Nodes[0].Expand();
        }

        private void treeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode nowTreeNode = e.Node;
            RegistryNode node = registryTree.FindNodeByPath(nowTreeNode.FullPath);
            if (node != null) node.OpenNode();
        }

        private void treeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            currentIndex = -1;
            dataGridView.Rows.Clear();
            TreeNode nowTreeNode = e.Node;
            RegistryNode node = registryTree.FindNodeByPath(nowTreeNode.FullPath);
            if (node != null && node.registryKey != null)
            {
                string[] values = node.registryKey.GetValueNames();

                foreach (string str in values)
                {
                    RegistryValueKind kind = node.registryKey.GetValueKind(str);
                    string value = getStringByType(kind, node.registryKey.GetValue(str));
                    dataGridView.Rows.Add(str, kind, value);
                }
            }
        }

        private string getStringByType(RegistryValueKind kind, object value)
        {
            switch (kind)
            {
                case RegistryValueKind.Binary:
                    StringBuilder result = new StringBuilder();
                    foreach (byte b in (byte[])value) result.Append(Convert.ToString(b, 16) + " ");
                    return result.ToString();
                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    try
                    {
                        string resultStr;
                        if (kind == RegistryValueKind.DWord) resultStr =
                                Convert.ToString((Int32)value, 16); else resultStr = Convert.ToString((Int64)value, 16);
                        return "0x" + resultStr + " (" + value + ")";
                    }
                    catch (Exception e) { return "Invalid value of Dword"; }
                default: return value.ToString();
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                DialogResult res = MessageBox.Show(
                    "Are you sure you want to delete this element?",
                    "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    RegistryNode node = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                    node.RemoveNode();
                }
            }
        }

        private void treeViewMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ((TreeView)sender).SelectedNode = ((TreeView)sender).GetNodeAt(e.X, e.Y);
                ((TreeView)sender).Focus();
            }
        }

        private void addSectionStripClick(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RegistryNode registryNode = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                InputNameWindow enterName = new InputNameWindow(registryNode);
                enterName.ShowDialog();
                if (enterName.DialogResult == DialogResult.OK && enterName.Name != null)
                {
                    registryNode.AddChildNode(enterName.Name);
                    treeView.SelectedNode.Expand();
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RegistryNode node = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                InputNameWindow enterName = new InputNameWindow(node.parentNode);
                enterName.ShowDialog();
                if (enterName.DialogResult == DialogResult.OK && enterName.Name != null)
                {
                    node.RenameChildNode(enterName.Name);

                }
            }
        }

        private void addParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RegistryNode node = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                if (node != null && node.registryKey != null)
                {
                    InputParameterWindow addFile = new InputParameterWindow(node.registryKey);
                    addFile.ShowDialog();
                    if (addFile.DialogResult == DialogResult.OK && addFile.Name != null && addFile.Value != null)
                    {
                        String name = addFile.Name;
                        node.registryKey.SetValue(name, addFile.Value);
                        RegistryValueKind kind = node.registryKey.GetValueKind(name);
                        String value = getStringByType(kind, node.registryKey.GetValue(name));
                        dataGridView.Rows.Add(name, kind, value);
                    }
                }
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RegistryNode node = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                if (node != null && node.registryKey != null && currentIndex != -1)
                {
                    DialogResult res = MessageBox.Show("Вы уверены что хотите удалить этот элемент?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        node.registryKey.DeleteValue(dataGridView.Rows[currentIndex].Cells[0].Value.ToString());
                        dataGridView.Rows.RemoveAt(currentIndex);
                    }
                }
                currentIndex = -1;
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            currentIndex = e.RowIndex;
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RegistryNode node = registryTree.FindNodeByPath(treeView.SelectedNode.FullPath);
                if (node != null && node.registryKey != null && currentIndex != -1)
                {
                    String NAME = dataGridView.Rows[currentIndex].Cells[0].Value.ToString();
                    InputParameterWindow addFile = new InputParameterWindow(node.registryKey, NAME);
                    addFile.ShowDialog();
                    if (addFile.DialogResult == DialogResult.OK && addFile.Name != null && addFile.Value != null)
                    {
                        String name = addFile.Name;
                        node.registryKey.DeleteValue(dataGridView.Rows[currentIndex].Cells[0].Value.ToString());
                        dataGridView.Rows.RemoveAt(currentIndex);
                        node.registryKey.SetValue(name, addFile.Value);
                        RegistryValueKind kind = node.registryKey.GetValueKind(name);
                        String value = getStringByType(kind, node.registryKey.GetValue(name));
                        dataGridView.Rows.Add(name, kind, value);
                    }
                }
                currentIndex = -1;
            }
        }
    }
}
