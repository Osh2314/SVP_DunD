using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_FootHold : MonoBehaviour
{
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
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.StartCoroutine(enemy.State_Dead());
            Destroy(gameObject);
        }
    }
}
