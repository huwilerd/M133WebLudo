using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für DataHelper
/// </summary>
public class DataHelper
{
    public DataHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void createFileAndDirectory(String filepath)
    {
        if (!File.Exists(filepath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            FileStream stream = File.Create(filepath);
            stream.Close();
        }
    }

    public static String getContentOfFile(String filepath)
    {
        StreamReader reader = new StreamReader(filepath);
        String contentOfFile = reader.ReadToEnd();
        reader.Close();
        return contentOfFile;
    }

    public static String createJsonFromObject(Object objc)
    {
        return JsonConvert.SerializeObject(objc);
    }

    public static void saveDataToFile(String path, String json, bool force)
    {
        if (force)
        {
            File.WriteAllText(path, json);
        } else
        {
            File.AppendAllText(path, json);
        }
        
    }
}