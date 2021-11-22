using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject checkPoint;
    public float forwardSpeed = 0.2f;
    public float sideSpeed = 1f;
    float stopPeriod = 10f;

    [HideInInspector]
    public bool isOnCheckPoint = false;

    void Update()
    {
        Movement();
        IsReadyToDefence();
        if (isOnCheckPoint)
            NextCheckPoint();
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

    public void IsReadyToDefence()
    {
        if (checkPoint.transform.position.x - transform.position.x < 0.01f)
            isOnCheckPoint = true;
        else
            isOnCheckPoint = false;
    }

    void NextCheckPoint()
    {
        stopPeriod -= Time.deltaTime;
        if (stopPeriod < 0)
        {
            checkPoint.transform.position = new Vector3(Random.Range(-25, 25), 1, Random.Range(-25, 25));
            stopPeriod = 10f;
        }
    }
}
