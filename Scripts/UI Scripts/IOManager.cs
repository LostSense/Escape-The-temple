using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class IOManager
{
    private string path;

    /// <summary>
    /// This method create IOManager and gives default path.
    /// </summary>
    public IOManager()
    {
        path = Application.persistentDataPath;
    }

    /// <summary>
    /// This method reads exist file
    /// </summary>
    /// <param name="fileName">add name of file</param>
    /// <returns>If file exist and readed - returns object. If not - returns null.</returns>
    public object ReadFile(string fileName)
    {
        string fullPath = GetFullPathToFile(fileName);
        bool fileExist = CheckIfFileExist(fullPath);
        if (fileExist)
        {
            return TryGetFileFromDevice(fullPath);
        }
        else
        {
            return null;
        }
    }

    private static object TryGetFileFromDevice(string fullPath)
    {
        try
        {
            FileStream dataStream = new FileStream(fullPath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            object data = converter.Deserialize(dataStream);
            dataStream.Close();
            return data;
        }
        catch
        {
            return null;
        }
    }

    private string GetFullPathToFile(string fileName)
    {
        return GetStringRow(path, "/", fileName);
    }

    /// <summary>
    /// This method safe file whose you cast. If file with same name exist - he will be deleted.
    /// </summary>
    /// <param name="fileName">Give a name for your file.</param>
    /// <param name="file">Cast an object for serialization.</param>
    public void SafeFile(string fileName, object file)
    {
        string fullPath = GetFullPathToFile(fileName);
        bool fileExist = CheckIfFileExist(fullPath);
        if (fileExist)
        {
            DeleteFile(fullPath);
        }
        SafeFileOnIODevice(file, fullPath);
    }

    private static void SafeFileOnIODevice(object file, string fullPath)
    {
        try
        {
            FileStream dataStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, file);
            dataStream.Close();
        }
        catch 
        {
        }
    }

    public string GetSystemPath()
    {
        return path;
    }

    private string GetStringRow(params string[] args)
    {
        string temp = "";
        for (int i = 0; i < args.Length; i++)
        {
            temp += args[i];
        }
        return temp;
    }

    private bool CheckIfFileExist(string path)
    {
        return File.Exists(path);
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }
}
