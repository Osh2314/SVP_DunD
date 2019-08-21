using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Thorn : MonoBehaviour
{
    public float second;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            enemyHealth.Hp -= damage;
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.StartCoroutine(enemy.Slow(second));
        }
    }

}