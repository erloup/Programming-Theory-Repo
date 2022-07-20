using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public struct PlayerTime
    {
        public string name;
        public float time;
    }
    public static MainManager Instance;
    public string name;
    public bool isGameOver = false;
    public List<PlayerTime> timesList;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public List<PlayerTime> timesList;
        public string lastname;
    }
    public void SaveInfo()
    {
        SaveData data = new SaveData();
        data.lastname = name;
        data.timesList = timesList;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.lastname;
            timesList = data.timesList;
            if (timesList == null) timesList = new List<PlayerTime>();
        }
        else timesList = new List<PlayerTime>();
    }
}
