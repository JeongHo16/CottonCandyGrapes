using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMonCtrl : MonsterCtrl
{
    const float OffsetX = 0.08f;
    float landMonHp = 2000.0f;

    void OnEnable()
    {
        slowTimer = 0.0f;
        curHp = landMonHp;
        base.Init();
    }

    void Start() { monType = MonsterType.EliteMon; }

    void FixedUpdate()
    {
        base.Move();
        SetCollPos();
    }

    void SetCollPos()
    {
        Vector2 tmp = Vector2.zero;
        tmp.x = moveDir.x < 0 ? -OffsetX : OffsetX;
        coll.offset = tmp;
    }
}