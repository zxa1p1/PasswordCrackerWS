using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PasswordCrackerWebservice
{
    /// <summary>
    /// Summary description for PasswordCrackerWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PasswordCrackerWS : System.Web.Services.WebService
    {
        private const string _sourceFileName = "webster-dictionary.txt";
        private const string destinationFileName = "File-Part-{0}.txt";
        private const int linesPerFile = 5000;



        [WebMethod(Description = "Divides dictionary into chunks and sends chunks to clients")]
        public void DoWork()
        {

            using (var sourceFile = new StreamReader(_sourceFileName))
            {

                var fileCounter = 0;
                var destinationFile = new StreamWriter(string.Format(destinationFileName, fileCounter + 1));

                try
                {
                    var lineCounter = 0;
                    string line;
                    while ((line = sourceFile.ReadLine()) != null)
                    {
                        if (lineCounter >= linesPerFile)
                        {
                            lineCounter = 0;
                            fileCounter++;
                            Console.WriteLine("Writing to file .. . {0}.", fileCounter);

                            destinationFile.Dispose();
                            destinationFile = new StreamWriter(string.Format(destinationFileName, fileCounter + 1));
                        }
                        destinationFile.WriteLine(line);
                        lineCounter++;

                    }
                }
                finally
                {
                    destinationFile.Dispose();
                }


            }
            
        }
    }
}
