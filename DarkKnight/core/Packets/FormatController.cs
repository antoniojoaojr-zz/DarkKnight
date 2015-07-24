using DarkKnight.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkKnight.core.Packets
{
    class FormatController
    {
        private static bool defaultRegisted = false;
        private static Dictionary<string, int> formatLibrary = new Dictionary<string, int>();
        private static Dictionary<int, string> formatId = new Dictionary<int, string>();

        private static void register(string name, int value)
        {
            if (FormatReserver.reserved.Contains(value))
                throw new Exception("The format value {" + value + "} is invalid because is reserved for DarkKnight server");

            if (formatExists(name) || formatExists(value))
                throw new Exception("The format {" + name + "} or value {" + value + "} has been previously registered, we can not allow a copy");

            formatLibrary.Add(name, value);
            formatId.Add(value, name);
        }

        public static void registerDefault()
        {
            if (!defaultRegisted)
            {
                defaultRegisted = true;
                foreach (int value in Enum.GetValues((new DefaultFormat()).GetType()))
                {
                    string name = Enum.GetName((new DefaultFormat()).GetType(), value);
                    formatLibrary.Add(name, value);
                    formatId.Add(value, name);
                }
            }
        }

        /// <summary>
        /// Register a format from enum
        /// </summary>
        /// <param name="enumObject">Enum object to register</param>
        public static void registerEnumFormat(Enum enumObject)
        {
            registerDefault();

            foreach (int value in Enum.GetValues(enumObject.GetType()))
                register(Enum.GetName(enumObject.GetType(), value), value);
        }

        public static void registerDictionaryFormat(Dictionary<string, int> dicObject)
        {
            registerDefault();

            foreach (KeyValuePair<string, int> result in dicObject)
                register(result.Key, result.Value);
        }

        /// <summary>
        /// Checks a format exist in server
        /// </summary>
        /// <param name="format">string name of format</param>
        /// <returns>true if exists otherwise false</returns>
        public static bool formatExists(string format)
        {
            return formatLibrary.ContainsKey(format);
        }

        /// <summary>
        /// Checks a format exist in server
        /// </summary>
        /// <param name="format">int id of format</param>
        /// <returns>true if exists otherwise false</returns>
        public static bool formatExists(int format)
        {
            return formatId.ContainsKey(format);
        }

        /// <summary>
        /// Gets format name
        /// </summary>
        /// <param name="format">int format ID</param>
        /// <returns>string name of format</returns>
        public static string getFormatName(int format)
        {
            if (!formatId.ContainsKey(format))
                throw new Exception("The format id not found");

            return formatId[format];
        }

        /// <summary>
        /// Get format ID
        /// </summary>
        /// <param name="format">string format name</param>
        /// <returns>int ID of format</returns>
        public static int getFormatId(string format)
        {
            if (!formatLibrary.ContainsKey(format))
                throw new Exception("The format name not found");

            return formatLibrary[format];
        }
    }
}
