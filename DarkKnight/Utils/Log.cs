using System;

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
    }
}
