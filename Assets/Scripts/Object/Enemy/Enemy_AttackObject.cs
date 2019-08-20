using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackObject: MonoBehaviour
{
    public int damage = 5;
    public bool canAttack = false;
    [Header("카메라 밖으로 나가면 오브젝트를 삭제 할것인가?")]
    public bool willInvisibleDestroy = false;
    [Header("적 타격에 성공하면 오브젝트를 삭제할 것인가?")]
    public bool willSuccessAttackDestroy = true;
    [Header("몇초 후 사라지게 할것인가?")]
    public bool timeDuration = false;
    [Header("시간")]
    public float time;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDuration == true)
        {
            StartCoroutine(TimeD());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Hp -= damage;
            canAttack = false;
            if (willSuccessAttackDestroy == true)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Core" && canAttack == true)
        {
            Core core = collision.gameObject.GetComponent<Core>();
            core.Hp -= damage;
            canAttack = false;
            if (willSuccessAttackDestroy == true)
            {
                Destroy(gameObject);
            }
        }

    }
    void OnBecameInvisible()
    {
        if(willInvisibleDestroy==true)
        Destroy(gameObject);
    }

    public IEnumerator TimeD()
    {
        Debug.Log("아");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
