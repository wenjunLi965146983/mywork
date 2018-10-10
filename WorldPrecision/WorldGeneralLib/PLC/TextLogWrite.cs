﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WorldGeneralLib.PLC
{
    static class TextLogWrite
    {
        static object objLock = new object();
        static public void AppendLog(string strMessage)
        {
            lock (objLock)
            {
                if (!Directory.Exists(@".//Logfile/"))
                {
                    Directory.CreateDirectory(@".//Logfile/");
                }
                string strFileName = Application.StartupPath + "//Logfile//" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.AppendAllText(strFileName, DateTime.Now.ToString("HH:mm:ss") + "     " + strMessage + "\r\n");

            }
        }
        static public void AppendLog(string strMessage, String strFilePath)
        {
            lock (objLock)
            {
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }
                string strFileName = strFilePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.AppendAllText(strFileName, DateTime.Now.ToString("HH:mm:ss") + "     " + strMessage + "\r\n");
            }
        }
        static public void AppendLog(string strMessage, String strFilePath, string strFileName)
        {
            lock (objLock)
            {
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }
                string strFullName = strFilePath + DateTime.Now.ToString("yyyy-MM-dd") + strFileName + ".txt";
                File.AppendAllText(strFullName, DateTime.Now.ToString("HH:mm:ss") + "     " + strMessage + "\r\n");
            }
        }
        static public void InitLog()
        {

            string strFileName = Application.StartupPath + "//Logfile//";
            if (Directory.Exists(strFileName))
            {

            }
            else
            {
                Directory.CreateDirectory(strFileName);
            }
        }
    }
}
