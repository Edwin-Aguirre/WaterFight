using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    [SerializeField]
    private GameObject centerPoint;

    [SerializeField]
    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centerPoint.transform.position, Vector3.down, speed * Time.deltaTime);
    }
}
