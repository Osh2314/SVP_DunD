using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkill1 : MonoBehaviour
{
    public float dashPower;
    private bool isDirRight;
    private Player pm;
    private PolygonCollider2D bc;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<Player>();
        bc = GetComponent<PolygonCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            bc.isTrigger = true;
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            if (pm.GetDirection() == true)
            {
                StartCoroutine(startDash(0.5f, dashPower));
            }
            else
            {
                StartCoroutine(startDash(0.5f, dashPower * -1));
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    IEnumerator startDash(float duration, float dashX) {
        while (duration > 0) {
            rigid.velocity = new Vector2(dashX, rigid.velocity.y);
            duration -= Time.deltaTime;
            yield return null;
        }
        rigid.velocity = new Vector2(0, 0);
        bc.isTrigger = false;
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        pm.cJump = true; 

        yield break;
    }
}
