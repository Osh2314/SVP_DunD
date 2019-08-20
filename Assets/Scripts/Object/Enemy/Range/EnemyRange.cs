using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public GameObject Enemy;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Enemy.gameObject.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //   Debug.Log(collision.gameObject.name + "발견");
        if (enemy.state == global::Enemy.State.MOVE || enemy.state == global::Enemy.State.ATTACK)
        {
            if (collision.gameObject.tag == "Player")
            {
                Vector3 colPos = collision.gameObject.transform.position;
                enemy.GetPlayerPosition(colPos);
                enemy.StartCoroutine(enemy.State_Attack());
            }
            else if(collision.gameObject.tag == "Core")
            {
                Vector3 colPos = collision.gameObject.transform.position;
                enemy.GetPlayerPosition(colPos);
                enemy.StartCoroutine(enemy.State_Attack());
            }

            //if (collision.gameObject.tag == "Enemy")
            //{
            //    Enemy enemy2 = collision.gameObject.GetComponent<Enemy>();
            //    if (enemy.state == global::Enemy.State.MOVE && enemy2.state == global::Enemy.State.ATTACK)
            //    {
            //        enemy.StartCoroutine(enemy.State_Idle());
            //    }
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy2 = collision.gameObject.GetComponent<Enemy>();
        // Debug.Log("Exit");
        if ((collision.gameObject.tag == "Player" && enemy.state == global::Enemy.State.ATTACK || collision.gameObject.tag == "Core") && enemy.state == global::Enemy.State.ATTACK)
        {
            enemy.StartCoroutine(enemy.State_Move());
        }

        //if (enemy2.state == global::Enemy.State.MOVE)
        //{
        //    enemy.StartCoroutine(enemy.State_Move());
        //}
    }
}
