using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : Enemy
{
    public GameObject Bow;
    public GameObject Arrow;
    public float arrowspeed;
    public float arrowway;

    Animator BowAnim;
    Enemy_Archer_Arrow arrow;
    public bool isAttacking = false;

    // Start is called before the first frame update
    new void Start()
    {
        arrow = GetComponent<Enemy_Archer_Arrow>();
        //BowAnim = Bow.GetComponent<Animator>();
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator State_Attack()
    {
        state = State.ATTACK;
        Bow.SetActive(true);
        Vector3 dir = followTarget.transform.position - transform.position;
        dir.Normalize();
        if (dir.x > 0)//목표가 오른쪽에 있다고 판단
            isSeeLeft = false;
        else
            isSeeLeft = true;

        if (isAttacking == false)
        {
            anim.SetBool("Enemy_Attack", true);
            //anim.SetBool("Enemy_Attack", true);
            isAttacking = true;

            while (state == State.ATTACK)
            {
                yield return new WaitForSeconds(3f);
                if (isSeeLeft == true)
                {
                    arrowway = -0.3f;
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    arrowway = 0.3f;
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                GameObject createdObject;
                Rigidbody2D createdRigid;
                Vector2 lookDir = followTarget.transform.position - transform.position;
                lookDir.Normalize();
                createdObject = Instantiate(Arrow, new Vector3(transform.position.x + arrowway, transform.position.y - 0.3f, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
                createdRigid = createdObject.GetComponent<Rigidbody2D>();
                createdRigid.AddForce(lookDir * arrowspeed);
            }
            isAttacking = false;
            Bow.SetActive(false);
            yield break;
        }


    }
}
