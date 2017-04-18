using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class NUnitFile : System.Attribute
    {
        public NUnitFile(string fileProjectRelativePath)
        {
            string filePath = fileProjectRelativePath.Replace("/", @"\");
            DirectoryInfo environmentDir = new DirectoryInfo(Environment.CurrentDirectory);
            Uri itemPathUri = new Uri(Path.Combine(environmentDir.Parent.Parent.FullName, filePath));
            string itemPath = itemPathUri.LocalPath;
            string binFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Uri itemPathInBinUri = new Uri(Path.Combine(binFolderPath, filePath));
            string itemPathInBin = itemPathInBinUri.LocalPath;
            if (File.Exists(itemPathInBin)) File.Delete(itemPathInBin);
            string itemDirectory = Path.GetDirectoryName(itemPathInBin);
            if (!Directory.Exists(itemDirectory)) Directory.CreateDirectory(itemDirectory);
            if (File.Exists(itemPath)) File.Copy(itemPath, itemPathInBin);
        }
    }
}
