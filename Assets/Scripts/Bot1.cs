using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot1 : MonoBehaviour
{
    public static Bot1 obj;
    public GameObject parentObj;
    public GameObject bot_Body;
    public float moveSpeed;
    private Rigidbody rb;
    public Vector3 moveDirection;
    public GameObject baseCube;
    public GameObject botCubePrefab;

    public List<GameObject> cubeList = new List<GameObject>();
    public int index = 0;
    public float minDist, maxDist;


    public Animator BAnimator;


    //RayCast for Walls
    public float ray_Range;
    public LayerMask wall_Layer;

    //Inner Raycast for cubes 
    public Collider[] cubes;
    private GameObject innerHitObject;
    public float radius;
    private Vector3 origin;
    private Vector3 direction;
    public LayerMask cubelayermask;

    //Outer Raycast for cubes 
    public Collider[] enemies;
    public float outerRadius;
    //public LayerMask enemylayermask;



    public bool isMove = true;
    public Vector3 tempPos;




    public GameObject effect_point;
    public GameObject evolve_Effect;


    //Spherecast
    private Vector3 origin_S;
    public float radius_S;
    private Vector3 direction_S;
    public float max_Distance;
    public LayerMask botmask;

    //Weapons
    float shoot_Time = 0;
    //public float pistol_shoot_Delay;
    public float bazooka_Shoot_Delay;
    public float rifle_Shoot_Delay;
    public float tank_Shoot_Delay;

    public GameObject enemy_Body;
    public bool isShooting = false;
    public GameObject weapon_Point;

    //public GameObject pistol_ShootingPoint;
    //public GameObject pistol;
    public GameObject pistol_Bullet;
    public GameObject pistol_Muzzle;

    public GameObject rifle_ShootingPoint;
    public GameObject rifle;

    //public GameObject bazooka_ShootingPoint;
    //same shooting point of pistol
    public GameObject bazooka;
    public GameObject bazooka_Bullet;
    public GameObject bazooka_Muzzle;
    public GameObject bazooka_ShootingPoint;

    public GameObject tank;
    public GameObject tank_Bullet;
    public GameObject tank_Muzzle;
    public GameObject tank_ShootingPoint;

    //bools for weapons
    //private bool isPistol = false;
    private bool isRifle = false;
    private bool isBazooka = false;
    private bool isTank = false;

    public GameObject water_Effect;
    public float sp;
    public GameObject stackUp_Puff;
    void Start()
    {
        sp = moveSpeed;
        bot_Body.SetActive(true);
        tank.SetActive(false);
        rb = GetComponent<Rigidbody>();
        cubeList.Add(baseCube);
        ChangeDirection();
        rifle.SetActive(false);
        bazooka.SetActive(false);
    }


    void Update()
    {

        //if (index >= 4 && index <= 7 && !isPistol)
        //{
        //    Give_Pistol();
        //}

        if (index > 5 && index <= 15 && !isRifle)
        {
            Give_Rifle();
        }

        if (index > 15 && index <= 22 && !isBazooka)
        {
            Give_Bazooka();
        }
        if (index > 22 && !isTank)
        {
            Give_Tank();
        }





        if (TapStart.obj.startGame)
        {
            if (isMove)
            {
                //Debug.Log(index);
                transform.position += moveSpeed * Time.deltaTime * moveDirection;
                //rb.AddForce(moveDirection* moveSpeed*Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(moveDirection);

                //RAYCAST FOR WALLS
                Vector3 pos = transform.position;
                pos.y = 1f;
                Vector3 fwd = transform.forward;
                fwd.y = 1f;
                if (Physics.Raycast(pos, fwd, ray_Range, wall_Layer))
                {
                    moveDirection = -moveDirection;
                    ChangeDirection();
                }




                //INNER OVERLAPSPHERE FOR WHITE CUBES
                origin = transform.position;
                origin.y = 1f;
                if (enemy_Body == null)
                {
                    cubes = Physics.OverlapSphere(origin, radius, cubelayermask);
                    if (cubes != null)
                    {
                        for (int i = 1; i < cubes.Length; ++i)
                        {
                            if (cubes[i].gameObject.layer == LayerMask.NameToLayer("Colorless"))
                            {
                                tempPos = cubes[i].transform.position;
                                moveDirection = (tempPos - transform.position).normalized;
                                moveDirection.y = 0;
                            }
                            //else if(cubes[i].gameObject.layer == LayerMask.NameToLayer("Bots"))
                            //{
                            //    ChangeDirection();
                            //}
                        }
                    }
                }



                //Spherecast for bots and players
                origin_S = transform.position;
                origin_S.y = 0f;
                direction_S = transform.forward;
                shoot_Time += Time.deltaTime;
                RaycastHit hit;

                if (enemy_Body == null && index > 5)
                {
                    if (Physics.SphereCast(origin_S, radius_S, direction_S, out hit, max_Distance, botmask))
                    //if(Physics.Raycast(origin_S, transform.forward, out hit, 12f, botmask))
                    {
                        
                        if (hit.collider.gameObject.tag == "Bot")
                        {
                            if (index > hit.collider.gameObject.GetComponent<Bot1>().index && hit.collider.gameObject.GetComponent<Bot1>().index >= 0)
                            {

                                enemy_Body = hit.collider.gameObject;
                            }
                            else
                            {
                                enemy_Body = null;
                            }
                        }
                        else if (hit.collider.gameObject.tag == "Player")
                        {
                            if (index > hit.collider.gameObject.GetComponent<PlayerMove>().listLength && hit.collider.gameObject.GetComponent<PlayerMove>().listLength >= 0)
                            {

                                enemy_Body = hit.collider.gameObject;
                            }
                            else
                            {
                                enemy_Body = null;
                            }
                        }
                    }
                }
                if(enemy_Body!=null)
                    KillTarget();

                ////OUTER OVERLAPSPHERE FOR BOTS AND PLAYER
                //enemies = Physics.OverlapSphere(origin, outerRadius, enemylayermask);


                //if (enemies.Length > 1)
                //{
                //    for (int i = 1; i < enemies.Length; ++i)
                //    {
                //        if (enemies[i].gameObject.layer == LayerMask.NameToLayer("Bots"))
                //        {
                //            //print("Bot Detected");
                //            //if (this.index > enemies[i].GetComponent<Bot1>().index) //IF THIS IS POWERFUL
                //            //{
                //            //    tempPos = enemies[i].transform.position;
                //            //    moveDirection = (tempPos - transform.position).normalized;
                //            //    moveDirection.y = 0;
                //            //}
                //        }

                //        if (enemies[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                //        {
                //            //print("Player Detected");
                //            //if (this.index > PlayerMove.obj.listLength) //IF THIS IS POWERFUL
                //            //{
                //            //    tempPos = enemies[i].transform.position;
                //            //    moveDirection = (tempPos - transform.position).normalized;
                //            //    moveDirection.y = 0;
                //            //}
                //        }
                //    }
                //}
            }
        }
    }
    public void KillTarget()
    {
        if(enemy_Body.tag == "Bot")
        {
            if (index > enemy_Body.GetComponent<Bot1>().index && enemy_Body.GetComponent<Bot1>().index >= 0)
            {
                Vector3 look = (enemy_Body.transform.position);
                look.y = transform.position.y;

                transform.LookAt(look);

                if (Vector3.Distance(transform.position, enemy_Body.transform.position) >= minDist)
                {

                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    //moveDirection = (enemy_Body.transform.position - transform.position).normalized;
                    //moveDirection.y = 0;

                    if (Vector3.Distance(transform.position, enemy_Body.transform.position) <= maxDist)
                    {
                        StartShooting();
                        //Here Call any function U want Like Shoot at here or something
                    }
                }

            }
        }

        else if (enemy_Body.tag == "Player")
        {
            if (index > enemy_Body.GetComponent<PlayerMove>().listLength && enemy_Body.GetComponent<PlayerMove>().listLength >= 0)
            {
                Vector3 look = (enemy_Body.transform.position);
                look.y = transform.position.y;

                transform.LookAt(look);

                if (Vector3.Distance(transform.position, enemy_Body.transform.position) >= minDist)
                {

                    //moveDirection = (enemy_Body.transform.position - transform.position).normalized;
                    //moveDirection.y = 0;
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, enemy_Body.transform.position) <= maxDist)
                    {
                        StartShooting();
                        //Here Call any function U want Like Shoot at here or something
                    }
                }

            }
        }

    }
    void StartShooting()
    {
        //if (pistol.activeInHierarchy)
        //{
        //    if (shoot_Time >= pistol_Shoot_Delay)
        //    {
        //        shoot_Time = 0;
        //        Shoot_Pistol();
        //    }

        //}

        if (rifle.activeInHierarchy)
        {
            if (shoot_Time >= rifle_Shoot_Delay)
            {
                shoot_Time = 0;
                Shoot_Rifle();
            }

        }
        if (bazooka.activeInHierarchy)
        {
            if (shoot_Time >= bazooka_Shoot_Delay)
            {
                shoot_Time = 0;
                Shoot_Bazooka();
            }

        }
        if (tank.activeInHierarchy)
        {
            if (shoot_Time >= tank_Shoot_Delay)
            {
                shoot_Time = 0;
                Shoot_Tank();
            }

        }
    }

    //void Give_Pistol()
    //{
    //    GameObject effect = Instantiate(evolve_Effect, effect_point.transform.position, Quaternion.identity, effect_point.transform);
    //    pistol.SetActive(true);
    //    isPistol = true;
    //    StartCoroutine(EffectDestroyer(effect));      
    //}
    void Give_Rifle()
    {
        GameObject effect = Instantiate(evolve_Effect, effect_point.transform.position, Quaternion.identity, effect_point.transform);
        //pistol.SetActive(false);
        rifle.SetActive(true);
        isRifle = true;
        StartCoroutine(EffectDestroyer(effect));
    }
    void Give_Bazooka()
    {
        GameObject effect = Instantiate(evolve_Effect, effect_point.transform.position, Quaternion.identity, effect_point.transform);
        rifle.SetActive(false);
        bazooka.SetActive(true);
        isBazooka = true;
        StartCoroutine(EffectDestroyer(effect));
    }
    void Give_Tank()
    {
        GameObject effect = Instantiate(evolve_Effect, effect_point.transform.position, Quaternion.identity, effect_point.transform);
        bazooka.SetActive(false);
        bot_Body.SetActive(false);
        tank.SetActive(true);
        isTank = true;
        StartCoroutine(EffectDestroyer(effect));
    }


    //public void Shoot_Pistol()
    //{
    //    BAnimator.SetBool("Surfing", false);
    //    BAnimator.SetBool("PistolShoot", true);
    //    GameObject effect = Instantiate(pistol_Muzzle, pistol_ShootingPoint.transform.position, pistol_ShootingPoint.transform.rotation);
    //    StartCoroutine(EffectDestroyer(effect));
    //    Instantiate(pistol_Bullet, pistol_ShootingPoint.transform.position, pistol_ShootingPoint.transform.rotation).GetComponent<PistolBullet>().SetTarget(enemy_Body);
    //}
    public void Shoot_Rifle()
    {
        BAnimator.SetBool("Surfing", false);
        BAnimator.SetBool("RifleShoot", true);
        GameObject effect = Instantiate(pistol_Muzzle, rifle_ShootingPoint.transform.position, rifle_ShootingPoint.transform.rotation);
        StartCoroutine(EffectDestroyer(effect));
        Instantiate(pistol_Bullet, rifle_ShootingPoint.transform.position, rifle_ShootingPoint.transform.rotation).GetComponent<PistolBullet>().SetTarget(enemy_Body);
    }
    public void Shoot_Bazooka()
    {
        //AnimBool();
        BAnimator.SetBool("BazookaShoot", true);
        GameObject effect = Instantiate(bazooka_Muzzle, bazooka_ShootingPoint.transform.position, bazooka_ShootingPoint.transform.rotation);
        StartCoroutine(EffectDestroyer(effect));
        Instantiate(bazooka_Bullet, bazooka_ShootingPoint.transform.position, bazooka_ShootingPoint.transform.rotation).GetComponent<BazookaBullet>().SetTarget(enemy_Body);
    }
    public void Shoot_Tank()
    {
        //AnimBool();
        GameObject effect = Instantiate(tank_Muzzle, tank_ShootingPoint.transform.position, tank_ShootingPoint.transform.rotation);
        StartCoroutine(EffectDestroyer(effect));
        Instantiate(tank_Bullet, tank_ShootingPoint.transform.position, tank_ShootingPoint.transform.rotation).GetComponent<TankBullet>().SetTarget(enemy_Body);
    }

    public void ChangeDirection()
    {
        Vector3 newdirection = Random.insideUnitSphere;
        newdirection.y = 0f;
        moveDirection = newdirection.normalized;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Colorless")
        {
            other.gameObject.tag = "Untagged";
            Destroy(other.gameObject);

            transform.position += Vector3.up * 0.35f;
            GameObject n = Instantiate(stackUp_Puff, cubeList[index].transform.position, baseCube.transform.rotation, cubeList[index].transform);
            Destroy(n, 1f);
            GameObject newPlatform = Instantiate(botCubePrefab, cubeList[index].transform.position + new Vector3(0, -0.35f, 0), baseCube.transform.rotation);
            water_Effect.transform.position -= transform.up * 0.35f;
            cubeList.Add(newPlatform);
            newPlatform.name = index.ToString();

            index++;
            cubeList[index].transform.SetParent(parentObj.transform);
            StartCoroutine(jump());
            ChangeDirection();
        }
    }
    private IEnumerator jump()
    {
        BAnimator.SetBool("Surfing", false);
        BAnimator.SetBool("Jump", true);
        yield return null;
        BAnimator.SetBool("Jump", false);
    }

    public void botDestroy()
    {
        //if (this.index < 0)
        //{
        //    EnemyFall();
        //}

        if (this.index == 0)
        {
            GameManager.obj.botCount -= 1;
            cubeList[index].transform.SetParent(null);
            GameObject obj = cubeList[index].gameObject;
            cubeList.RemoveAt(index);
            Destroy(obj);
            --index;
            gameObject.GetComponent<Indicator>().indicatorRt.gameObject.SetActive(false);
            gameObject.GetComponent<Indicator>().enabled = false;
            Destroy(gameObject, 3f);
            AnimBool();
            BAnimator.SetBool("Fall", true);
            moveSpeed = 0f;
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            //StartCoroutine(EnemyFall());
        }
        else if (this.index > 0)
        {
            GameObject obj = cubeList[index].gameObject;
            cubeList[index].transform.SetParent(null);
            cubeList.RemoveAt(index);
            Destroy(obj);
            --index;
            transform.position -= Vector3.up * 0.35f;
            water_Effect.transform.position += transform.up * 0.35f;
            //bot_Body.transform.position += Vector3.up * 0.35f;
            //tank.transform.position += Vector3.up * 0.35f;
            //foreach (GameObject go in cubeList)
            //{
            //    go.transform.position += new Vector3(0, 0.35f, 0);
            //}

            //Debug.Log(gameObject.name + " = LAST ONE");
            //moveDirection = -moveDirection;
        }
    }

    //IEnumerator EnemyFall()
    //{

    //    transform.position += Vector3.down * 0.2f;
    //    yield return new WaitForSeconds(0.1f);
    //    StartCoroutine(EnemyFall());        
    //}
    IEnumerator EffectDestroyer(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(obj);
    }
    void AnimBool()
    {
        foreach (AnimatorControllerParameter parameter in BAnimator.parameters)
        {
            BAnimator.SetBool(parameter.name, false);
        }

    }
    public void MakeSlow(float time)
    {
        moveSpeed = sp / 2;
        StartCoroutine(SlowSpeed(time));
    }
    public IEnumerator SlowSpeed(float t)
    {
        yield return new WaitForSeconds(t);
        moveSpeed = sp;
    }
    //private void OnDrawGizmos()
    //{
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireSphere(origin, radius);
    //    ////blue
    //    Gizmos.color = Color.blue;
    //    Debug.DrawLine(origin_S, origin_S + direction_S * max_Distance);
    //    Gizmos.DrawWireSphere(origin_S + direction_S * max_Distance, outerRadius);
    //}



}
