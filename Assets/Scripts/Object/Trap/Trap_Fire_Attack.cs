using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy.StartCoroutine(enemy.Burn(3));
        }
    }
}
