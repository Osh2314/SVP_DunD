using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    //public float knockBackForce = 5;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (hp > value)
            {
                StartCoroutine(Camera.main.GetComponent<CameraShake>().CameraVibration(((float)35 / (hp * 2 + 1)) + 0.2f, (10 / (hp/3))+2f, 3f));
                Debug.Log("코어 피격에 의한 카메라 진동");
            }
            hp = value;
            UIManager.Instance.slider_CoreHp.value = hp;
            if (hp <= 0)
            {
                event_Death();
            }
            Debug.Log(gameObject.name + " hp : "+Hp);
        }
    }

    [SerializeField]
    private int hp = 10;
    private void Awake()
    {
        GameManager.Instance.core = this;
    }
    void Start()
    {
        UIManager.Instance.slider_CoreHp.maxValue = hp;
        UIManager.Instance.slider_CoreHp.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
    //        Vector3 knockBackDir=new Vector3(collision.gameObject.transform.position.x-transform.position.x, 0, 0);
    //        knockBackDir.Normalize();
    //        rigid.AddForce(knockBackDir*knockBackForce);
    //    }
    //}

    private void event_Death()
    {
        UIManager.Instance.panel_GameOver.SetActive(true);
        Time.timeScale = 0;
        
        //여기에 코어 파괴 애니메이션 StartCoroutine(STATE_DEAD());
    }

}
