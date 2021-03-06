﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Checksums;

namespace DoSomeThing.Utility
{
    public static class Tools
    {
        private static readonly Object logLock = new Object();

        private static readonly Object logErrorLock = new Object();


        /// <summary>
        /// 替换Sql语句中单引号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string modifysql(this string s)
        {
            return s.Replace("'", "''");
        }


        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="Message"></param>
        public static void LogError(string Message)
        {
            try
            {
                lock (logErrorLock)
                {
                    string logErrorPath = System.IO.Directory.GetCurrentDirectory() + "\\Log\\" + DateTime.Now.ToString("yyyyMMdd") + "_Error.txt";

                    FileInfo fileInfo = new FileInfo(logErrorPath);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    using (System.IO.StreamWriter streamWriter = new StreamWriter(logErrorPath, true))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + Message + "\r\n" + "--------------------------------------------------\r\n";
                        streamWriter.Write(msg);
                    }
                }
            }
            catch { }
        }
      
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="Message"></param>
        public static void Log(string Message, string logFileName)
        {
            try
            {
                lock (logFileName)
                {
                    string logPath = System.IO.Directory.GetCurrentDirectory() + "\\Log\\" + logFileName;
                    FileInfo fileInfo = new FileInfo(logPath);
                    if (!fileInfo.Directory.Exists)
                    {
                        fileInfo.Directory.Create();
                    }
                    using (System.IO.StreamWriter streamWriter = new StreamWriter(logPath, true))
                    {
                        string msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + Message + "\r\n" + "--------------------------------------------------\r\n";
                        streamWriter.Write(msg);
                    }
                }
            }
            catch { }
        }



        /// <summary>
        /// 文件压缩
        /// </summary>
        /// <param name="strFile">待压缩文件目录</param>
        /// <param name="strZip">压缩后的目标文件</param>
        public static void ZipFile(string strFile, string strZip)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                strFile += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6); 
            zip(strFile, s, strFile);
            s.Finish();
            s.Close();
        }

        /// <summary>
        /// 文件压缩
        /// </summary>
        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {

                if (Directory.Exists(file))
                {
                    zip(file, s, staticFile);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="TargetFile">待解压的文件</param>
        /// <param name="fileDir">解压后放置的目标目录</param>
        /// <returns></returns>
        public static string unZipFile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();

                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }  

    }
}
