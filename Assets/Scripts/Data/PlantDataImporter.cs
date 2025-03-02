using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

public class PlantDataImporter : EditorWindow
{
    private const string CSV_PATH = "Assets/Data/Plants.csv";
    private const string IMAGE_FOLDER = "Assets/Sprites/Plants/";
    private const string SCRIPTABLE_OBJECT_FOLDER = "Assets/Data/Plants/";
        
    [MenuItem("Tools/Import Plant Data")]
    public static void ImportPlantData()
    {
        if (!File.Exists(CSV_PATH)) {
            Debug.LogError("CSV file not found at: " + CSV_PATH);
            return;
        }

        if (!Directory.Exists(SCRIPTABLE_OBJECT_FOLDER)) {
            Directory.CreateDirectory(SCRIPTABLE_OBJECT_FOLDER);
            AssetDatabase.Refresh();
        }

        //string[] lines = File.ReadAllLines(CSV_PATH);
        string[] lines = ReadCsvWithSharedAccess(CSV_PATH);


        for (int i = 1; i < lines.Length; i++) // Skip header row
        {
            string[] values = lines[i].Split(';');
            if (values.Length < 2) continue; // Ensure we have at least itemName & LatinName

            string latinName = values[0].Trim();
            string itemName = values[1].Trim();
            string info = values.Length > 2 ? values[2].Trim():"No Info!";

            // Find all matching images
            Sprite[] sprites = FindSpritesForPlant(latinName);

            // Save the asset
            string assetPath = SCRIPTABLE_OBJECT_FOLDER + latinName + ".asset";

            if (sprites.Length == 0) {
                Debug.LogWarning($"No images found for {latinName}");

                DeletePlantDataIfAvailable(assetPath);
                continue;
            }

            // Create or update ScriptableObject
            QuestionData plantData = CreateOrUpdatePlantData(itemName, latinName, info, sprites);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static string[] ReadCsvWithSharedAccess(string path)
    {
        try {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
                List<string> lines = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lines.Add(line);
                }
                return lines.ToArray();
            }
        }
        catch (IOException e) {
            Debug.LogError("Error reading CSV file: " + path + "\n" + e.Message);
            return null;
        }
    }


    private static Sprite[] FindSpritesForPlant(string latinName)
    {
        List<Sprite> spriteList = new List<Sprite>();
        
        string[] files = Directory.GetFiles(IMAGE_FOLDER, latinName + "*.*", SearchOption.TopDirectoryOnly);

        foreach (string file in files) {
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")) {
                
                //string assetPath = Application.dataPath +"Assets/";
                string assetPath = file.Replace(Application.dataPath, "").Replace("\\", "/");
                Debug.Log("Asset Path: "+assetPath);


                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                if (sprite != null) {
                    Debug.LogWarning("Loaded sprite: "+latinName+" at "+assetPath);
                    spriteList.Add(sprite);
                }else
                    Debug.LogWarning("Could not load sprite: "+latinName+" at "+assetPath);
            }
        }

        return spriteList.ToArray();
    }
        
    public static List<QuestionData> ReturnAllData()
    {
        List<QuestionData> datas = new();
        
        Debug.Log("Trying to read all data from library: ");

        string[] files = Directory.GetFiles(SCRIPTABLE_OBJECT_FOLDER);

        foreach (string file in files) {
            if(!file.EndsWith(".asset"))
                continue;

            string assetPath = file.Replace(Application.dataPath, "").Replace("\\", "/");

            QuestionData questionData = AssetDatabase.LoadAssetAtPath<QuestionData>(assetPath);

            datas.Add(questionData);            
        }

        return datas;
    }        
        
    private static void DeletePlantDataIfAvailable(string assetPath)
    {
        if (AssetDatabase.AssetPathExists(assetPath)) {
            AssetDatabase.DeleteAsset(assetPath);
        }
    }
    
    private static QuestionData CreateOrUpdatePlantData(string itemName, string latinName, string info, Sprite[] sprites)
    {
        Debug.Log("Create Or Update Plant Data: " + latinName);
        QuestionData plantData = GetData(latinName);

        plantData.ItemName = itemName;
        plantData.LatinName = latinName;
        plantData.info = info;
        plantData.sprites = sprites;

        return plantData;
    }

    public static QuestionData GetData(string latinName)
    {
        string assetPath = SCRIPTABLE_OBJECT_FOLDER + latinName + ".asset";
        QuestionData plantData = AssetDatabase.LoadAssetAtPath<QuestionData>(assetPath);

        if (plantData == null) {
            plantData = ScriptableObject.CreateInstance<QuestionData>();
            AssetDatabase.CreateAsset(plantData,assetPath);
        }
        return plantData;
    }

}
