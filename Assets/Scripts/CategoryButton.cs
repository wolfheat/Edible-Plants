using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    [SerializeField] private Image backgroundColor;
    [SerializeField] private TextMeshProUGUI textField;

    [SerializeField] private AspectRatioFitter aspectRatioFitter;
    [SerializeField] private RectTransform rectTransform;

    private Color activeColor = Color.green;
    private Color inActiveColor = Color.red;

    internal string ButtonText() => textField.text;

    private void Start()
    {
        OnRescaledWindow();
    }


    void OnDrawGizmos()
    {
        OnRescaledWindow();
    }

    public void OnButtonClick() => FindFirstObjectByType<Categories>()?.OnClickButton(this);

    [ContextMenu("Scale Aspect")]
    public void OnRescaledWindow()
    {
        // Set Border Size as Square of smallest widht or height
        bool widthLargerThanHeight = rectTransform.sizeDelta.x < rectTransform.sizeDelta.y;
        float smallest = widthLargerThanHeight ? rectTransform.sizeDelta.x : rectTransform.sizeDelta.y;


        rectTransform.sizeDelta = new Vector2(smallest,smallest);

        rectTransform.ForceUpdateRectTransforms();

#if UNITY_EDITOR
        SceneView.RepaintAll();
#endif
        //aspectRatioFitter.aspectMode = widthLargerThanHeight ? AspectRatioFitter.AspectMode.WidthControlsHeight : AspectRatioFitter.AspectMode.HeightControlsWidth;
    }


    internal void SetButtonActive(bool set) => backgroundColor.color = set ? activeColor : inActiveColor;
}
