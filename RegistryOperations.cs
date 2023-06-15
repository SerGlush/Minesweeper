using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs
{
    public class RegisterOperations
    {
        static public void RenameChildKey(RegistryKey parentKey, string childKeyName, string newChildKeyName)
        {
            CopyRegistryKey(parentKey, childKeyName, newChildKeyName);
            parentKey.DeleteSubKeyTree(childKeyName);
        }

        static public void CopyRegistryKey(RegistryKey parentKey, string oldKeyName, string newKeyName)
        {
            RegistryKey destinationKey = parentKey.CreateSubKey(newKeyName);
            RegistryKey sourceKey = parentKey.OpenSubKey(oldKeyName);
            ChildRegistryCopy(sourceKey, destinationKey);
        }

        static private void ChildRegistryCopy(RegistryKey sourceKey, RegistryKey destinationKey)
        {
            foreach (string name in sourceKey.GetValueNames())
            {
                object value = sourceKey.GetValue(name);
                RegistryValueKind valueKind = sourceKey.GetValueKind(name);
                destinationKey.SetValue(name, value, valueKind);
            }

            foreach (string sourceChildKeyName in sourceKey.GetSubKeyNames())
            {
                RegistryKey sourceChildKey = sourceKey.OpenSubKey(sourceChildKeyName);
                RegistryKey destinationChildKey = destinationKey.CreateSubKey(sourceChildKeyName);
                ChildRegistryCopy(sourceChildKey, destinationChildKey);
            }
        }
    }
}
