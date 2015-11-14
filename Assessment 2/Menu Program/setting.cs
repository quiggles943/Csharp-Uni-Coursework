﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Program
{
    public class Setting
    {
        private int fontsize;
        private string[,] setting = new string[100,2];
        string settingpath = @"..\..\setting.txt";
        public string settingfilepath;
        private int settinglength;

        public Setting()
        {
            settingfilepath = System.IO.Path.GetFullPath(settingpath);
            setting = Settingreadwrite;
        }
        public int Fontsize
        {
            get
            {
                setting = Settingreadwrite;
                fontsize = Int32.Parse(setting[2, 1]);
                return fontsize;
            }
            set
            {
                setting[2, 1] = value.ToString();
                Settingreadwrite = setting; 
                
            }
        }

        public string Password
        {
            get
            { 
                setting = Settingreadwrite;
                string pass = setting[1, 1];
                return pass; 
            }
            set
            {
                setting[1, 1] = value;
                Settingreadwrite = setting;
            }
        }

        public string[,] Settingreadwrite
        {
            get
            {
                int filelength = 0;
                using (StreamReader r = new StreamReader(settingfilepath))
                {
                    while (r.ReadLine() != null) { filelength++; }
                }
                int i = 1;
                string[] file = System.IO.File.ReadAllLines(settingfilepath);
                int len = file.Length;
                while (i < (len))
                {
                    string[] column = file[i].Split('\t');
                    int j = 0;
                    while (j < (column.Length))
                    {
                        string buffer = column[j];
                        string buffer2 = buffer;
                        setting[i, j] = buffer2;
                        j++;
                    }

                    i++;
                }
                settinglength = filelength;
                filelength = 0;
                return setting;
            }
            set
            {
                string[] empty = new string[0];
                File.WriteAllLines(settingfilepath, empty);
                StreamWriter logfile = File.AppendText(settingfilepath);
                logfile.WriteLine("hello\tVariable");
                for (int i = 1; i <= settinglength + 1; i++)
                {
                    logfile.WriteLine(setting[i, 0] + "\t" + setting[i, 1]);
                }
                logfile.Close();
            }
        }
    }
}
