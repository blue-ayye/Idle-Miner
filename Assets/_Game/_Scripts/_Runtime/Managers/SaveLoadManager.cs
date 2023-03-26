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
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
        if (Input.GetKeyDown(KeyCode.C)) Clean();
    }

    private void Clean()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);

            Debug.Log($"<color=#FFAD5A>[SAVE DATA CLEAR]</color> Removed save file!");
        }
        else
        {
            Debug.Log($"<color=#4F9DA6>Can Not Find Saved Data!</color>");
        }
    }

    private void Load()
    {
        var gameData = _defaultGameData;

        if (!File.Exists(_filePath))
        {
            // Set data
            SetData(gameData);

            Debug.Log($"<color=#FFAD5A>Default Game Data Loaded Successfully!</color>");
            return;
        }

        var json = File.ReadAllText(_filePath);

        try
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        catch
        {
            Debug.Log($"<color=#FF5959>[GAME LOAD]</color> Unsuccessful. Load default data.");
        }

        // Set data
        SetData(gameData);
        Debug.Log($"<color=#729D39>[GAME LOAD]</color> Successful! File Path: {_filePath}");
    }

    private void Save()
    {
        // Get Data
        var gameData = GetData();

        var json = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(_filePath, json);

        Debug.Log($"<color=#729D39>[GAME SAVE]</color> Successful! File Path: {_filePath}");
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