using System;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string dataDirName = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirName, string dataFileName)
    {
        this.dataDirName = dataDirName;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirName, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load file : " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirName, dataFileName);
        try
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            }

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when save data to file : " + fullPath + "\n" + e);
        }
    }
}
