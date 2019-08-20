using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinRange : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemy.state == global::Enemy.State.MOVE)
        {
            if(enemy.gameObject.transform.position.x >= collision.transform.position.x)
            {
            enemy.gameObject.transform.position = new Vector3(collision.transform.position.x  - 1, collision.gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                enemy.gameObject.transform.position = new Vector3(collision.transform.position.x + 1, collision.gameObject.transform.position.y, gameObject.transform.position.z);
            }

        }
    }
}
