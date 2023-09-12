using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterBalloon : MonoBehaviour
{
    public GameObject splashZone;

    [SerializeField]
    private GameObject splash;

    [SerializeField]
    private Transform spawnPoint;

    private Vector3 offSet =  new Vector3(0f, 0.09f, 0f);

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            StartCoroutine(DestroyWaterBalloon());
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        
    }

    IEnumerator DestroyWaterBalloon()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        if(view.IsMine)
        {
            splashZone = PhotonNetwork.Instantiate(splash.name, spawnPoint.position - offSet, Quaternion.identity);
            SoundManagerScript.PlaySound("sfx_exp_short_hard3");
        }
    }
}
