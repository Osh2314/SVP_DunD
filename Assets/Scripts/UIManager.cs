using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panel_Play_Pause;
    public Slider slider_PlayerHp;
    public Slider slider_CoreHp;
    public Text text_GoldValue;
    public Text text_RoundNotify;
    public Button button_ONObjectShop;
    public Button button_OFFObjectShop;
    public GameObject panel_ObjectShop;
    public GameObject panel_GameClear;
    public GameObject panel_GameOver;
    public Button button_Play_GameStart;
    public Text text_SelectObjNameValue;
    public Text text_SelectObjPriceValue;
    public Image image_ObjectSprite;
    public Text text_ObjToolTipValue;

    public int nowSaveFileSelectNum;
    public struct NowSelectObjInfo {
        public GameObject nowSelectObj;
        public string nowSelectObjName;
        public int nowSelectObjPrice;
        public string nowSelectObjToolTip;
    }

    public NowSelectObjInfo nowSelectObjInfo;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static UIManager instance;
    
    private void Awake()
    {
        if (UIManager.Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //메인화면에서 쓸 함수들*****************************************
    public void SetSaveFileNum(int value=0)
    {
        nowSaveFileSelectNum = value;
        if (value == 0)
            Debug.Log("@@@@@@@@nowSaveFIleSelectNum이 0입니다");
    }

    public void LoadInGameScene() {

    }
    
    public void GameExit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();//프로그램 종료
#endif
    }
    //***************************************************************
    //플레이 화면에서 쓰일 함수들**************************************
    public void MainPanel_GameStartBtn() {

        GameManager.Instance.MainPanel_GameStartBtn();
    }
    public void PlayPanel_GameStartBtn()
    {
        GameManager.Instance.PlayPanel_GameStartBtn();
    }
    public void OnGoldChanged()
    {
        text_GoldValue.text = GameManager.Instance.Gold.ToString();
    }

    public void Btn_GameReStart() {
        GameManager.Instance.GameReset();
    }

    public void SetSelectObjInfo(GameObject selectObj, string selectObjName, int selectObjPrice,
        string selectObjToolTip) {
        //선택오브젝트 정보 구조체를 업데이트한다.
        nowSelectObjInfo.nowSelectObj = selectObj;
        nowSelectObjInfo.nowSelectObjName = selectObjName;
        nowSelectObjInfo.nowSelectObjPrice = selectObjPrice;
        nowSelectObjInfo.nowSelectObjToolTip = selectObjToolTip;

        //선택오브젝트 정보 패널을 업데이트한다.
        text_SelectObjNameValue.text = selectObjName;
        text_SelectObjPriceValue.text = selectObjPrice.ToString();
        image_ObjectSprite.sprite = selectObj.GetComponent<SpriteRenderer>().sprite;
        text_ObjToolTipValue.text = selectObjToolTip;
    }

    /// <summary>
    /// 해당 targetUIText의 text를 str변수의 값으로 바꾸고 시간 변수에 따라 targetUIText가 보이지 않게된다
    /// </summary>
    /// <param name="targetUIText"></param>
    /// <param name="str"></param>
    /// <param name="appearDuration"></param>
    /// <param name="fadeDuration"></param>
    /// <returns></returns>
    public IEnumerator TargetTextFadeOutWithTime(Text targetUIText,string str, float appearDuration,float fadeOutDuration)
    {
        targetUIText.text = str;
        targetUIText.color = new Color(targetUIText.color.r, targetUIText.color.g, targetUIText.color.b, 1);
        yield return new WaitForSeconds(appearDuration);

        Color fadecolor = targetUIText.color;
        float time = 0f;
        //알파값
        fadecolor.a = Mathf.Lerp(1, 0, time);

        while (fadecolor.a > 0f)
        {

            time += Time.deltaTime / fadeOutDuration;

            fadecolor.a = Mathf.Lerp(1, 0, time);

            targetUIText.color = fadecolor;

            yield return null;

        }

        yield break;
    }
    //***************************************************************
}
