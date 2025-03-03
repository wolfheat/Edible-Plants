using UnityEngine;

[CreateAssetMenu(menuName = "QuestionData")]
public class QuestionData : ScriptableObject
{
    [Header("Main Info")]
    public string ItemName;
    public string LatinName;
    public string info;
    public Sprite[] sprites;

    [Header("Edible Parts")]
    public int root;
    public int stem;
    public int leaf;
    public int flower;
    public int seed;
    public int[] PlantParts => new int[]{root,stem,leaf,flower,seed};

}
