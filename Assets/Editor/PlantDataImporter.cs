using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;
public class PlantDataImporter : EditorWindow
{
    private const string CSV_PATH = "Assets/Data/Plants.csv";
    private const string IMAGE_FOLDER = "Assets/Sprites/Plants/";
    private const string SCRIPTABLE_OBJECT_FOLDER = "Assets/Data/Plants/";
        
    [MenuItem("Tools/Import Plant Data from Plants.cvs")]
    public static void ImportPlantData()
    {
        Debug.Log("Updating Plant Data from Plants.cvs: ");
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

        int updatedItems = 0;
        for (int i = 1; i < lines.Length; i++) // Skip header row
        {
            //Debug.Log("Line "+i+": " + lines[i]);
            string[] values = lines[i].Split(';');
            if (values.Length < 2) continue; // Ensure we have at least itemName & LatinName
            if (values[0].Length == 0) continue;

            // DATA LOOKS LIKE THIS
            //      0	          1           2         3        4        5       6        7      8        9     10        11
            // Latin Name	Swedish Name	Info	Vanlighet	Root	Stem	Leafs	Flower	Seeds	Berries	Avoid	Förvildad

            string latinName = values[0].Trim();
            string itemName = values[1].Trim();
            string info = FinalizeInfo(values[2]);
            Debug.Log("parsing commonness [" + values[0] +"]: " + values[3]);
            int commonness = Int32.Parse(values[3]);
            // Edibles 4-9
            int[] edible = new int[7];
            for (int j = 4; j < 7+4; j++) {
                int index = j - 4;
                //Debug.Log("parsing:" + values[j]);
                int value = (j>=values.Length || values[j].Length == 0) ? 0 : Int32.Parse(values[j]);
                edible[index] = value;
            }
            bool feral = values[11]!="";
            bool protectedPlant = values[12]!="";

            int treeBush = values[13].Length > 0 ? Int32.Parse(values[13]):0;
            string medicinal = values[14];
            int army = values[15].Length > 0 ? Int32.Parse(values[15]):0;

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
            QuestionData plantData = CreateOrUpdatePlantData(itemName, latinName, info, commonness, sprites, edible, feral, protectedPlant, treeBush, medicinal, army);
            updatedItems++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Updating Plant Data from Plants.cvs: COMPLETE ["+updatedItems+"]");

    }

    private static string FinalizeInfo(string info)
    {
        // Removet any brackets section
        string removedBrackets = RemoveBrackets(info);

        // remove any other unwanted stuff...


        return removedBrackets;
    }

    private static string RemoveBrackets(string info)
    {
        int brackets = 0;

        char[] chars = info.ToCharArray();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chars.Length; i++) {
            char c = chars[i];
            if (c == '[') {
                brackets++;
                continue;
            }
            else if (c == ']') {
                brackets--;
                continue;
            }
            else if (brackets == 0) {
                sb.Append(c);
            }
        }
        return sb.ToString();
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
                //Debug.Log("Asset Path: "+assetPath);

                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                if (sprite != null) {
                    //Debug.Log("Loaded sprite: "+latinName+" at "+assetPath);
                    spriteList.Add(sprite);
                }else
                    Debug.LogWarning("Could not load sprite: "+latinName+" at "+assetPath);
            }
        }

        return spriteList.ToArray();
    }
        
    private static void DeletePlantDataIfAvailable(string assetPath)
    {
        if (AssetDatabase.AssetPathExists(assetPath)) {
            AssetDatabase.DeleteAsset(assetPath);
        }
    }
    
    private static QuestionData CreateOrUpdatePlantData(string itemName, string latinName, string info, int commonness, Sprite[] sprites, int[] edible, bool feral, bool protectedPlant, int treeBush, string medicinal, int army)
    {
        //Debug.Log("Create Or Update Plant Data: " + latinName);
        
        string assetPath = SCRIPTABLE_OBJECT_FOLDER + latinName + ".asset";
        QuestionData plantData = AssetDatabase.LoadAssetAtPath<QuestionData>(assetPath);

        bool dataExists = plantData != null;

        if(!dataExists) {        
            plantData = ScriptableObject.CreateInstance<QuestionData>();
        }

        plantData.ItemName = itemName;
        plantData.LatinName = latinName;
        plantData.info = info;
        plantData.sprites = sprites;
        plantData.root = edible[0];
        plantData.stem = edible[1];
        plantData.leaf = edible[2];
        plantData.flower = edible[3];
        plantData.seed = edible[4];
        plantData.fruit = edible[5];
        plantData.avoid = edible[6];
        plantData.treeBush = treeBush;
        plantData.medicinal = medicinal;
        plantData.army = army;

        plantData.commonness = commonness;
        plantData.feral = feral;
        plantData.protectedPlant = protectedPlant;

        plantData.CreateCategories(); 

        if (!dataExists)
            AssetDatabase.CreateAsset(plantData,assetPath);

        // Make sure the changes are stored
        EditorUtility.SetDirty(plantData);

        return plantData;
    }

}
