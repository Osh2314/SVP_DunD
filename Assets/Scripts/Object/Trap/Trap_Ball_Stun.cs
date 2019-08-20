using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Ball_Stun : MonoBehaviour
{
    public float second;
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
        if (collision.gameObject.tag == "Enemy")
        {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.StartCoroutine(enemy.State_Stun(second));
        }
    }
}
