﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.SqlClient;

namespace Exam_Ref_70_483
{

    class C4_Listing1   // tried whatever I can, still cannot install DriveInfo class successfully
    {
        public static void Test_Listing_Drive_Info()
        {           
            //DriveInfo[] drivesInfo = DriveInfo.GetDrives();
        }
    }

    class C4_Listing2
    {
        public static void Test_Create_NewDirectory()
        {
            var directory = Directory.CreateDirectory(@"C:\Temp\ProgrammingInCSharp\Directory");  // use static class directory to create a new folder
            var directoryInfo = new DirectoryInfo(@"C:\Temp\ProgrammingInCSharp\DirectoryInfo");  // use directoryInfo class object to create a new folder
            directoryInfo.Create();
            Console.WriteLine("new folders have been created!");
            Console.ReadKey();
        }
    }

    class C4_Listing3
    {
        public static void Test_Delete_Existing_Directory()
        {
            if (Directory.Exists(@"C:\Temp\ProgrammingInCSharp\Directory"))
                Directory.Delete(@"C:\Temp\ProgrammingInCSharp\Directory");
            var directoryInfo = new DirectoryInfo(@"C:\Temp\ProgrammingInCSharp\DirectoryInfo");
            if (directoryInfo.Exists)
                directoryInfo.Delete();
            Console.WriteLine("Delete existing directory completed");
            Console.ReadKey();
        }
    }

    class C4_Listing4   // cannot install DirectorySecurity class
    {
        public static void Test_Set_Directory_Access_Control()
        {
            //DirectoryInfo directoryInfo = new DirectoryInfo("TestDirectory");
            //directoryInfo.Create();
            //DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
        }
    }

    class C4_Listing5
    {
        public static void Test_Build_directory_tree()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Program Files");
            ListDirectories(directoryInfo, "*a*", 5, 0);
            Console.ReadKey();
        }

