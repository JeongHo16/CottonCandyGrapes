using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBiteMonCtrl : MonsterCtrl
{
    float SkullHp = 2500.0f;

    void OnEnable()
    {
        slowTimer = 0.0f;
        curHp = SkullHp;
        base.Init();
    }

    void Start() { monType = MonsterType.EliteMon; }

    void FixedUpdate()
    {
        base.Move();
        RotateMonster();
    }
}