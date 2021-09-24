using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public float speed;
    public GameObject target;
    public GameObject explosion_Effect;
    public GameObject fire_Effect;
    public GameObject fire_Point;
    private void Start()
    {
        StartCoroutine(Fire());
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    void Update()
    {
        var dir2 = (target.transform.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * dir2;
        transform.LookAt(target.transform.position);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bot")
        {

            GameObject effect = Instantiate(explosion_Effect, transform.position, Quaternion.identity, target.transform);
            other.gameObject.GetComponent<Bot1>().botDestroy();
            //other.gameObject.GetComponent<Bot1>().botDestroy();
            //other.gameObject.GetComponent<Bot1>().botDestroy();
            other.gameObject.GetComponent<Bot1>().MakeSlow(5f);
            //other.gameObject.GetComponent<Bot1>().ChangeDirection();
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            
            GameObject effect = Instantiate(explosion_Effect, transform.position, Quaternion.identity,target.transform);
            other.gameObject.GetComponent<PlayerMove>().playerDestroy();
            //other.gameObject.GetComponent<PlayerMove>().playerDestroy();
            //other.gameObject.GetComponent<PlayerMove>().playerDestroy();
            other.gameObject.GetComponent<PlayerMove>().MakeSlow(5f);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(fire_Effect, fire_Point.transform.position, fire_Point.transform.rotation, fire_Point.transform);
    }
}
