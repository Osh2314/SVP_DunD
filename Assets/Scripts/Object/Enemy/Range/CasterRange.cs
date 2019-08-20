using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterRange : MonoBehaviour
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
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
            {
                enemy.StartCoroutine(enemy.State_Attack());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy2 = collision.gameObject.GetComponent<Enemy>();
        // Debug.Log("Exit");
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") && enemy.state == global::Enemy.State.ATTACK)
        {
            enemy.StartCoroutine(enemy.State_Move());
        }
    }
}
