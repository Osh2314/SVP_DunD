using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallObj_Rendering : MonoBehaviour
{
    
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log(other.gameObject.name + " : touched!!!");
    //    if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Floor"
    //        || other.gameObject.tag == "Player")
    //    {
    //        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
    //        canInstall = false;
    //    }

    //}
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if(other.gameObject.tag == "Platform" || other.gameObject.tag == "Floor"
    //        || other.gameObject.tag == "Player")
    //        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    //    canInstall = true;
    //}
    private void OnDestroy()
    {
        GetComponent<Collider2D>().isTrigger = false;
        Destroy(GetComponent<Rigidbody2D>());
    }

   
}
