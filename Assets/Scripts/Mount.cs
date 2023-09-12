using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Mount : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerMovement playerScript;
    [SerializeField]
    public GameObject mountPrefab;
    [SerializeField]
    private MountMovement mountMovement;

    PhotonView view;
    

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!view.IsMine)
        {
            return;
        }
        // if(other.gameObject.transform.parent.tag == "Car")
        // {
        //     view.RPC("CarMount", RpcTarget.All);
        //     //CarMount();
        // }
        if(other.gameObject.transform.parent.tag == "Turtle")
        {
            view.RPC("TurtleMount", RpcTarget.All);
            //TurtleMount();
        }
        if(other.gameObject.transform.parent.tag == "Snail")
        {
            view.RPC("SnailMount", RpcTarget.All);
            //SnailMount();
        }
        
    }

    [PunRPC]
    void CarMount()
    {
        mountPrefab = GameObject.Find("Car");
        mountMovement = GameObject.Find("Car").GetComponent<MountMovement>();
        playerScript.mountMovement = GameObject.Find("Car").GetComponent<MountMovement>();
        mountMovement.controller.enabled = true;
        mountMovement.enabled = true;
        mountMovement.playerSpeed = 10;
        playerScript.animator.SetBool("isSitting", true);
        player.transform.parent = mountPrefab.transform.GetChild(0);
        playerScript.Sitting();
        player.transform.position = new Vector3(mountPrefab.transform.position.x, 0.1f, mountPrefab.transform.position.z);
    }
    [PunRPC]
    void TurtleMount()
    {
        mountPrefab = GameObject.Find("Turtle");
        mountMovement = GameObject.Find("Turtle").GetComponent<MountMovement>();
        playerScript.mountMovement = GameObject.Find("Turtle").GetComponent<MountMovement>();
        mountMovement.controller.enabled = true;
        mountMovement.enabled = true;
        mountMovement.playerSpeed = 5;
        playerScript.animator.SetBool("isSitting", true);
        player.transform.parent = mountPrefab.transform.GetChild(0);
        playerScript.Sitting();
        player.transform.position = new Vector3(mountPrefab.transform.position.x, 0.1f, mountPrefab.transform.position.z);
    }
    [PunRPC]
    void SnailMount()
    {
        mountPrefab = GameObject.Find("Snail");
        mountMovement = GameObject.Find("Snail").GetComponent<MountMovement>();
        playerScript.mountMovement = GameObject.Find("Snail").GetComponent<MountMovement>();
        mountMovement.controller.enabled = true;
        mountMovement.enabled = true;
        mountMovement.playerSpeed = 1;
        playerScript.animator.SetBool("isSitting", true);
        player.transform.parent = mountPrefab.transform.GetChild(0);
        playerScript.Sitting();
        player.transform.position = new Vector3(mountPrefab.transform.position.x, 0.1f, mountPrefab.transform.position.z);
    }
}
