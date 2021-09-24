using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject indicator;
    private Renderer rd;
    Camera camera;
    void Start()
    {
        rd = GetComponent<Renderer>();
        camera = Camera.main;
    }
    private void Update()
    {
        //if()
    }

    void FixedUpdate()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
        //Debug.Log(screenPos);
    }
}
