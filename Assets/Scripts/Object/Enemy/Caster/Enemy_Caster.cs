using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Caster : Enemy
{
    public GameObject Blessing;
    public int point = 0;
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
        point++;
        while (state == State.ATTACK)
        {
            if (point >= 2)
            {
               // Debug.Log("삭제");
                point--;
                yield break;
            }
            Instantiate(Blessing, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
        point--;
            yield break;
    }

    }
