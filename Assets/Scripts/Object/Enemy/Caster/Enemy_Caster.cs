using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Caster : Enemy
{
    public GameObject Blessing;

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
            anim.SetTrigger("Enemy_Attack");
            //StartCoroutine(Atk());
            isAttacking = true;

            while (state == State.ATTACK)
            {
                Instantiate(Blessing, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                yield return new WaitForSeconds(3f);
            }
            isAttacking = false;
            yield break;
        }

    }
}
