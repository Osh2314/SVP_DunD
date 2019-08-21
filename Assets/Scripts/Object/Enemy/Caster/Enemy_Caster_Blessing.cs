using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Caster_Blessing : MonoBehaviour
{
    public int healAmount;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TimeD());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemyHealth = collision.gameObject.GetComponent<Enemy>();
            enemyHealth.Hp += healAmount;
        }
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Hp -= healAmount;
        }
    }

    public IEnumerator TimeD()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