        private static void ListDirectories(DirectoryInfo directoryInfo, string searchPattern, int maxLevel, int currentLevel)
        {
            if (currentLevel >= maxLevel)
                return;
            string indent = new string('-', currentLevel);

            try
            {
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories(searchPattern);   // return an array of directories

                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    Console.WriteLine(indent + subDirectory.Name);  // print current directory item with the indent to indicate current level
                    ListDirectories(subDirectory, searchPattern, maxLevel, currentLevel + 1);   // call ListDirectories recursively to traverse directory and build directory tree
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(indent + "Can't access:" + directoryInfo.Name);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(indent + "Can't find:" + directoryInfo.Name);
                return;
            }
        }
    }

    class C4_Listing6
    {
        public static void Test_Move_Directory()    // this method will throw errors, since we don't have those directories
        {
            Directory.Move(@"C:\Source", @"C:\destination");

            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Source");
            directoryInfo.MoveTo(@"C:\destination");
        }
    }

    class C4_Listing7
    {
        public static void Test_Listing_all_Files_in_Directory()
        {
            // here we wil only get files, not directory
            // the output filenames from those two methods are identical

            foreach (string file in Directory.GetFiles(@"C:\Windows"))
                Console.WriteLine(file);

            Console.WriteLine("-------------------------------");

            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Windows");
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                Console.WriteLine(fileInfo.FullName);

            Console.WriteLine("-------------------------------");

            Console.ReadKey();
        }
    }

    class C4_Listing8
    {
        public static void Test_Delete_File()
        {
            string path = @"c:\temp\test.txt";

            if (File.Exists(path))  // use File static class
                File.Delete(path);

            FileInfo fileInfo = new FileInfo(path); // use object of FileInfo class

            if (fileInfo.Exists)
                fileInfo.Delete();
        }
    }

    class C4_Listing9_10
    {
        public static void Test_Moving_a_File()
        {
            string path = @"c:\temp\test.txt";
            string destPath = @"c:\temp\destTest.txt";

            File.CreateText(path).Dispose();
            Console.WriteLine("file is in source folder now, press any key to continue");
            Console.ReadLine();
            File.Move(path, destPath);
            Console.WriteLine("file is moved to destination now, press any key to continue");
            Console.ReadLine();

            FileInfo fileInfo = new FileInfo(path); // this method will not create a new file first, you have to prep the file first
            fileInfo.MoveTo(destPath); // and if the dest file already exist, this method will throw an exception, samething for File.Move(0

        }

        public static void Test_Copy_a_File()
        {
            string path = @"c:\temp\test.txt";
            string destPath = @"c:\temp\destTest.txt";

            File.CreateText(path).Dispose();
            File.Copy(path, destPath);

            FileInfo fileInfo = new FileInfo(path);  // same as Test_Moving_a_File. you need to prep the file and remove duplicate file. Or it won't work
            fileInfo.CopyTo(destPath);
        }
    }

    class C4_Listing11_12
    {
        public static void Test_Manually_Concatenate_filePath()
        {
            string folder = @"C:\temp";
            string fileName = "test.dat";
            string fullPath = folder + fileName;
            Console.WriteLine("The path is:{0}", fullPath);
            Console.ReadLine();
        }

        public static void Test_Use_PathCombine_Method()
        {
            string folder = @"C:\temp";
            string fileName = "test.dat";

            string fullPath = Path.Combine(folder, fileName);
            Console.WriteLine("The path is:{0}", fullPath);
            Console.ReadLine();
        }
    }

    class C4_Listing13
    {
        public static void Test_other_Path_Method()
        {
            string path = @"C:\temp\test.txt";

            Console.WriteLine(Path.GetDirectoryName(path));
            Console.WriteLine(Path.GetExtension(path));
            Console.WriteLine(Path.GetFileName(path));
            Console.WriteLine(Path.GetPathRoot(path));
            Console.ReadLine();
        }
    }

    class C4_Listing14
    {
        public static void Test_use_FileStream()
        {
            string path = @"c:\temp\test.dat";

            using (FileStream fileStream = File.Create(path)) // FileStream class:Provides a Stream for a file, supporting both synchronous and asynchronous read and write operations.
            {
                string myValue = "MyValue2";
                byte[] data = Encoding.UTF8.GetBytes(myValue);  // convert the string to a specified encoding byte
                fileStream.Write(data, 0, data.Length);
            }
        }
    }

    class C4_Listing15
    {
        public static void Test_StreamWriter()
        {
            string path = @"c:\temp\test.dat";

            using (StreamWriter streamWriter = File.CreateText(path))  // CreateText method:Creates or opens a file for writing UTF-8 encoded text
            {
                string myValue = "MyValue";
                streamWriter.Write(myValue);    // convert the string to UFT8 and write to the streamWriter
            }
        }
    }

    class C4_Listing16
    {
        public static void Test_FileStream_Decoding()
        {
            string path = @"c:\temp\test.dat";
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] data = new byte[fileStream.Length];

                for (int index = 0; index < fileStream.Length; index++)
                {
                    data[index] = (byte)fileStream.ReadByte();
                }
                Console.WriteLine(Encoding.UTF8.GetString(data));
                //Console.WriteLine(data.ToString());  // this will not print the string, you have to indicate the encoding code
                Console.ReadLine();
            }
        }
    }

    class C4_Listing17
    {
        public static void Test_TextFile_reading_Content()
        {
            string path = @"c:\temp\test.dat";
            using (StreamReader streamWriter = File.OpenText(path)) // OpenText: Opens an existing UTF-8 encoded text file for reading.
            {                                                       // for text file, we use UTF-8 by default
                Console.WriteLine(streamWriter.ReadLine());
                Console.ReadLine();
            }
        }
    }

    class C4_Listing18
    {
        public static void Test_using_GZipStream_compressing_data()
        {
            string folder = @"c:\temp";
            string uncompressedFilePath = Path.Combine(folder, "uncompressed.dat");
            string compressedFilePath = Path.Combine(folder, "compressed.gz");
            byte[] dataToCompress = Enumerable.Repeat((byte)'a', 1024 * 1024).ToArray();    // prepare the byte for files
                                                                                            // Console.WriteLine(((byte)'a').ToString()) shows 97

            using (FileStream uncompressedFileStream = File.Create(uncompressedFilePath))   // create filestream for uncompressedFilePath
            {
                uncompressedFileStream.Write(dataToCompress, 0, dataToCompress.Length);     // write the byte array into filestream
            }

            using (FileStream compressedFileStream = File.Create(compressedFilePath))       // create filestream for compressedFilePath
            {
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))  // create GZipStream using compressedFilePath filestream
                {
                    compressionStream.Write(dataToCompress, 0, dataToCompress.Length);  // write data into GZipStream
                }
            }

            FileInfo uncompressedFile = new FileInfo(uncompressedFilePath);
            FileInfo compressedFile = new FileInfo(compressedFilePath);
            
            Console.WriteLine("uncompressedFile.Length:" + uncompressedFile.Length);
            Console.WriteLine("compressedFile.Length:" + compressedFile.Length);
            Console.ReadLine();
        }
    }

    class C4_Listing19
    {
        public static void Test_Buffer_Stream()
        {
            string path = @"c:\temp\bufferedStream.txt";

            using (FileStream fileStream = File.Create(path))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))  // BufferedStream wraps fileStream
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        streamWriter.WriteLine("A line of text.");
                    }
                }
            }
        }
    }

    class C4_Listing20_21
    {
        public static string Test_Check_FileExist()
        {
            string path = @"C:\temp\test.txt";

            if (File.Exists(path))
                return File.ReadAllText(path);
            return string.Empty;
        }

        public static string Test_use_Exception_When_Opening_File()
        {
            string path = @"C:\temp\test.txt";
            try
            {
                return File.ReadAllText(path);
            }
            catch (DirectoryNotFoundException) { }
            catch (FileNotFoundException) { }

            return string.Empty;
        }
    }

    class C4_Listing22
    {
        public static void Test_executing_WebRequest_and_WebResponse()
        {
        //    WebRequest request = WebRequest.Create("http://www.microsoft.com");
        //    WebResponse response = request.GetResponse();  // cannot use GetResponse method. Try to use GetResponseAsync method and task to implement later

        //    StreamReader responseStream = new StreamReader(response.GetResponseStream());
        //    string responseText = responseStream.ReadToEnd();

        //    Console.WriteLine(responseText);
        //    Console.ReadKey();

        //    response.Close();
        }
    }

    class C4_Listing23
    {
        public static async Task CreateAndWriteAsyncToFile()
        {
            using (FileStream stream = new FileStream(@"C:\Temp\test.dat", FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                byte[] data = new byte[100000];
                new Random().NextBytes(data);

                await stream.WriteAsync(data, 0, data.Length);  // write data into the filestream async
            }
        }

        public static void Test_WriteAsync()
        {
            Console.WriteLine("The returned task status is:{0}",CreateAndWriteAsyncToFile().Status.ToString());
            Console.ReadLine();
        }

    }

    class C4_Listing24
    {

        public static async Task ReadAsyncHttpRequest()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://www.microsoft.com");
            Console.WriteLine("the result is:{0}", result);
            Console.ReadLine();
        }


        public static async void Test_GetStringAsync()
        {
            await ReadAsyncHttpRequest();
            
        }
    }

    class C4_Listing25
    {
        public static async Task ExecuteMultipleRequests()
        {
            HttpClient client = new HttpClient();

            string microsoft = await client.GetStringAsync("http://www.microsoft.com");
            string msdn = await client.GetStringAsync("http://msdn.microsoft.com");
            string blogs = await client.GetStringAsync("http://blogs.msdn.com");
            Console.WriteLine("microsoft:{0}", microsoft);
            Console.ReadLine();
        }
    }
    
    class C4_Listing26
    {
        public async Task ExecuteMultipleRequestsInParallel()
        {
            HttpClient client = new HttpClient();

            Task microsoft = client.GetStringAsync("http://www.microsoft.com");
            Task msdn = client.GetStringAsync("http://msdn.microsoft.com");
            Task blogs = client.GetStringAsync("http://blogs.msdn.com");

            await Task.WhenAll(microsoft, msdn, blogs);
        }
    }

    class C4_Listing27
    {
        public static void Test_SqlConnection()
        {
            using (SqlConnection connection = new SqlConnection("Data Source = HUICHEN;Integrated Security=true;Initial Catalog=Hui"))
            {

            }
        }
    }

    class C4_Listing28
    {

    }
}
