using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyLight : MonoBehaviour
{
    [SerializeField]
    private GameObject fireflyLight;

    [SerializeField]
    private Color fireflyLightColor;

    private float emissiveIntensity;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float emissiveStrength;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        emissiveIntensity = Mathf.PingPong(Time.time * speed, emissiveStrength);
        fireflyLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", fireflyLightColor * emissiveIntensity);
    }
}
