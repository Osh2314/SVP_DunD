using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Assassin_Knife_Cut : MonoBehaviour
{
    public float time; //지속시간
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
