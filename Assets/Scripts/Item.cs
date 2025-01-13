using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemInfo
{
    Potion,
    Speed,
}

public class Item : MonoBehaviour
{
    ItemInfo itemInfo;

    private void Start()
    {
        SetItem();
    }

    void SetItem()
    {
        itemInfo = (ItemInfo)Random.Range(0, (int)ItemInfo.Speed + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddBuff(other.GetComponent<Player>());
    }

    void AddBuff(Player player)
    {
        switch (itemInfo)
        {
            case ItemInfo.Potion:
                player.ItemPotion();
                Destroy(this.gameObject);
                break;

            case ItemInfo.Speed:
                player.ItemSpeed(); 
                Destroy(this.gameObject);
                break;
        }
    }
}
