using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ShipSerializer
{
    private const string ShipExtension = ".shp";
    private const string ShipFileNameWithoutExtension = "Ship";
    private const string ShipFileName = ShipFileNameWithoutExtension + ShipExtension;

    private static string ApplicationFolderPath => Application.persistentDataPath;
    private static readonly string ShipFullPath = $"{ApplicationFolderPath}/{ShipFileName}";

    public static void SerializeShip(ShipData ship)
    {
        string jsonString = JsonUtil.ToJson(ship);
        File.WriteAllText(ShipFullPath, jsonString);
    }

    public static ShipData DeserializeShip()
    {
        string jsonString = File.ReadAllText(ShipFullPath);
        return JsonUtil.FromJson<ShipData>(jsonString);
    }

    public static bool HasSavedShip()
    {
        return File.Exists(ShipFullPath);
    }
}
