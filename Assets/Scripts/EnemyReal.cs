using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReal : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform player;
    Animator animator;
    public bool isAttackCheck = false;
    public int hp = 2;
    bool isStop = false;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navMeshAgent.destination = player.position;
    }
    private void Update()
    {
        if(!navMeshAgent.isStopped)
        {
            if(Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
            {
                if (!isStop)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isWalk", false);
                    isAttackCheck = true;
                    StartCoroutine("Attack");
                }
            }
            else
            {
                navMeshAgent.isStopped = false;
                animator.SetBool("isWalk", true);
                navMeshAgent.destination = player.position;
            }
        }
        if (!isStop)
        {
            this.transform.LookAt(player.position);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            if(!isStop)
            {
                isAttackCheck=true;
                StartCoroutine("Attack");
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }
    public void SetHp(int damage)
    {
        if (!isStop)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                Debug.Log("die");
                animator.SetTrigger("Death");
                isAttackCheck = false;
                isStop = true;
            }
        }
    }
}
