using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace DataAcessMock
{
    public class resources
    {
        public string GetFilePath(string origem)
        {
            switch (origem)
            {
                case "teste":
                    return Path.GetFullPath("../.. /../../../nebioFox/Content/FileOFX/OFX/");                    
                default:
                    return "";
            }
            //var teste2 = Path.GetFullPath("..");
            //var teste = 
            
        }
    }
}
