using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpLowUIMgr : MonoBehaviour
{
    public Sprite Pressed_Sprite = null;

    Dictionary<string, Button> sceneBtnDic;
    string SceneName = "";

    [Header("------ Top UI ------")]
    public Text Nickname_Txt = null;
    public Text Heart_Txt = null;
    public Text Gold_Txt = null;
    public Text Level_Txt = null;

    [Header("------ Bottom UI ------")]
    public Button Store_Btn = null;
    public Button Inventory_Btn = null;
    public Button Battle_Btn = null;
    public Button Evolve_Btn = null;

    void Awake()
    {
        sceneBtnDic = new Dictionary<string, Button> {
            { "Store", Store_Btn },
            { "Inventory", Inventory_Btn },
            { "Battle", Battle_Btn },
            { "Evolve", Evolve_Btn },
        };
    }

    void Start()
    {
        if (Store_Btn)
            Store_Btn.onClick.AddListener(StoreBtnClick);

        if (Inventory_Btn)
            Inventory_Btn.onClick.AddListener(InventoryBtnClick);

        if (Battle_Btn)
            Battle_Btn.onClick.AddListener(BattleBtnClick);

        if (Evolve_Btn)
            Evolve_Btn.onClick.AddListener(EvolveBtnClick);

        SceneName = SceneManager.GetSceneAt(1).name;
        if (SceneName != "" && SceneName != "UpLowUI")
            sceneBtnDic[SceneName].image.sprite = Pressed_Sprite; //현재 씬 버튼 표시
    }

    void StoreBtnClick()
    {
        SceneManager.LoadScene("UpLowUI");
        SceneManager.LoadScene("Store", LoadSceneMode.Additive);
    }

    void InventoryBtnClick()
    {
        SceneManager.LoadScene("UpLowUI");
        SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    }

    void BattleBtnClick()
    {
        SceneManager.LoadScene("UpLowUI");
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
    }

    void EvolveBtnClick()
    {
        SceneManager.LoadScene("UpLowUI");
        SceneManager.LoadScene("Evolve", LoadSceneMode.Additive);
    }
}