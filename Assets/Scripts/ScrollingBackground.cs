using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public RawImage backgroundImage;
    public float scrollSpeedX = 0.05f;
    public float scrollSpeedY = 0.1f;

    void Update()
    {
        Rect uvRect = backgroundImage.uvRect;
        uvRect.x += scrollSpeedX * Time.deltaTime;
        uvRect.y += scrollSpeedY * Time.deltaTime;
        backgroundImage.uvRect = uvRect;
    }
}