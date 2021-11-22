using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject checkPoint;
    public GameObject enemy;
    GameObject currentEnemy;
    public float forwardSpeed = 0.2f;
    public float sideSpeed = 1f;
    public AudioSource shotGun;
    public LineRenderer lineToTarget;

    Transform onTarget;
    [HideInInspector]
    bool isEnemyDead = false;
    float delayMoving = 2f;

    private void Awake()
    {
        currentEnemy = GameObject.Instantiate(enemy);
        currentEnemy.transform.position = new Vector3(Random.Range(-25, 25), 1, Random.Range(-25, 25));
    }
    void Update()
    {
        MouseMovement();
        Movement();
        GetControllers();
        if (isEnemyDead)
            NextCheckPoint();
    }

    void GetControllers()
    {
        if (Input.GetMouseButtonDown(0))
            KillEnemy();
    }

    void Movement()
    {
        RaycastHit raycastHit = new RaycastHit();
        Ray raycast;

        Vector3 adjustDir;
        Vector3 direction = checkPoint.transform.position - transform.position;


        raycast = new Ray(transform.position, direction);
        if (Physics.Raycast(raycast, out raycastHit, 5f))
        {
            if (raycastHit.collider.tag == "walls")
            {
                adjustDir = raycastHit.collider.transform.right;
                transform.Translate(adjustDir * sideSpeed * Time.deltaTime);
                Debug.DrawLine(transform.position, raycastHit.transform.position, Color.red);
            }
        }
        if (!Physics.Raycast(raycast, out raycastHit, 5f))
            transform.Translate(direction * forwardSpeed * Time.deltaTime);
    }

    /*public void IsReadyToDefence()
    {
        if (checkPoint.transform.position.x - transform.position.x < 0.01f)
            ;
        else
            
    }*/

    void NextCheckPoint()
    {
        delayMoving -= Time.deltaTime;
        if (delayMoving < 0)
        {
            checkPoint.transform.position = new Vector3(Random.Range(-25, 25), 1, Random.Range(-25, 25));
            isEnemyDead = false;
            currentEnemy = GameObject.Instantiate(enemy);
            currentEnemy.transform.position = new Vector3(Random.Range(-25, 25), 1, Random.Range(-25, 25));
        }
    }

    void KillEnemy()
    {
        shotGun.Play();
        if (onTarget.tag == currentEnemy.tag)
        {
            isEnemyDead = true;
            delayMoving = 2f;
            Destroy(currentEnemy);
        }
    }

    void MouseMovement()
    {
        RaycastHit raycastHit = new RaycastHit();
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        lineToTarget.SetPosition(0, this.transform.position);
        
        if (Physics.Raycast(raycast, out raycastHit, 10f))
        {
            onTarget = raycastHit.transform;
            lineToTarget.SetPosition(1, raycastHit.transform.position);
        }
    }
}
