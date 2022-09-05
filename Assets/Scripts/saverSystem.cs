using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class saverSystem 
{
    
    public static void Save(GlobalControl data, options data2, dataStatic data3)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.th";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        saver save = new saver(data, data2, data3);

        formatter.Serialize(stream, save);
        Debug.Log(path);
        stream.Close();
    }

    public static saver loadSave()
    {
        string path = Application.persistentDataPath + "/save.th";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            

            saver save = formatter.Deserialize(stream) as saver;
            stream.Close();
            return save;
        }
        else
        {
            Debug.LogError("save file is not present" + path);
            return null;
        }
    }
}
