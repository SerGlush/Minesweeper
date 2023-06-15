using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs
{
    public class RegistryNode
    {
        public RegistryNode parentNode;
        public Dictionary<string, RegistryNode> childNodes;

        public RegistryKey registryKey;
        private TreeNode formsTreeNode;

        public RegistryNode(RegistryNode parentNode, TreeNode formsTreeNode, RegistryKey registryKey)
        {
            this.parentNode = parentNode;
            this.formsTreeNode = formsTreeNode;
            this.registryKey = registryKey;
            this.childNodes = new Dictionary<string, RegistryNode>();

            if (registryKey != null)
            {
                string[] childKeyNames = registryKey.GetSubKeyNames();
                if (childKeyNames != null && childKeyNames.Length != 0) formsTreeNode.Nodes.Add("Placeholder");
            }
        }

        public void OpenNode()
        {
            string[] childKeyNames = registryKey.GetSubKeyNames();
            if (childKeyNames != null && (childNodes.Count == 0 || childKeyNames.Length != formsTreeNode.Nodes.Count))
            {
                formsTreeNode.Nodes.Clear();
                childNodes.Clear();

                foreach (string keyName in childKeyNames)
                {
                    TreeNode currentTreeNode = formsTreeNode.Nodes.Add(keyName);
                    RegistryKey currentRegistryKey;
                    try
                    {
                        currentRegistryKey = registryKey.OpenSubKey(keyName, true);
                    }
                    catch (Exception e) { currentRegistryKey = null; }
                    childNodes.Add(keyName, new RegistryNode(this, currentTreeNode, currentRegistryKey));
                }
            }
        }
        public void AddChildNode(string name)
        {
            if (registryKey.GetSubKeyNames() != null && childNodes.Count == 0) OpenNode();
            TreeNode currentTreeNode = formsTreeNode.Nodes.Add(name);
            RegistryKey newRegistryKey = registryKey.CreateSubKey(name);
            childNodes.Add(name, new RegistryNode(this, currentTreeNode, newRegistryKey));
        }
        public void RemoveNode()
        {
            if (childNodes != null) foreach (RegistryNode registryNode in childNodes.Values) { registryNode.RemoveNode(); }
            if (parentNode != null)
            {
                parentNode.childNodes.Remove(formsTreeNode.Text);
                if (registryKey != null) parentNode.registryKey.DeleteSubKeyTree(formsTreeNode.Text);
            }
            formsTreeNode.Remove();
        }
        public void RenameChildNode(string name)
        {
            RegisterOperations.CopyRegistryKey(parentNode.registryKey, formsTreeNode.Text, name);
            RemoveNode();
            parentNode.OpenNode();

        }
    }
}
