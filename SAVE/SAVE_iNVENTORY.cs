
using System.IO;
using UnityEngine;

[System.Serializable]
public class InventorySave
{
    public int[] itemCuantity;
    public int coins;
}

public class SAVE_iNVENTORY : MonoBehaviour
{
    static string SavePath => Path.Combine(
        Application.persistentDataPath,
        "inventory.json"
    );

    public static void RESET_DATA() 
    {
        string json = JsonUtility.ToJson(new InventorySave());
        File.WriteAllText(SavePath, json);
    }

    static InventorySave GetData()
    {
        if (!File.Exists(SavePath))
        {
            return new InventorySave
            {
                itemCuantity = new int[0],
                coins = 0
            };
        }

        string json = File.ReadAllText(SavePath);

        InventorySave data = JsonUtility.FromJson<InventorySave>(json);

        if (data == null)
            data = new InventorySave();

        if (data.itemCuantity == null)
            data.itemCuantity = new int[0];

        return data;
    }

    static void SaveData(InventorySave data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    // Inicializar o actualizar el tamaño de la lista
    public static void StartItemList(int length)
    {
        InventorySave data = GetData();

        if (data.itemCuantity.Length != length)
        {
            System.Array.Resize(ref data.itemCuantity, length);
            SaveData(data);
        }
    }

    // MONEDAS

    public static int GetCoins()
    {
        return GetData().coins;
    }

    public static void SAVE_Coins(int coins)
    {
        InventorySave data = GetData();

        data.coins = coins;

        SaveData(data);
    }

    // OBJETOS

    public static int GetItemCuantity(int id)
    {
        InventorySave data = GetData();

        if (id < 0 || id >= data.itemCuantity.Length)
        {
            Debug.LogError("Inventory ID fuera de rango: " + id);
            return 0;
        }

        return data.itemCuantity[id];
    }

    public static void SAVE_ItemCuantity(int id, int cuantity)
    {
        InventorySave data = GetData();

        if (id < 0 || id >= data.itemCuantity.Length)
        {
            Debug.LogError("Inventory ID fuera de rango: " + id);
            return;
        }

        data.itemCuantity[id] = cuantity;

        SaveData(data);
    }
}
