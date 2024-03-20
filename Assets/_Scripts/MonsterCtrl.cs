using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    NormalMon = 0,
    EliteMon,
    BossMon,
}

public class MonsterCtrl : MonoBehaviour
{
    protected MonsterType monType = MonsterType.NormalMon;

    //이동 관련
    float moveSpeed = 1.0f;
    Vector3 moveDir = Vector3.zero;
    SpriteRenderer spRenderer = null;
    //이동 관련

    //넉백 관련 
    bool isKnockBack = false;
    float kbDist = -2.0f;
    float kbSpeed = 3.0f;
    float kbTimer = 0.0f;
    Vector3 kbTarget = Vector3.zero;
    //넉백 관련 

    //능력치 관련
    protected float curHp = 100;
    float maxHp = 100;
    //float defense = 10;
    //float attack = 10;
    float dftDmg = 30;
    float expVal = 0;
    //능력치 관련

    //UI 관련
    Vector3 dmgTxtOffset = new Vector3(0, 0.5f, 0);
    //UI 관련

    void Awake()
    {
        spRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void OnEnable()
    {
        curHp = maxHp;
        SetExp();
    }

    void Start() { }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("P_Bullet"))
        {
            TakeDamage(dftDmg);
            coll.gameObject.SetActive(false);
        }
        else if (coll.CompareTag("Player"))
        {
            float dmg = 10;
            if (monType == MonsterType.EliteMon)
                dmg = 20;

            GameMgr.Inst.player.TakeDamage(dmg);
        }
        else if (coll.CompareTag("Guard"))
        {
            isKnockBack = true;
            kbTarget = transform.position + moveDir * kbDist;
            TakeDamage(10);
        }
        else if (coll.CompareTag("Drill"))
        {
            TakeDamage(dftDmg);
        }
    }

    void SetExp() //TODO : Init()만들어서 monType으로 나뉘는 변수들 한번에 초기화 하기
    {
        expVal = 10;
        if (monType == MonsterType.EliteMon)
            expVal = 20;
    }

    protected void Move()
    {
        if (!isKnockBack)
        {
            moveDir = GameMgr.Inst.player.transform.position - transform.position;
            moveDir.Normalize();

            if (moveDir.x < 0)
                spRenderer.flipX = false;
            else
                spRenderer.flipX = true;

            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            kbTimer += kbSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, kbTarget, kbTimer);

            if (1.0f <= kbTimer)
            {
                isKnockBack = false;
                kbTimer = 0.0f;
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (curHp <= 0) return; //이미 0이하인 경우에도 들어오는 경우가 있어서 추가함.
        //boss여서 그런가?

        //1. Hp변수 깎기
        float dmgTxt = curHp < damage ? curHp : damage;
        curHp -= damage;
        //2. Dmg Txt 띄우기 
        GameMgr.Inst.SpawnDmgTxt(transform.position + dmgTxtOffset, dmgTxt, Color.red);

        if (curHp <= 0) MonsterDie();
    }

    void MonsterDie()
    {
        //Boss 아닐때만 Die()
        if (monType == MonsterType.BossMon) return;

        MemoryPoolMgr.Inst.ActiveMonsterCount--;
        GameMgr.Inst.KillTxtUpdate(); //킬수 올리기
        GameMgr.Inst.AddExpVal(expVal); //경험치 올리기
        ItemMgr.Inst.SpawnGold(transform.position, monType); //골드 스폰

        gameObject.SetActive(false);
    }
}