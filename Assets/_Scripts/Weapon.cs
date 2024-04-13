using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    protected const int MaxLevel = 3;

    protected bool isEvolve = false;
    protected int curLevel = 0;
    public int CurLv { get { return curLevel; } }

    abstract public void LevelUpWeapon();
    abstract public void EvolveWeapon();
}