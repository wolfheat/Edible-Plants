using UnityEngine;
using UnityEngine.UI;

public class AnimateUIAnimation : MonoBehaviour
{
    private const float MinWait = 0.2f;
    private const float MaxWait = 1.4f;
    [SerializeField] private float animationsSpeed;

    [SerializeField] private Sprite[] sprites; 
    [SerializeField] private Image image; 
    [SerializeField] private bool randomWaiting = true; 

    private float flapWait = 2f;
    private bool waiting = false;

    private float timer = 0f;
    private int activeSprite = 0;
    
    void Update()
    {
        if (waiting) {
            timer += Time.deltaTime;
            if (timer > flapWait) {
                timer = 0f;
                waiting = false;
            }                 
        }

        if (!waiting)
            Flap();
    }

    private void Flap()
    {
        timer += Time.deltaTime * animationsSpeed;
        if (timer > 1f) {
            timer = 0;
            StepAnimation();
        }
    }

    private void StepAnimation()
    {
        Debug.Log("Updating Step with speed: "+animationsSpeed);
        if (sprites.Length == 0) return;
        activeSprite++;
        if(activeSprite >= sprites.Length) {
            activeSprite = 0;
            if (randomWaiting) {
                waiting = true;
                flapWait = Random.Range(MinWait, MaxWait);
            }
        }

        ShowSprite(activeSprite);
    }

    private void ShowSprite(int activeSprite) => image.sprite = sprites[activeSprite];
}
