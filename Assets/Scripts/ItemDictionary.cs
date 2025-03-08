using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
public class ItemDictionary : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionData> questions;
    
    List<QuestionData> selectedQuestions = new();

    [Header("Singleton")]
    public static ItemDictionary Instance { get; private set; }

    private static string[] questionAnswers;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("Duplicate ItemDictionary!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Debug.Log("Defining Item Dictionary Instance");
        Debug.Log("ItemDictionary start has "+questions.Count+" items.");
        GenerateListOfAllAnswers();
        GenerateSelectedListOfAllAnswers();
    }

    public void UpdatePlantsAssetDatas(List<QuestionData> list)
    {
        Debug.Log("Reloading Plants");
        questions = list;
        Debug.Log(questions.Count + " Plants found!");
        GenerateListOfAllAnswers();

#if UNITY_EDITOR
        EditorUtility.SetDirty(this); // Marks the ScriptableObject as modified
        AssetDatabase.SaveAssets();   // Ensures the change is written to disk
        AssetDatabase.Refresh();      // Refreshes the asset database
#endif
    }

    private void GenerateListOfAllAnswers()
    {
        questionAnswers = new string[questions.Count];
        for (int i = 0; i < questions.Count; i++) 
            questionAnswers[i] = questions[i].ItemName;        
        Debug.Log("Generating "+questionAnswers.Length+" question Answers.");
    }
    
    public void GenerateSelectedListOfAllAnswers()
    {
        
        selectedQuestions.Clear();
        selectedQuestions = questions.Where(x => (x.CategoriesBinary&Settings.Instance.SelectedCategoriesBinary)!=0).ToList();


        Debug.Log("Generating List Of Selected questions: "+ selectedQuestions.Count+" question Answers.");
    }

    public static QuestionData GetRandomQuestionData()
    {
        // Make sure the dictionary exists
        if (Instance == null || Instance.selectedQuestions.Count == 0) {
            Debug.LogWarning("Item Dictionary does not have any items in it.");
            return null;
        }

        // Get random index from the dictionary
        int index = Random.Range(0, Instance.selectedQuestions.Count);
        Debug.Log("Getting question index "+index+" from the quesions of length "+ Instance.selectedQuestions.Count+".");
        return Instance.selectedQuestions[index];
    }

    internal static List<string> GetRandomWrongAnswerStrings(string itemName, int amt)
    {
        // Will run until a random item that is not allready used is picked. Change to limit this and use more mem instead
        List<string> ans = new List<string>();
        while(ans.Count < amt) {
            int index = Random.Range(0, questionAnswers.Length);
            string answer = questionAnswers[index];
            if(itemName != answer && !ans.Contains(answer))
                ans.Add(answer);
        }
        return ans;
    }

    internal static List<string> GetBestFit(string input, int v)
    {
        if(input == "") return new List<string>();

        //String.Equals(s, "Foo", StringComparison.CurrentCultureIgnoreCase));
        
        // Return the X best options for this request
        return questionAnswers.Where(x => x.Contains(input, StringComparison.CurrentCultureIgnoreCase)).Take(v).ToList();
    }
}
