using System;
using System.Collections.Generic;
using System.Text;

namespace MonDiabete.Class.Dependency_Services_Class
{
    public interface IFileReadWrite
    {
        void WriteData(string filename, string data);
        string ReadData(string filename);
        Boolean IsFileExiste(string filename);
    }
}
