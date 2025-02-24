using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 2.0f;
    Animator animator;
    bool isWalk = false;
    public bool isAttackCheck = false;
    public int hp = 2;
    bool isStop = false;
    public GameObject PrefabBullet;
    public Transform BulletPoint;

    public float BulletDelay = 1.0f;
    public float BulletTime = 0f;
    bool isBullet = false;
    bool isLive = true;
    bool isItemSpeed = false;
    float itemSpeed = 10.0f;
    int maxHp = 10;
    float ItemSpeedTimeSpan;
    float itemSpeedTimer;
    float baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            Walk();
            Attack();
            Rotation();
            ItemSpeedTimer();
        }
    }

    void Walk()
    {
        isWalk = false;

        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-this.transform.forward * Time.deltaTime * speed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterController.Move(-this.transform.right * Time.deltaTime * speed);
            isWalk = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            characterController.Move(this.transform.right * Time.deltaTime * speed);
            isWalk = true;
        }

        if (isWalk)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

    }

    public void SetHp(int damage)
    {
        if (!isStop)
        {
            hp -= damage;
            if(hp <= 0)
            {
                hp = 0;
                Debug.Log("GameOver");
                animator.SetTrigger("Death");
                isAttackCheck = false;
                isLive = false;
                isStop = true;
            }
        }
    }

    void Attack()
    {
        if (isBullet)
        {
            BulletTime += Time.deltaTime;
            if (BulletTime >= BulletDelay)
            {
                isBullet = false;
                BulletTime = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isBullet = true;
                animator.SetTrigger("isShoot");
                Invoke("SpawnBullet", 0.2f);
            }

        }
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    animator.SetTrigger("isAttack");
        //}
    }

    void SpawnBullet()
    {
        Instantiate(PrefabBullet, BulletPoint.position, this.transform.rotation);
    }

    void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        if(plane.Raycast(ray, out rayLength))
        {
            Vector3 mousePoint = ray.GetPoint(rayLength);

            this.transform.LookAt(new Vector3(mousePoint.x, this.transform.position.y, mousePoint.z));
        }
    }

    public void ItemPotion()
    {
        hp = maxHp;
    }

    public void ItemSpeed()
    {
        speed = itemSpeed;
        isItemSpeed = true;
    }

    void SetBaseSpeed()
    {
        speed = baseSpeed;
    }

    public void ItemSpeedTimer()
    {
        if(isItemSpeed)
        {
            ItemSpeedTimeSpan += Time.deltaTime;
            if(ItemSpeedTimeSpan >= itemSpeedTimer)
            {
                isItemSpeed = false;
                ItemSpeedTimeSpan = 0;
            }
        }
    }

    public void GameOver()
    {
        Manager.Instance.PauseGame();
    }
}