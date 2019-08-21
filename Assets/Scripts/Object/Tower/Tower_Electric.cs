using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Electric : MonoBehaviour
{
    public float second;

    Animator anim;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
                anim.SetTrigger("Attack");
                Debug.Log("스턴");
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.StartCoroutine(enemy.State_Stun(0.3f));
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
