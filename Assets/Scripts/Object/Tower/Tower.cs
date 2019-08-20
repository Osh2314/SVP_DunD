using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Attack;
    public enum State { IDLE, ATTACK};
    public State state = State.IDLE;
    public float atkspeed;
    public float cooldown;

    private int atkpoint = 0;
    private Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator State_Idle()
    {
        state = State.IDLE;
        yield break;
    }

    public IEnumerator State_Attack()
    {
        state = State.ATTACK;
        atkpoint++;
            if (atkpoint >= 2)
            {
                atkpoint--;
                yield break;
            }
        while (state == State.ATTACK)
        {
            GameObject createdObject;
            Rigidbody2D createdRigid;
            Vector2 lookDir = enemyPos - transform.position;
            lookDir.Normalize();
            createdObject = Instantiate(Attack, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
            createdRigid = createdObject.GetComponent<Rigidbody2D>();
            createdRigid.AddForce(lookDir * atkspeed);
            yield return new WaitForSeconds(cooldown);
        }
        atkpoint--;
        yield break;
    }

    public void GetEnemyPosition(Vector3 pos)
    {
        enemyPos = pos;
    }
}
