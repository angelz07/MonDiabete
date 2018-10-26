using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Droid.Class;
using Xamarin.Forms;

[assembly: Dependency(typeof(IFileReadWrite_Droid))]
namespace MonDiabete.Droid.Class
{
    public class IFileReadWrite_Droid : IFileReadWrite
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
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return File.ReadAllText(filePath);
        }

        public void WriteData(string filename, string data)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, data);
        }
    }
}