using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class PlantDataReader 
{
    private const string SCRIPTABLE_OBJECT_FOLDER = "Assets/Data/Plants/";

    public static List<QuestionData> ReturnAllData()
    {
        List<QuestionData> datas = new();

#if UNITY_EDITOR
        Debug.Log("Trying to read all data from library: ");

        string[] files = Directory.GetFiles(SCRIPTABLE_OBJECT_FOLDER);

        foreach (string file in files) {
            if (!file.EndsWith(".asset"))
                continue;

            string assetPath = file.Replace(Application.dataPath, "").Replace("\\", "/");

            QuestionData questionData = AssetDatabase.LoadAssetAtPath<QuestionData>(assetPath);

            datas.Add(questionData);
        }
#endif
        return datas;
    }

}
