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
    Renderer[] renderers;
    Color originColor;
    public float navDistance = 15.0f;

    public float spawnItemPossible = 20.0f;
    public GameObject prefabItem;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navMeshAgent.destination = player.position;
        renderers = this.GetComponentsInChildren<Renderer>();
        originColor = renderers[0].material.color;
    }
    private void Update()
    {
        if (isStop || Vector3.Distance(this.transform.position, player.position) > navDistance)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (Vector3.Distance(this.transform.position, player.position) < navMeshAgent.stoppingDistance + 0.1f)
        {
            navMeshAgent.isStopped = true;
            StartCoroutine("Attack");
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = player.position;
        }
        this.transform.LookAt(player.position);

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
                navMeshAgent.isStopped = true;
                DropItem();
                Destroy(this.gameObject);
            }
            else
            {
                StartCoroutine("HitColor");
            }
        }
    }

    void DropItem()
    {
        Instantiate(prefabItem, this.transform.position, this.transform.rotation);
    }

    IEnumerator HitColor()
    {
        foreach (Renderer render in renderers)
        {
            render.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Renderer render in renderers)
        {
            render.material.color = originColor;
        }
    }
}
