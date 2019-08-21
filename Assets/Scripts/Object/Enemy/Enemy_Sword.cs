using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sword : Enemy
{
    //검을 쓰는 적의 스크립트이다.
    public GameObject sword;

    public float attackAngle;
    public float attackAngleZ_Min = 240f;
    public float attackAngleZ_Max = 320f;

    private bool isAttacking = false;
    public bool cool = true;
    public bool swordup = false;
    private Vector3 originalRotate;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        originalRotate = sword.transform.localEulerAngles;
    }



    public override IEnumerator State_Attack()
    {
        Debug.Log("Attack!");
        Vector3 dir = followTarget.transform.position - transform.position;
        dir.Normalize();
        if (dir.x > 0)//목표가 오른쪽에 있다고 판단
            isSeeLeft = false;
        else
            isSeeLeft = true;
        state = State.ATTACK;
        
        if (isAttacking == false)
        {
            anim.SetTrigger("Enemy_Attack");
            //StartCoroutine(Atk());
            isAttacking = true;

            while (state == State.ATTACK)
            {
                sword.GetComponent<Enemy_AttackObject>().canAttack = true;
                while (cool == true)
                {
                    if (swordup == true)
                    {
                        //Debug.Log("업");
                        sword.transform.localEulerAngles += new Vector3(0, 0, attackAngle * Time.deltaTime);
                        if (sword.transform.localEulerAngles.z >= originalRotate.z)
                        {
                            swordup = false;
                            cool = false;
                        }
                    }
                    if (swordup == false)
                    {
                       // Debug.Log("다운");
                        sword.transform.localEulerAngles += new Vector3(0, 0, attackAngle * Time.deltaTime * -1);
                        if (sword.transform.localEulerAngles.z <= attackAngleZ_Min)
                        {
                            swordup = true;
                        }
                    }
                    yield return null;
                }
                sword.GetComponent<Enemy_AttackObject>().canAttack = false;

                sword.transform.localEulerAngles = originalRotate;

                //yield return new WaitForSeconds(1f);
                cool = true;
            }
            isAttacking = false;
        }

        yield break;
    }

}
