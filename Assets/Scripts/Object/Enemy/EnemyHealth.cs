using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Hp {
        get {
            return hp;
        }
        set {
            if (hp > value)
            {
                StartCoroutine(GetComponent<Enemy>().State_Damaged());
            }
            hp = value;
            if(Hp <= 0)
            {
                event_Death();
            }
            Debug.Log(gameObject.name+" hp : "+Hp);
        }
    }

    [SerializeField]
    private int hp = 10;

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

    }

    private void event_Death()
    {
        StartCoroutine(GetComponent<Enemy>().State_Dead());
    }
}
