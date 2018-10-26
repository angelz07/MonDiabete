using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.iOS.Class;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileReadWrite_IOS))]
namespace MonDiabete.iOS.Class
{
    public class IFileReadWrite_IOS : IFileReadWrite
    {
        public bool IsFileExiste(string filename)
        {
            Boolean retour = false;
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (File.Exists(filePath))
            {

                retour = true;
            }

            return retour;
        }

        public string ReadData(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return File.ReadAllText(filePath);
        }

        public void WriteData(string filename, string data)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, data);
        }
    }
}