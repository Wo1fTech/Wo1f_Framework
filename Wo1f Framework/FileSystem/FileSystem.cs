using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Wo1f_Framework
{
    public class FileSystem
    {
        Dictionary<string, object> Cache = new Dictionary<string, object>();


        private string dir = "";
        public string logfile = "WF.log";
        public FileSystem()
        {
            dir = Application.StartupPath;
        }
        public FileSystem(string Directory)
        {
            dir = Directory;
        }

        public static DirectoryInfo[] GetDirectories(string Location, bool Recurse)
        {
            List<DirectoryInfo> Ret = new List<DirectoryInfo>();
            DirectoryInfo DI = new DirectoryInfo(Location);
            if (!DI.Exists)
                throw new FileNotFoundException("File '" + Location + "' does not exist");

            if (Recurse)
            {
                foreach (string S in Directory.GetDirectories(Location, "*", SearchOption.AllDirectories))
                {
                    Ret.Add(new DirectoryInfo(S));
                }
                return Ret.ToArray();
            }
            else
            {
                foreach (string S in Directory.GetDirectories(Location, "*", SearchOption.TopDirectoryOnly))
                {
                    Ret.Add(new DirectoryInfo(S));
                }

            }


            return Ret.ToArray();
        
        }

        public static FileInfo[] GetFiles(string Location, bool Recurse)
        {
            List<FileInfo> Ret = new List<FileInfo>();
            if (Recurse)
            {
                List<DirectoryInfo> DIs = new List<DirectoryInfo>();
                string[] SS = Directory.GetFiles(Location, "*", SearchOption.AllDirectories);
                foreach (string S in SS)
                {
                    Ret.Add(new FileInfo(S));
                }
                return Ret.ToArray();
            }
            else
            {
                foreach (string S in Directory.GetFiles(Location))
                {
                    Ret.Add(new FileInfo(S));
                }
            }
            return Ret.ToArray();
        }


        public void Log(string Text)
        {
            Log(Text, false);
        }
        public void Log(string Text, bool AddStuff)
        {
            try
            {
                if (!File.Exists(logfile))
                    File.WriteAllText(logfile, "");

                string log = File.ReadAllText(logfile);
                if (AddStuff)
                    Text = CS.Timestamp() + Text;
                log = (log + CS.NL + Text);
                File.WriteAllText(logfile, log);
            }
            catch(IOException E) //log file in use, change the name
            {
                FileInfo FI = new FileInfo(logfile);
                logfile = FI.FullName.Replace(FI.Extension, "_.txt");
                
            }
        }
        public void Write(object Data, bool Async)
        {

        }
        public FileInfo[] GetFiles(string Directory)
        {
            DirectoryInfo DI = new DirectoryInfo(Directory);
            if (DI.Exists)
                return DI.GetFiles();
            return null;
        }

        public object ReadFile(string FileName)
        {

            //CS.CLog("ReadFile(" + FileName + ");");
            FileInfo FI = new FileInfo(FileName);
            if (!FI.Exists)
            {
                FI = new FileInfo("Content/" + FileName);
            }
            if (!FI.Exists)
            {
                Log("ReadFile returning - File does not exist.");
                return null;
            }

            object Ret = null;

            FileName = FI.FullName;
            DateTime Old = DateTime.Now;
            TimeSpan Span;
            if (Cache.Keys.Contains(FileName)) //If file has not been cached yet, it will be added into the cache.
            {
                Log("File in cache.  Loading.");

                Ret = Cache[FileName];
                Span = DateTime.Now.Subtract(Old);
            }
            else
            {
                long MaxMB = 2;
                MaxMB = MaxMB * 1024 * 1024;

                switch (FI.Extension.ToLower())
                {
                    case ".dds":
                        Ret = Image.FromFile(FileName);
                        break;
                    case ".gif":
                            Ret = Image.FromFile(FileName);
                        break;
                    case ".txt":
                        Ret = File.ReadAllText(FileName);
                        break;
                    case ".png":
                        Ret = Image.FromFile(FileName);
                        break;
                    case ".jpg":
                        Ret = Image.FromFile(FileName);
                        break;
                    case ".wo1f":
                        Ret = File.ReadAllText(FileName);
                        break;
                    default:
                        Ret = File.ReadAllBytes(FileName);
                        break;
                }

                if (FI.Length < MaxMB)
                {
                    Cache.Add(FileName, Ret);
                }
                else
                    Log("File not cached due to max memory requirement");
                Span = DateTime.Now.Subtract(Old);

            }

            if (Ret == null)
                Log("ERROR: Returning null.");
            long Len = 0;
            try
            {
                Len = FI.Length/1024;
            }
            catch
            { }
            if (Len == 0)
                Log("ReadFile returning after " + (int)Span.TotalMilliseconds + "ms");
            else
                Log("ReadFile returning file size: "+ Len + "kb after " + (int)Span.TotalMilliseconds + "ms");

            return Ret;
        }
    }
    public class CacheObject
    {
        private object data = null;
        public string Name { get; private set; }
        DateTime LastAccessed = DateTime.MinValue;
        private int size = 0;
        public int Size { get { return size; } }

        public CacheObject()
        {
            Init();
        }
        public CacheObject(string name, object Data)
        {
            Init();
            data = Data;
            Name = name;
        }

        private void Init()
        {
            throw new NotImplementedException();
        }

        public object Get()
        {
            LastAccessed = DateTime.Now;

            return data;
        }

        public object Get(Type type)
        {

            return Convert.ChangeType(Get(), type);
        }

        public Image GetImg()
        {
            return (Image)Get();
        }
        public long GetL()
        {
            return (long)Get();
        }
        public int GetI()
        {
            return (int)Get();
        }

        public override string ToString()
        {
            return Get().ToString();
        }


    }
}
