using System.IO;
using UnityEngine;

[System.Serializable]
public struct GameData
{
    public TabManager.Data TabManagerData;
}

[DefaultExecutionOrder(order: -1)]
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    [SerializeField] private GameData _defaultGameData; //TODO: Add default values
    [SerializeField] private string _fileName = "data.dat";

    private static string _filePath;

    private void Awake()
    {
        Instance = this;

        _filePath = $"{Application.persistentDataPath}/{_fileName}";
    }

    private void Start()
    {
        Debug.Log($"Start {gameObject.name}");

        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }

    private void Clean()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);

            Debug.Log("Cleaned Saved Data Completed!");
        }
        else
        {
            Debug.Log("Can Not Find Saved Data!");
        }
    }

    private void Load()
    {
        var gameData = _defaultGameData;

        if (!File.Exists(_filePath))
        {
            // Set data
            SetData(gameData);

            Debug.Log($"Default Game Data Loaded Successfully!");
            return;
        }

        var json = File.ReadAllText(_filePath);

        try
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        catch
        {
            Debug.Log("[GAME LOAD] Unsuccessful. Load default data.");
        }

        // Set data
        SetData(gameData);
        Debug.Log($"[GAME LOAD] Successful! File Path: {_filePath}");
    }

    private void Save()
    {
        // Get Data
        var gameData = GetData();

        var json = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(_filePath, json);

        Debug.Log($"[GAME SAVE] Successful! File Path: {_filePath}");
    }

    private GameData GetData()
    {
        var gameData = new GameData();
        gameData.TabManagerData = TabManager.Instance.GetData();

        return gameData;
    }

    private void SetData(GameData gameData)
    {
        TabManager.Instance.SetData(gameData.TabManagerData);
    }
}