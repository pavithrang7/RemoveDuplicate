using System;
using System.IO;
using System.Security.Cryptography;

namespace RemoveDupilicate
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {     
                String InputPath = args[0];
                DirectoryInfo d = new DirectoryInfo(InputPath);
                FileInfo[] Files = d.GetFiles("*.*");

                    foreach (FileInfo filename in Files)
                    {
                        if (File.Exists(filename.FullName))
                        {
                            Byte[] shaHash;
                            string sourcefilename = filename.FullName;
                            using (var shaForStream = new SHA1Managed())
                            using (Stream sourceFileStream = File.Open(sourcefilename, FileMode.Open))
                            using (Stream sourceStream = new CryptoStream(sourceFileStream, shaForStream, CryptoStreamMode.Read))

                            {
                                while (sourceStream.ReadByte() != -1) ;
                                shaHash = shaForStream.Hash;
                                string shavalue = Convert.ToBase64String(shaHash);
                                sourceFileStream.Close();
                                DirectoryInfo d1 = new DirectoryInfo(InputPath);
                                FileInfo[] Files1 = d1.GetFiles("*.*");
                                foreach (FileInfo filename1 in Files1)
                                {
                                    Byte[] shaHash1;
                                    string sourcefilename1 = filename1.FullName;
                                    using (var shaForStream1 = new SHA1Managed())
                                    using (Stream sourceFileStream1 = File.Open(sourcefilename1, FileMode.Open))
                                    using (Stream sourceStream1 = new CryptoStream(sourceFileStream1, shaForStream1, CryptoStreamMode.Read))
                                    {
                                        while (sourceStream1.ReadByte() != -1) ;
                                        shaHash1 = shaForStream1.Hash;
                                        string shavalue1 = Convert.ToBase64String(shaHash1);
                                        sourceFileStream1.Close();
                                        if (shavalue == shavalue1 & filename.FullName != filename1.FullName)
                                        {
                                            Console.WriteLine("Found Duplicate : Removing " + filename1.FullName);
                                            File.Delete(filename1.FullName);
                                        }
                                    }
                                }
                            }
                        }
                    }
            }
            else
            {
                const string Value = "Pass the Folder Location to remove dupilcate files in double quotes";
                Console.WriteLine(Value);
            }
        }
    }
}