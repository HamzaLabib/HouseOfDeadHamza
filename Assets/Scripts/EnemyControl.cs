using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public GameObject targetPlayer;
    NavMeshAgent agent;
    float offset = 2f;
    //public PlayerMovement isAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
