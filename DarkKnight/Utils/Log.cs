using System;
using System.IO;

#region License Information
/* ************************************************************
 * 
 *    @author AntonioJr <antonio@emplehstudios.com.br>
 *    @copyright 2015 Empleh Studios, Inc
 * 
 * 	  Project Folder: https://github.com/antoniojoaojr/DarkKnight
 * 
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion

namespace DarkKnight.Utils
{
    public enum LogLevel
    {
        WARNING,
        ERROR,
        TITLE,
        TEXT
    }
    public class Log
    {
        private static bool started = false;

        public static void Write(string msg)
        {
            Write(msg, LogLevel.TEXT);
        }

        public static void Write(string msg, LogLevel level)
        {
            if (!started)
            {
                started = true;
                Write("Iniciado Logs");
            }

            string log = "";

            switch (level)
            {
                case LogLevel.TITLE:
                    log += "== [ " + DateTime.Now + " ] ";
                    log = completeLine(log, '=');
                    log += "== [ " + msg + " ] ";
                    log = completeLine(log, '=');
                    break;
                case LogLevel.TEXT:
                    log += "- [ " + DateTime.Now + " ] - " + msg;
                    break;
                case LogLevel.ERROR:
                    log += "-- ERROR ";
                    log = completeLine(log, '-');
                    log += "- [ " + DateTime.Now + " ] - " + msg;
                    log = completeLine(log, ' ');
                    log += "-";
                    log = completeLine(log, '-');
                    break;
                case LogLevel.WARNING:
                    log += "-- WARNING ";
                    log = completeLine(log, '-');
                    log += "- [ " + DateTime.Now + " ] - " + msg;
                    log = completeLine(log, ' ');
                    log += "-";
                    log = completeLine(log, '-');
                    break;
            }

            writeLog(log, level);
            Console.WriteLine(log);
        }

        private static string completeLine(string log, char complete)
        {
            int length = 80 - log.Length;
            while (length < 0)
                length += 80;

            for (int i = 0; i < length; i++)
                log += complete;

            return log;
        }

        private static void writeLog(string log, LogLevel level)
        {
            string file = "logs\\log-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            try
            {
                writeFile(file + ".log", log);

                if (level != LogLevel.TITLE)
                {
                    string type = Enum.GetName(typeof(LogLevel), level);
                    writeFile(file + "-" + type + ".log", log);
                }
            }
            catch
            {

            }
        }

        private static void writeFile(string file, string log)
        {

            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");

            //string file = "logs\\log-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".log";

            if (!File.Exists(file))
            {
                using (StreamWriter sw = File.CreateText(file))
                {
                    writeChar(log, sw);
                }
                return;
            }
            using (StreamWriter sw = File.AppendText(file))
            {
                writeChar(log, sw);
            }
        }

        private static void writeChar(string log, StreamWriter sw)
        {
            for (int i = 0; i < log.Length; i++)
            {
                if (i % 80 == 0)
                    sw.WriteLine();
                sw.Write(log[i]);
            }
        }
    }
}
