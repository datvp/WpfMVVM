using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using WindowsInstaller;

namespace zSpaceWinApp.Processor
{
    public class InstallUnInstallProcessor
    {
        public IEnumerable<System.IO.FileInfo> queryInstallFiles()
        {
            Console.Out.WriteLine("test for queryInstallFile");
            // Modify this path as necessary.  
            string startFolder = @"D:\Softs\Setups\";

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            string searchTerm = @"Unikey";

            // Search the contents of each file.  
            // A regular expression created with the RegEx class  
            // could be used instead of the Contains method.  
            // queryMatchingFiles is an IEnumerable<string>.  
            var queryMatchingFiles =
                from file in fileList
                where file.Extension == ".htm"
                let fileText = GetFileText(file.FullName)
                where fileText.Contains(searchTerm)
                select file.FullName;

            // Execute the query.  
            Console.WriteLine("The term \"{0}\" was found in:", searchTerm);
            
            return fileList;
        }

        // Read the contents of the file.  
        static string GetFileText(string name)
        {
            string fileContents = String.Empty;

            // If the file has been deleted since we took   
            // the snapshot, ignore it and return the empty string.  
            if (System.IO.File.Exists(name))
            {
                fileContents = System.IO.File.ReadAllText(name);
            }
            return fileContents;
        }

        public void Install(string appPath)
        {
            PowerShell powerShell = null;
            Console.WriteLine(" ");

            //here “executableFilePath” need to use in place of “'D:\Softs\Snoop.msi'”
            //but I am using the path directly in the script.
            try {
                using (powerShell = PowerShell.Create())
                {
                    var path = String.Format(@"$setup=Start-Process '{0}' -ArgumentList '/qr' -Wait -PassThru", appPath);
                    powerShell.AddScript(path);

                    ICollection<PSObject> PSOutput = powerShell.Invoke();
                    foreach (PSObject outputItem in PSOutput)
                    {
                        if (outputItem != null)
                        {
                            Console.WriteLine(outputItem.BaseObject.GetType().FullName);
                            Console.WriteLine(outputItem.BaseObject.ToString() + "\n");
                        }
                    }

                    if (powerShell.Streams.Error.Count > 0)
                    {
                        string temp = powerShell.Streams.Error.First().ToString();
                        Console.WriteLine("Error: {0}", temp);
                    }
                    else
                        Console.WriteLine("Installation has completed successfully.");
                }
            } catch (Exception e)
            {
                Console.Out.WriteLine("Install app errer '{0}'", e.Message);
            } finally
            {
                if(powerShell != null)
                {
                    powerShell.Dispose();
                }
            }
        }

        public void UnInstall(string productCode)
        {
            // /qn: Set user interface level : None
            // /qb: Set user interface level : Basic UI
            // /qr: Set user interface level : Reduced UI
            // /qf: Set user interface level : Full UI (default)
            //Process p = new Process();
            //p.StartInfo.FileName = "msiexec.exe";
            //p.StartInfo.Arguments = $"/X{productCode} /qn";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardError = true;
            //p.StartInfo.CreateNoWindow = false;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.Start();
            //p.WaitForExit();

            //Type type = Type.GetTypeFromProgID("WindowsInstaller.Installer");
            //Installer installer = (Installer)Activator.CreateInstance(type);
            //installer.UILevel = MsiUILevel.msiUILevelNone;
            //installer.ConfigureProduct(productCode, 0, MsiInstallState.msiInstallStateAbsent);
        }
    }
}
