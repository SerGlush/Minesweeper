using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    public class RegistryTree
    {
        public RegistryNode BaseNode { set; get; }

        public RegistryTree(RegistryNode baseNode)
        {
            this.BaseNode = baseNode;
        }

        public RegistryNode FindNodeByPath(string path)
        {
            List<string> list = path.Split("\\").ToList();
            list.RemoveAt(0);
            if (list.Count != 0)
            {
                RegistryNode nextNode = BaseNode.childNodes.GetValueOrDefault(list[0]);
                list.RemoveAt(0);
                return FindNode(list, nextNode);
            }
            else return null;
        }

        private RegistryNode FindNode(List<string> list, RegistryNode Node)
        {
            if (list.Count != 0)
            {
                RegistryNode nextNode = Node.childNodes.GetValueOrDefault(list[0]);
                list.RemoveAt(0);
                return FindNode(list, nextNode);
            }
            else return Node;
        }
    }
}
