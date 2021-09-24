using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public GameObject gameWon_Image;

    public float valueX, valueY;
    public LeanTweenType tweenType;
    public float time;
    private Vector2 size;
    void Start()
    {
        size = gameWon_Image.transform.localScale;
        Tween();
    }

    // Update is called once per frame
    private void Tween()
    {
        LeanTween.scaleX(gameWon_Image, valueX * size.x, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true);
        LeanTween.scaleY(gameWon_Image, valueY * size.y, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true);
        LeanTween.delayedCall(time, () => LeanTween.scale(gameWon_Image, size, time).setEase(tweenType).setLoopPingPong(-1).setIgnoreTimeScale(true));
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
