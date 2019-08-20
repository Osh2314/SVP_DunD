using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float origin_JumpForce;
    public bool cJump = true;
    public GameObject hand;
    public enum STATE { IDLE, MOVE, STOP, ATTACK, DEAD };
    public STATE state = STATE.IDLE;

    public int maxHp = 30;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
	        if(hp>value){
                StartCoroutine(Camera.main.GetComponent<CameraShake>().CameraVibration(0.3f, 3f, 1f));
	         Debug.Log("플레이어 피격에 의한 카메라 진동");
	        }

            hp = value;
            UIManager.Instance.slider_PlayerHp.value = hp;
            if (Hp <= 0)
            {
                event_Death();
            }
        }
    }

    private int hp=30;

    //현재 셀렉중인 블록의 가격
    private bool isSeeRight = true;
    private float jumpForce;

    private Rigidbody2D rigid;
    private Animator anim;
    private Vector3 spownPos;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spownPos=transform.position;
        Respown();
        UIManager.Instance.slider_PlayerHp.maxValue = hp;
        UIManager.Instance.slider_PlayerHp.value = hp;
        StartCoroutine(STATE_IDLE());
        
    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        if (h != 0)
            StartCoroutine(STATE_MOVE());
        else if (h == 0)
            StartCoroutine(STATE_IDLE());
        transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);

        Vector3 mouseConvertedpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        Vector2 lookDir = mouseConvertedpoint - hand.transform.position;
        lookDir.Normalize();
        hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, lookDir);
        hand.transform.localEulerAngles += new Vector3(0, 0, -90);

        if (Input.GetKeyDown(KeyCode.Space) && cJump == true)
        {
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            cJump = false;
        }
        if (transform.position.x<GameManager.Instance.mouseCursor.transform.position.x) // 1이 오른쪽 -1이 왼쪽
        {
            isSeeRight = true;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.x > GameManager.Instance.mouseCursor.transform.position.x)
        {
            isSeeRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (GameManager.Instance.mouseCursor.state == CustomCursor.State.Install)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //설치를 구현하는 함수
                GameManager.Instance.mouseCursor.tryInstallObj();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine(GameManager.Instance.mouseCursor.State_Idle());
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            cJump = true;
            jumpForce = origin_JumpForce;
        }
        if (collision.gameObject.tag == "Floor")
        {
            cJump = true;
            jumpForce = origin_JumpForce * 1.5f;
        }
    }

    public bool GetDirection() // Direction == 방향
    {
        return isSeeRight;
    }

    

    IEnumerator STATE_IDLE()
    {
        state = STATE.IDLE;

        anim.SetTrigger("Player_Idle");

        while (state == STATE.IDLE)
        {
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                StartCoroutine(STATE_MOVE());
                yield break;
            }
            //Debug.Log(Time.realtimeSinceStartup + " || " + "현재 IDLE상태");
            yield return null;
        }
        yield break;
    }

    IEnumerator STATE_MOVE()
    {
        state = STATE.MOVE;
        if (isSeeRight==true && Input.GetAxis("Horizontal") > 0) {
            anim.SetTrigger("Player_Move_Front");
        }
        else if (isSeeRight==false && Input.GetAxis("Horizontal") < 0)
        {
            anim.SetTrigger("Player_Move_Front");
        }
        else
            anim.SetTrigger("Player_Move_Back");
        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == STATE.MOVE)
        {
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
            }
            //   Debug.Log(Time.realtimeSinceStartup + " || " + "현재 MOVE상태");
            yield return null;
        }
        yield break;
    }

    IEnumerator STATE_DEAD(){
 state = STATE.MOVE;
        //ThisAnimator.SetTrigger((int)State.DEAD);

        while (state == STATE.DEAD)
        {
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
            }
            //   Debug.Log(Time.realtimeSinceStartup + " || " + "현재 DEAD상태");
            yield return null;
        }
        yield break;
    }

   

    private void event_Death()
    {
        UIManager.Instance.panel_GameOver.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(STATE_DEAD());
    }

    public void Respown() {
        transform.position = spownPos;
        Hp = maxHp;
        rigid.velocity=new Vector3(0, 0, 0);
    }
}
