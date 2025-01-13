using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public EnemyReal enemy;
    public Player player;
    public int damage;

    private void Start()
    {
        
    }

    public enum Char
    {
        Player,
        Enemy,
    }
    public Char character;

    private void OnTriggerEnter(Collider other)
    {
        switch (character)
        {
            case Char.Player:
                if (other.CompareTag("Enemy")){
                    if (player.isAttackCheck)
                    {
                        Debug.Log(other.tag);
                        player.isAttackCheck = false;
                        enemy.SetHp(damage);
                    }
                }
                break;
            case Char.Enemy:
                if (other.CompareTag("Player"))
                {
                    Player player = other.GetComponent<Player>();
                    if (enemy.isAttackCheck)
                    {
                        Debug.Log(other.tag);
                        enemy.isAttackCheck = false;
                        player.SetHp(damage);
                    }
                }
                break;
        }
    }
}
