using System;
using System.IO;
using Newtonsoft.Json;

class Program
{
	static void Main()
	{
		// Obtener la instancia del JsonLogger (Singleton)
		JsonLogger jsonLogger = JsonLogger.Instance;

		// Crear un objeto LogJournalier
		LogJournalier logJournalier = new LogJournalier
		{
			IdLogJourn = "123",
			NomLj = "Registro Diario",
			FileSource = "C:\\Archivos\\Origen.txt",
			FileTarget = "C:\\Archivos\\Destino.txt",
			FileSize = "1024", 
			FileTransferTime = "5 segundos",
			Time = "2023-12-19 12:00:00", 
			TimeCrypt = "2023-12-19 12:05:00"
		};

		// Registrar el objeto en el archivo JSON
		jsonLogger.Log(logJournalier);

		Console.WriteLine("Datos guardados en el archivo JSON.");
	}
}

// Clase que sigue el patrón Singleton para manejar la creación del archivo JSON
public sealed class JsonLogger
{
	private static JsonLogger instance;
	private static readonly object lockObject = new object();
	private readonly string rutaArchivoJson = "C:\\LOGJ\\2020-11-30.json";

	// Propiedad para obtener la instancia del Singleton
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

	// Método para realizar el registro en el archivo JSON
	public void Log(LogJournalier logJournalier)
	{
		string jsonString = JsonConvert.SerializeObject(logJournalier, Formatting.Indented);

		lock (lockObject)
		{
			File.AppendAllText(rutaArchivoJson, jsonString, System.Text.Encoding.UTF8);
		}
	}

	// Constructor privado para evitar instanciación directa
	private JsonLogger() { }
}

// Clase LogJournalier para representar los datos
public class LogJournalier
{
	public string IdLogJourn { get; set; }
	public string NomLj { get; set; }
	public string FileSource { get; set; }
	public string FileTarget { get; set; }
	public string FileSize { get; set; }
	public string FileTransferTime { get; set; }
	public string Time { get; set; }
	public string TimeCrypt { get; set; }
}
 