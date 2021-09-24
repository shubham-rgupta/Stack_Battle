using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject white_Cubes;
    public GameObject cubes_Parent;
    public float min_Cubes;
    void Start()
    {
        
    }
    private void Update()
    {
        if (cubes_Parent.transform.childCount <= min_Cubes)
        {
            StartCoroutine(startSpawning());
        }
    }
    IEnumerator startSpawning()
    {
        yield return new WaitForSecondsRealtime(3f);
        GameObject obj = Instantiate(white_Cubes, spawnPoints[0].position, Quaternion.identity);
        obj.tag = "Colorless";
        obj.layer = LayerMask.NameToLayer("Colorless");
        obj.transform.SetParent(cubes_Parent.transform);
        GameObject obj2 = Instantiate(white_Cubes, spawnPoints[1].position, Quaternion.identity);
        obj2.tag = "Colorless";
        obj2.layer = LayerMask.NameToLayer("Colorless");
        obj2.transform.SetParent(cubes_Parent.transform);
        GameObject obj3 = Instantiate(white_Cubes, spawnPoints[2].position, Quaternion.identity);
        obj3.tag = "Colorless";
        obj3.layer = LayerMask.NameToLayer("Colorless");
        obj3.transform.SetParent(cubes_Parent.transform);
        GameObject obj4 = Instantiate(white_Cubes, spawnPoints[3].position, Quaternion.identity);
        obj4.tag = "Colorless";
        obj4.layer = LayerMask.NameToLayer("Colorless");
        obj4.transform.SetParent(cubes_Parent.transform);
    }
}
