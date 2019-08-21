using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public Image HPBar;
    public GameObject Fire;
    public float speed;
    public float spinforce = 10000;
    public int dropGoldValue = 10;
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (hp > value)
            {
                StartCoroutine(GetComponent<Enemy>().State_Damaged());
            }
            hp = value;
            if (Hp <= 0)
            {
                StartCoroutine(State_Dead());
            }
            HPBar.fillAmount = hp/Maxhp;
            Debug.Log(gameObject.name + " hp : " + Hp);
        }
    }
    [SerializeField]
    private float hp = 40;
    [SerializeField]
    private float Maxhp = 40;


    public enum State { IDLE, MOVE, STUN, DAMAGED, ATTACK, DEAD};
    public State state = State.IDLE;

    GameObject createObject;
    protected Rigidbody2D rigid;
    protected Animator anim;
    protected GameObject followTarget;

    private int burncount = 0;
    public bool isSeeLeft = true;
    // Start is called before the first frame update
    protected void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetfollowTarget(GameManager.Instance.core.gameObject);
        StartCoroutine(State_Idle());

        hp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSeeLeft == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
            transform.localEulerAngles = new Vector3(0, 180, 0);

    }

    public void SetfollowTarget(GameObject newTarget)
    {
        followTarget = newTarget;
    }

    public IEnumerator State_Idle() {
        state = State.IDLE;

        while (state == State.IDLE) {
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING) {
                StartCoroutine(State_Move());
                yield break;
            }
            //Debug.Log(Time.realtimeSinceStartup + " || " + "현재 IDLE상태");
            yield return null;
        }
        yield break;
    }

    public IEnumerator State_Move()
    {
        state = State.MOVE;
        anim.SetBool("Enemy_Attack", false);


        Vector3 dir = followTarget.transform.position - transform.position;
        dir.Normalize();
        if (dir.x > 0)//목표가 오른쪽에 있다고 판단
            isSeeLeft = false;
        else
            isSeeLeft = true;

        while (state == State.MOVE)
        {
            
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                transform.position+=new Vector3((dir*speed * Time.deltaTime).x, 0, 0);
            }
            //Debug.Log(Time.realtimeSinceStartup + " || " + "현재 MOVE상태");
            yield return null;
        }
        yield break;
    }

    public IEnumerator State_Damaged()
    {
        //state = State.DAMAGED;
        anim.SetTrigger("Enemy_Damaged");

        //while (state == State.DAMAGED)
        //{
        //    //피격 애니메이션이 끝났다면
        //    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //        break;
        //    yield return null;
        //}
        //if(state==State.DAMAGED)
        //    StartCoroutine(State_Move());
        yield break;
    }
    public IEnumerator State_Stun(float second)
    {
        state = State.STUN;
        while(second > 0)
        {
            yield return new WaitForSeconds(1f);
            second--;
        }
        StartCoroutine(State_Move());
        yield break;
    }

    public IEnumerator Slow(float second)
    {
        float originalSpeed = speed;
        speed = speed * 0.5f;
        yield return new WaitForSeconds(second);
        speed = originalSpeed;
        yield break;
    }

    public IEnumerator Burn(int amount)
    {
        burncount++;
        if(burncount>=2)
        {
            burncount--;
            yield break;
        }
        createObject = Instantiate(Fire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity,transform);
        while (amount > 0)
        {
            //Debug.Log(gameObject.name + "불타는중");
            Hp -= 5;
            amount--;
            yield return new WaitForSeconds(1f);
        }
        burncount--;
        Destroy(createObject);
        yield break;
    }

    public virtual IEnumerator State_Attack()
    {
        
        anim.SetTrigger("Enemy_Attack");
        yield break;
    }

    public IEnumerator State_Dead()
    {
        state = State.DEAD;

        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.AddForce(new Vector3(-500, 600, spinforce));

        float duration = 2.0f;
        while (duration > 0) {
            duration -= Time.deltaTime;

            transform.localEulerAngles += new Vector3(0, 0, 300 * Time.deltaTime);
            yield return null;
        }
        GameManager.Instance.Gold += dropGoldValue;
        Destroy(gameObject);
        //EnemySponwer의 nowDeadEnemyCount변수는 라운드 종료조건에 사용된다.
        GameManager.Instance.nowDeadEnemyCount += 1;
        yield break;
    }

}
