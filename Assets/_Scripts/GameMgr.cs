using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public PlayerCtrl player = null;

    //게임 시간 관련 변수
    public Text Time_Txt = null;
    float curTime = 0.0f;
    float minTime = 60.0f;
    int min = 0;
    int sec = 0;
    //float endTime = 90.0f;
    float endTime = float.MaxValue; //Test 용. //TODO : 적정 시간 찾기
    //게임 시간 관련 변수

    //몬스터 킬수 표시
    public Text Kill_Txt = null;
    int killCount = 0;
    //몬스터 킬수 표시

    //Gold 관련
    public Text Gold_Txt = null;
    public GameObject GoldPrefab = null;
    float inGameGold = 0.0f;
    //Gold 관련

    //Exp 관련
    //public Text CurExpLevel_Txt = null; //inGameExp test 용
    public Text ExpLevel_Txt = null;
    public Image ExpBar_Img = null;
    float inGameExp = 0.0f;
    float[] expLevelArr = { 0.0f, 30.0f, 60.0f, 100.0f, 150.0f, 210.0f, 280.0f, 360.0f, 450.0f, 550.0f }; //TODO : 만랩 늘리면 수식으로 바꾸기
    int inGameLevel = 1;
    int maxLevel = 10; // 현재 만랩 10 //TODO : 만랩 늘리기. 
    //Exp 관련

    //데미지 표시
    public Canvas SubCanvas = null;
    public GameObject DmgTxtPrefab = null;
    //데미지 표시

    public static GameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        curTime = 0.0f;

        Time.timeScale = 1.0f;
    }

    void Update()
    {
        UpdateGameTime();
    }

    void UpdateGameTime()
    {
        curTime += Time.deltaTime;

        min = (int)(curTime / minTime);
        sec = (int)(curTime - min * minTime);

        Time_Txt.text = string.Format("{0:D2}:{1:D2}", min, sec);

        if (endTime <= curTime)
        {
            GameOver();
            return;
        }
    }

    public void SpawnGold(Vector3 pos, MonsterType monType)
    {
        GameObject gold = Instantiate(GoldPrefab);
        gold.transform.position = pos;

        ItemCtrl item = gold.GetComponent<ItemCtrl>();
        item.GoldVal = 10;
        if (monType == MonsterType.EliteMon)
            item.GoldVal = 50;
        else if (monType == MonsterType.BossMon)
            item.GoldVal = 100;
    }

    public void AddGold(float val)
    {
        inGameGold += val;
        Gold_Txt.text = inGameGold.ToString();
    }

    public void KillTxtUpdate()
    {
        killCount++;
        if (Kill_Txt != null)
            Kill_Txt.text = killCount.ToString();
    }

    public void SpawnDmgTxt(Vector3 pos, float damage, Color txt_color)
    {
        GameObject dmgObj = Instantiate(DmgTxtPrefab, SubCanvas.transform);
        DmgTxtCtrl dmgTxtCtrl = dmgObj.GetComponent<DmgTxtCtrl>();
        dmgTxtCtrl.Init(pos, damage, txt_color);
    }

    public void AddExpVal(float eVal)
    {
        inGameExp += eVal;
        for (int i = 0; i < expLevelArr.Length; i++)
        {
            if (expLevelArr[i] <= inGameExp)
                inGameLevel = i + 1;
            else
                break;
        }

        //CurExpLevel_Txt.text = inGameExp.ToString(); //inGameExp Test용
        ExpLevel_Txt.text = inGameLevel.ToString();

        if (inGameLevel >= maxLevel)
            ExpBar_Img.fillAmount = 1;
        else
        {
            ExpBar_Img.fillAmount = (inGameExp - expLevelArr[inGameLevel - 1]) /
                (expLevelArr[inGameLevel] - expLevelArr[inGameLevel - 1]);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0.0f;
    }
}