using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapStart : MonoBehaviour
{
    public static TapStart obj;


    public GameObject start_Image;

    public float valueX,valueY;
    public LeanTweenType tweenType;
    public float time;
    private float timeScale;
    public bool startGame = false;
    private Vector2 size;
    void Start()
    {
        obj = this;
        size = start_Image.transform.localScale;
        Tween();
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
        
    }

    public void StartGame()
    {
        GameManager.obj.JoyStickPanel();
        Time.timeScale = timeScale;
        startGame = true;     
    }

    private void Tween()
    {
        LeanTween.scaleX(start_Image, valueX * size.x, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true);
        LeanTween.scaleY(start_Image, valueY * size.y, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true);
        LeanTween.delayedCall(time, () => LeanTween.scale(start_Image, size, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true));
    }
}
