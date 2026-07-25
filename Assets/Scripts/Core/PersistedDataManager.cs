using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


///////////////////////////
//SAVE DATA CLASSES ///////
///////////////////////////

[Serializable]
public class WorldSaveData
{
    public int seed;
    //public List<TileSaveData> modifiedTiles = new();
    public List<WorldObjectData> modifiedObjects = new();
    public List<WorldEntityData> worldEntities = new();
}

[System.Serializable]
public class PlayerSaveData
{
    public float currentHealth;
    public float currentEnergy;

    public Vector3 currentPosition;

    public InventorySaveData inventory;
    public EquipmentNodeSaveData equipment;
}

[Serializable]
public class EquipmentNodeSaveData
{
    public string slotId;

    public InventorySaveData inventory;

    public List<EquipmentNodeSaveData> children = new();
}

//[Serializable]
//public class TileSaveData
//{
//    public int x;
//    public int y;
//    public TileType type;
//    public int health;
//}


[System.Serializable]
public class InventorySaveData
{
    public int width;

    public int height;

    public List<ItemSaveData> items =
        new();
}

[System.Serializable]
public class ItemSaveData
{
    public string itemName;

    public int rotation;

    public int posX;
    public int posY;
}


[System.Serializable]
public class LootChestSaveData
{
    public bool isOpened;

    public bool isEmpty;

    public InventorySaveData inventory;
}

[System.Serializable]
public class EnemyMemorySaveData
{
    public Vector2 SpawnPosition;

    public Vector2 LastSeenPlayerPosition;

    public float TimeSincePlayerSeen;

    public bool HasSeenPlayer;
}

[Serializable]
public abstract class WorldInstanceData
{
    public string Guid;

    public string PrefabID;

    public Vector2 Position;
    public Quaternion Rotation;

    public bool Destroyed;

    public string CustomDataJson;
}

[Serializable]
public class WorldObjectData : WorldInstanceData
{
}

[Serializable]

public class WorldEntityData : WorldInstanceData
{
    // Future entity-specific fields
}

[System.Serializable]
public class LightWorldObjectSaveData
{
    public float minBrightness;
    public float maxBrightness;

    public float minFalloff;
    public float maxFalloff;

    public float flickerSpeed;

    public float animationSpeedMultiplier;

    public SerializableColor color;

    public bool setSpriteColor;
}

[System.Serializable]
public struct SerializableColor
{
    public float r;
    public float g;
    public float b;
    public float a;

    public SerializableColor(Color c)
    {
        r = c.r;
        g = c.g;
        b = c.b;
        a = c.a;
    }

    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }
}

[System.Serializable]
public class LightSpawnData
{

    public float range = 10f;
    public float maxRange = 10f;
    public Color lightColor = Color.white;

    public bool isCone = false;
    [Range(0f, 360f)]
    public float coneAngle = 90f; // total angle

    public bool isDynamic = false;

    public Vector3 offset;
    public Vector3 rotation;
}

public class ObjectSpawnData
{
    public string Guid;

    public string PrefabID;

    public string InitialStateJson;

    public Vector2 Position;
    public Quaternion Rotation;
}

public class EntitySpawnData
{
    public string Guid;

    public string PrefabID;

    public string InitialStateJson;

    public Vector2 Position;

    public Quaternion Rotation;
}


















public class PersistedDataManager : MonoBehaviour
{
    public static PersistedDataManager Instance { get; private set; }

    public string RootDir => Application.persistentDataPath;
    public string LogsDir => Path.Combine(RootDir, "Logs");
    public string SavesDir => Path.Combine(RootDir, "Saves");

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeDirectories();

        DebugManager.Initialize();
        DebugManager.Log(this, "Awake Finished");
    }

    private void Update()
    {

        DebugManager.Update();

    }
    private void OnApplicationQuit()
    {

        DebugManager.Shutdown();

    }

    void InitializeDirectories()
    {
        Directory.CreateDirectory(LogsDir);
        Directory.CreateDirectory(SavesDir);
    }

    // -------------------------
    // GENERIC FILE WRITING
    // -------------------------

    public void AppendLines(string path, string[] lines)
    {
        try
        {
            File.AppendAllLines(path, lines);
        }
        catch (Exception e)
        {
            Debug.LogError($"AppendLines failed: {e}");
        }
    }

    public void WriteAllText(string path, string content)
    {
        try
        {
            File.WriteAllText(path, content);
        }
        catch (Exception e)
        {
            Debug.LogError($"WriteAllText failed: {e}");
        }
    }

    // -------------------------
    // LOG FILE SUPPORT
    // -------------------------

    public string CreateLogFile()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string path = Path.Combine(LogsDir, $"log_{timestamp}.txt");

        string header =
            $"=== SESSION START ===\n" +
            $"Time: {DateTime.Now}\n" +
            $"Unity Version: {Application.unityVersion}\n" +
            $"Platform: {Application.platform}\n\n";

        WriteAllText(path, header);

        return path;
    }

    // -------------------------
    // WORLD SAVE
    // -------------------------

    //public void SaveWorld(WorldData world, int seed, string saveName = "save")
    //{
    //    WorldSaveData save = new WorldSaveData();
    //    save.seed = seed;

    //    var modified = world.GetModifiedTiles();

    //    foreach (var kvp in modified)
    //    {
    //        save.modifiedTiles.Add(new TileSaveData
    //        {
    //            x = kvp.Key.x,
    //            y = kvp.Key.y,
    //            type = kvp.Value.Type,
    //            health = kvp.Value.Health
    //        });
    //    }

    //    var modifiedObjects = world.GetModifiedObjects();

    //    foreach (var kvp in modifiedObjects)
    //    {
    //        save.modifiedObjects.Add(kvp.Value);
    //    }

    //    Debug.Log("SAVING " + world.GetEntities().Count + " ENTITIES");
    //    foreach (var kvp in world.GetEntities())
    //    {
    //        save.worldEntities.Add(kvp.Value);
    //    }


    //    string json = JsonUtility.ToJson(save, true);

    //    string path = Path.Combine(SavesDir, $"{saveName}.json");
    //    WriteAllText(path, json);

    //    Debug.Log($"World saved to {path}");
    //}


    public WorldSaveData LoadWorld(string saveName = "save")
    {
        string path = Path.Combine(SavesDir, $"{saveName}.json");

        if (!File.Exists(path))
        {
            Debug.Log("No save found, generating new world.");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<WorldSaveData>(json);
    }


    public void SavePlayer(PlayerSaveData dataIn, string saveName = "player")
    {

        string json = JsonUtility.ToJson(dataIn, true);

        string path = Path.Combine(SavesDir, $"{saveName}.json");
        WriteAllText(path, json);

        Debug.Log($"Player saved to {path}");

    }


    public PlayerSaveData LoadPlayer(string saveName = "player")
    {
        string path = Path.Combine(SavesDir, $"{saveName}.json");

        if (!File.Exists(path))
        {
            Debug.Log("No save found, generating new world.");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}