using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public ItemType itemType = ItemType.Gold;

    float goldVal = 0.0f;
    public float GoldVal
    {
        get { return goldVal; }
        set
        {
            if (itemType == ItemType.Gold)
                goldVal = value;
        }
    }

    float healRate = 0.0f;
    public float HealRate
    {
        get { return HealRate; }
        set
        {
            if (itemType == ItemType.Heal)
                healRate = value;
        }
    }

    float bombRadius = 0.0f;

    void Start()
    {
        bombRadius = (ScreenMgr.InitScMax.x - ScreenMgr.InitScMin.x) / 2.0f;

        Destroy(gameObject, 10.0f); //TODO : 나중에는 안먹으면 안사라지게 할거임
    }

    //void Update() { }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (itemType == ItemType.Gold)
            {
                GameMgr.Inst.AddGold(goldVal);
            }
            else if (itemType == ItemType.Heal)
            {
                PlayerCtrl player = GameMgr.Inst.player.GetComponent<PlayerCtrl>();
                player.GetHp(healRate);
            }
            else if (itemType == ItemType.Bomb)
            {
                Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, bombRadius);

                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].tag.Contains("Monster"))
                    {
                        MonsterCtrl monCtrl = colls[i].gameObject.GetComponent<MonsterCtrl>();
                        monCtrl.TakeDamage(1000); //TODO : Bomb 데미지 정하기
                    }
                }

                //섬광 효과. 코루틴 있어서 ItemMgr에 구현
                ItemMgr.Inst.FlashEffect(); 
            }

            Destroy(gameObject);
        }
    }
}