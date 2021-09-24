using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterModels : MonoBehaviour
{

    public float zFloatTime, xFloatTime;
    public bool floatOnX, floatOnZ;
    public Vector2 forZ, forX;
    public int Z, X;

    private void OnEnable()
    {
        if (floatOnZ)
            Z = LeanTween.rotateZ(this.gameObject, Random.Range(forZ.x, forZ.y), zFloatTime).setLoopPingPong().uniqueId;
        if (floatOnX)
            X = LeanTween.rotateX(this.gameObject, Random.Range(forX.x, forX.y), xFloatTime).setLoopPingPong().uniqueId;
    }
    private void OnDisable()
    {
        LeanTween.pause(Z);
        LeanTween.pause(X);
    }
}
