using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Core core;//게임 승리조건인 코어는 게임매니저의 역할이라고 생각되어 따로 빼주었다.
    public GameObject playerPrefab;
    public Player player;
    public CustomCursor mouseCursor;
    public Vector3 playerSpownPos=new Vector3(0, -3.2f, 0);
    //세이브에 쓰이는 변수
    [HideInInspector]
    public GameObject platformData;//CustomCursor스크립트의 tryInstall함수에서 플랫폼을 인스턴스화할때 부모개체로 사용
    //라운드 시스템 구현에 쓰이는 변수들
    public EnemySpowner enemySpownerPrefab;
    public Vector3 enemySpownrPos;
    public int nowRound = 0;
    public int nowDeadEnemyCount = 0;
    [Header("라운드가 바뀔때 몇초 대기할지")]
    public int roundChangeIntervalTime = 10;

    public enum GameState {
        //라운드 대기상태, 메인화면일때, 게임을 클리어했을때... 등등일때의 상태이다. 플랫폼 설치조건에 이 상태가 쓰인다
        ROUNDNOTPLAYING,
        //라운드 진행중일때의 상태이다. 라운드에 따른 몬스터를 스폰할때 이 상태가 쓰인다.
        ROUNDPLAYING,
    }

    //라운드 시스템을 구현할떄 사용되는 변수이다.
    public GameState gameState = GameState.ROUNDNOTPLAYING;
    [Header("초기 자본을 얼마로 할지 설정. 게임 재시작에도 영향을 주는 변수")]
    public int goldStartValue = 30;
    public int Gold {
        get {

            return gold;
        }
        set {
            gold = value;
            UIManager.Instance.OnGoldChanged();
        }
    }
    private int gold;

    public static GameManager Instance {
        get {
            return instance;
        }
    }

    private static GameManager instance;
    private bool canPausePanelActive = true;
    private IEnumerator DefenseStartCoroutine;
    [SerializeField]
    private EnemySpowner enemySpowner;
    //**************************************************************
    private void Awake()
    {
        if (GameManager.Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().name == "00_MainScreen") {
            return;
        }
            if (GameObject.FindGameObjectWithTag("Player") == null)
            player = Instantiate(playerPrefab, playerSpownPos, Quaternion.identity).GetComponent<Player>();
        else
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Start()
    {
        //메인화면에서 생성된 플레이어가 움직이지 못하게 한다.
        Time.timeScale = 0;
        DefenseStartCoroutine = DefenseStart();
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            //canPausePanelActive의 참/거짓에 따라 GamePause()내부에서 패널의 활성화/비활성화가 이뤄진다
            GamePause();
        }
    }

    public void MainPanel_GameStartBtn() {
        StartCoroutine(ResetScene());
        
    }

    IEnumerator ResetScene()
    {
        //AsyncOperation op = SceneManager.LoadSceneAsync("01_GamePlaying", LoadSceneMode.Single);

        AsyncOperation op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {

            if (op.progress >= 0.9f)
            {

                op.allowSceneActivation = true;
            }
            yield return null;
        }

        thisDataReset();
        enemySpowner=Instantiate(enemySpownerPrefab, enemySpownrPos, Quaternion.identity);
        StartCoroutine(UIManager.Instance.TargetTextFadeOutWithTime(UIManager.Instance.text_RoundNotify,
           "정비 시간", 1.5f, 1));
        yield break;
    }
    public void PlayPanel_GameStartBtn() {
        gameState = GameState.ROUNDPLAYING;

        //씬 변경 함수를 이 함수에 삽입해야한다
        //Debug.Log("디펜스 시작!!");
        StartCoroutine(DefenseStartCoroutine);
    }
    IEnumerator DefenseStart() {
        
        roundStart();
        //라운드 텍스트가 사라질때까지 기다린다(3초)
        yield return new WaitForSeconds(3f);
        //Enemy 스폰 시작
        yield return StartCoroutine(enemySpowner.GetComponent<EnemySpowner>().SpownEnemyWithRound(nowRound));
        //yield return StartCoroutine(enemySpowner.GetComponent<EnemySpowner>().SpownEnemyWithRound(nowRound));

        //모든 적이 죽을 때까지 기다리고, 전부 죽었다면 다음라운드까지 대기시간을 적용하고, 라운드를 넘긴다
        while (true)
        {
            //모든 적들이 죽었다면
            if (nowDeadEnemyCount >= enemySpowner.roundEnemyInfo[nowRound].spownEnemyPrefabs.Length)
            {
                //다음 라운드에 스폰할 적이 없다면(게임클리어)
                if (enemySpowner.roundEnemyInfo.Length <= nowRound + 1)
                {
                    break;
                }

                roundEnd();
                
                // 다음라운드 시작까지 대기시간을 적용
                yield return new WaitForSeconds(roundChangeIntervalTime);

                // 다음라운드 시작
                nowRound += 1;
                nowDeadEnemyCount = 0;

                roundStart();
                //라운드 텍스트가 사라질때까지 기다린다(3초)
                yield return new WaitForSeconds(3f);

                yield return StartCoroutine(enemySpowner.GetComponent<EnemySpowner>().SpownEnemyWithRound(nowRound));
            }
            
            yield return null;
        }
        //여기에 게임클리어 함수
        Debug.Log("GameClear!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        UIManager.Instance.panel_GameClear.SetActive(true);
        Time.timeScale = 0;
        yield break;
    }
    private void roundStart() {
        //라운드 텍스트를 띄운다
        StartCoroutine(UIManager.Instance.TargetTextFadeOutWithTime(UIManager.Instance.text_RoundNotify,
            "라운드 " + (nowRound + 1).ToString(), 1.5f, 1));
        // 라운드 플레이중이므로 gameState변수 상태를 바꾼다.
        gameState = GameState.ROUNDPLAYING;
        //오브젝트샵을 열어둔것을 강제종료
        UIManager.Instance.button_ONObjectShop.interactable = false;
        UIManager.Instance.button_ONObjectShop.gameObject.SetActive(true);
        UIManager.Instance.button_OFFObjectShop.gameObject.SetActive(false);
        UIManager.Instance.panel_ObjectShop.gameObject.SetActive(false);
        //플랫폼을 설치하지 못하게 한다
        StartCoroutine(mouseCursor.State_Idle());
    }
    private void roundEnd() {
        //정비 시간 텍스트를 띄운다
        StartCoroutine(UIManager.Instance.TargetTextFadeOutWithTime(UIManager.Instance.text_RoundNotify,
           "정비 시간", 1.5f, 1));
        // 라운드 플레이중이 아니므로 gameState변수 상태를 바꾼다.
        gameState = GameState.ROUNDNOTPLAYING;
        UIManager.Instance.button_ONObjectShop.interactable = true;
        UIManager.Instance.button_ONObjectShop.gameObject.SetActive(true);
        
    }
    
    public void GamePause()
    {
        canPausePanelActive = !canPausePanelActive;
        //canPausePanelActive변수가 먼저 반대로 되기 때문에 이 조건문도 반대로 조건을 설정한다.
        if (canPausePanelActive == false)
        {
            Time.timeScale = 0;
            UIManager.Instance.panel_Play_Pause.SetActive(true);
        }
        else if (canPausePanelActive == true)
        {
            Time.timeScale = 1;
            UIManager.Instance.panel_Play_Pause.SetActive(false);
        }
    }

    public void GameReset() {
        player.Respown();

        StartCoroutine(ResetScene());
        DefenseStartCoroutine=DefenseStart(); //코루틴의 진행 상황을 리셋한다
        StopCoroutine(DefenseStartCoroutine);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        UIManager.Instance.button_Play_GameStart.gameObject.SetActive(true);
        UIManager.Instance.button_ONObjectShop.interactable = true;
        UIManager.Instance.button_ONObjectShop.gameObject.SetActive(true);
        UIManager.Instance.panel_GameOver.SetActive(false);
        if (canPausePanelActive == false)
            GamePause();
        
    }

    void thisDataReset() {
        Time.timeScale = 1;
        nowRound = 0;
        nowDeadEnemyCount = 0;
        Gold = goldStartValue;
        gameState = GameState.ROUNDNOTPLAYING;
        Destroy(enemySpowner);
        StopCoroutine(DefenseStartCoroutine);
        //StopCoroutine(enemySpownCoroutine);
        StopAllCoroutines();
    }
    void loadSaveData()
    {
        //nowSaveFileSelectNum
    }

   
}
