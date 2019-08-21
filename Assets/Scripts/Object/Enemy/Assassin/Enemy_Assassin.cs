using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Assassin : Enemy
{
    // Start is called before the first frame update
    public GameObject Knife;
    public GameObject Cut;
    public float knifespeed;
    public float cutspeed;

    private bool isThrowing = true;
    public bool isAttacking = false;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator State_Attack()
    {
        state = State.ATTACK;
        Vector3 dir = followTarget.transform.position - transform.position;
        dir.Normalize();
        if (dir.x > 0)//목표가 오른쪽에 있다고 판단
            isSeeLeft = false;
        else
            isSeeLeft = true;

        if (isAttacking == false)
        {
            //anim.SetBool("Enemy_Attack", true);
            isAttacking = true;
            while (state == State.ATTACK)
            {
                anim.SetBool("Enemy_Attack", true);
                if (isThrowing == false) // 타겟이 일정범위 안에 있을때 바로 앞에 공격을 한다.
                {
                    anim.SetFloat("Enemy_Attack_Type", -1.0f);
                    if (isSeeLeft == false) // 타겟이 자신보다 오른쪽에 있을때
                    {
                        GameObject createdObject;
                        Rigidbody2D createdRigid;
                        createdObject = Instantiate(Cut, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                        createdObject.transform.localEulerAngles = new Vector3(0, 180, 0);
                        createdRigid = createdObject.GetComponent<Rigidbody2D>();
                        createdRigid.AddForce(new Vector2(cutspeed, 0));
                    }
                    else // 타겟이 자신보다 왼쪽에 있을때
                    {
                        GameObject createdObject;
                        Rigidbody2D createdRigid;
                        createdObject = Instantiate(Cut, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                        createdRigid = createdObject.GetComponent<Rigidbody2D>();
                        createdRigid.AddForce(new Vector2(cutspeed * -1, 0));
                    }
                }
                else //isThrowing이 true일때 (타겟이 일정범위 밖에 있을때 타겟에게 단검을 던진다)
                {
                    anim.SetFloat("Enemy_Attack_Type", 1.0f);
                    GameObject createdObject;
                    Rigidbody2D createdRigid;
                    Vector2 lookDir = followTarget.transform.position - transform.position;
                    lookDir.Normalize();
                    createdObject = Instantiate(Knife, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
                    createdRigid = createdObject.GetComponent<Rigidbody2D>();
                    createdRigid.AddForce(lookDir * knifespeed);
                }
                yield return new WaitForSeconds(1f);
                anim.SetBool("Enemy_Attack", false);
                yield return new WaitForSeconds(2f);
            }
            anim.SetFloat("Enemy_Attack_Type", 0);
            isAttacking = false;
            yield break;
        }
    }

public void GetIsThrowing(bool iT) 
{
        isThrowing = iT;
}

}

//state = State.ATTACK;
//        atkp++;
//        while (state == State.ATTACK)
//        {
//            if (atkp >= 2)
//            {
//                atkp--;
//                yield break;
//            }
//            GameObject createdObject;
//Rigidbody2D createdRigid;
//Vector2 lookDir = playerPos - transform.position;
//lookDir.Normalize();
//            createdObject = Instantiate(Knife, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
//            createdRigid = createdObject.GetComponent<Rigidbody2D>();
//            createdRigid.AddForce(lookDir* knifespeed);
//            yield return new WaitForSeconds(3f);
//        }
//        atkp--;
//        yield break;
//    }
