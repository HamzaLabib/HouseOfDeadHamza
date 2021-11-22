using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    Transform targetPlayer;
    NavMeshAgent agent;
    float offset = 2f;
    //public PlayerMovement isAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPlayer = GameObject.Find("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        agent.SetDestination(new Vector3(targetPlayer.transform.position.x + offset, targetPlayer.transform.position.y, targetPlayer.transform.position.z + offset));
    }
}
