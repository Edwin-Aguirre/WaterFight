using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            StartCoroutine(DestroySplashZone());
        }
    }

    IEnumerator DestroySplashZone()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
