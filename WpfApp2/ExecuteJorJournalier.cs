using System;
using System.IO;
using Newtonsoft.Json;

namespace WpfApp2
{
    // Class that follows the Singleton pattern to handle JSON file creation
    public class JsonLogger
	{
		private static JsonLogger instance;
		private static readonly object lockObject = new object();

        // Method to get the instance of the Singleton
        public static JsonLogger Instance
		{
			get
			{
				lock (lockObject)
				{
					if (instance == null)
					{
						instance = new JsonLogger();
					}
					return instance;
				}
			}
		}

        // Method to perform logging in the JSON file
        public void Log(LogJournalier logJournalier, string FilePath)
		{
			string jsonString = JsonConvert.SerializeObject(logJournalier, Formatting.Indented) + "/n";

			lock (lockObject)
			{
				File.AppendAllText(FilePath, jsonString, System.Text.Encoding.UTF8);
			}
		}

        // Private constructor to prevent direct instantiation
        private JsonLogger() 
		{}
	}

}