using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinRange2 : MonoBehaviour
{
    public GameObject Enemy;

    Enemy_Assassin enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy_Assassin>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Enemy.gameObject.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Core")
        {
            enemy.GetIsThrowing(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Core")
        {
            enemy.GetIsThrowing(true);
        }
    }
}
