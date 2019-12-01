using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharpFTP
{
    class Program
    {

        static string usage = "infile:C:\\Users\\User\\Downloads\\fileToUpload.txt ftp:ftp://user:password@url.net/upload/outFile.txt n:3";


        static void Main(string[] args)
        {

            try
            {

                var arguments = new Dictionary<string, string>();
                try
                {
                    foreach (var argument in args)
                    {
                        var idx = argument.IndexOf(':');
                        if (idx > 0)
                            arguments[argument.Substring(0, idx)] = argument.Substring(idx + 1);
                        else
                            arguments[argument] = string.Empty;
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("[-] Exception parsing arguments:");
                    Console.WriteLine(ex);
                }

                if (!arguments.ContainsKey("infile"))
                {
                    Console.WriteLine("[-] Error: No file to upload (infile) was given.");
                    Console.WriteLine("Usage:\r\n " + usage);
                    Environment.Exit(1);
                }

                if (!arguments.ContainsKey("ftp"))
                {
                    Console.WriteLine("[-] Error: No ftp was given.");
                    Console.WriteLine("Usage:\r\n" + usage);
                    Environment.Exit(1);
                }

                int n = 1;
                if (arguments.ContainsKey("n"))
                {
                    n = Int32.Parse(arguments["n"]);
                }

                /*
                if (!arguments.ContainsKey("user"))
                {
                    Console.WriteLine("[-] Error: No user was given.");
                    Console.WriteLine("Usage:\r\n" + usage);
                    Environment.Exit(1);
                }

                if (!arguments.ContainsKey("pass"))
                {
                    Console.WriteLine("[-] Error: No pass was given.");
                    Console.WriteLine("Usage:\r\n" + usage);
                    Environment.Exit(1);
                }*/

                for (; n > 0; n--)
                {
                    try
                    {
                        using (var client = new WebClient())
                        {
                            client.UploadFile(arguments["ftp"], WebRequestMethods.Ftp.UploadFile, arguments["infile"]);
                            Console.WriteLine("Upload successful");
                            client.Dispose();
                            n = 0; // if successful upload (no exception thrown) then task is completed and we can exit
                        }
                    } catch(Exception e)
                    {
                        Console.WriteLine(e);
                        continue;
                    }


                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }






        }
    }
}
