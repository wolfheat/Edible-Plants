using UnityEngine;

[CreateAssetMenu(menuName = "QuestionData")]
public class QuestionData : ScriptableObject
{
    [Header("Main Info")]
    public string ItemName;
    public string LatinName;
    [TextArea(15,20)]
    public string info;
    public Sprite[] sprites;

    [Header("Availability")]
    public int commonness;
    public bool feral;

    [Header("Edible Parts")]
    public int root;
    public int stem;
    public int leaf;
    public int flower;
    public int seed;
    public int fruit;
    public int avoid;

    public int[] PlantParts => new int[]{root,stem,leaf,flower,seed,fruit,avoid};

}
