using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    private string path;
    private SaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            path = Application.persistentDataPath + "/saveData.json";
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveTheData()
    {
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
            SaveTheData();
        }
    }

    public int GetLevelData()
    {
        return saveData.level;
    }
    public void SetLevelData(int newLevel)
    {
        saveData.level = newLevel;
        SaveTheData();
    }

    public void IncreaseLevel()
    {
        saveData.level++;
        SaveTheData();
    }
}

