using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject Fire;
    public float speed;
    public float spinforce = 10000;
    public int dropGoldValue = 10;
    public enum State { IDLE, MOVE, STUN, ATTACK, DEAD};
    public State state = State.IDLE;

    GameObject createObject;
    protected Rigidbody2D rigid;
    protected Vector3 playerPos;

    private int burncount = 0;
    // Start is called before the first frame update
    protected void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(State_Idle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerPosition(Vector3 pos)
    {
        playerPos = pos;
    }

    public IEnumerator State_Idle() {
        state = State.IDLE;

        //ThisAnimator.SetTrigger((int)State.IDLE);

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
        Vector3 dir = GameManager.Instance.core.transform.position - transform.position;
        dir.Normalize();
        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == State.MOVE)
        {
            //플레이어
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                transform.Translate(new Vector3((dir*speed * Time.deltaTime).x, 0, 0));
                //if()
            }
         //   Debug.Log(Time.realtimeSinceStartup + " || " + "현재 MOVE상태");
            yield return null;
        }
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
            EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Hp -= 5;
            amount--;
            yield return new WaitForSeconds(1f);
        }
        burncount--;
        Destroy(createObject);
        yield break;
    }

    public virtual IEnumerator State_Attack()
    {
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
