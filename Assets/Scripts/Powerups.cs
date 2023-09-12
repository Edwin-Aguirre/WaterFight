using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Powerups : MonoBehaviour
{
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
        // if(!view.IsMine)
        // {
        //     return;
        // }
        if(other.gameObject.tag == "Player" && gameObject.tag == "WaterBottle")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sounds_powerup5");
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "BubbleShield")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sound_neutral6");
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "WaterBalloon")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sounds_powerup10");
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "Muscle")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sounds_powerup16");
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "Heart")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sounds_powerup3");
        }
        if(other.gameObject.tag == "Player" && gameObject.tag == "Shoes")
        {
            view.RPC("DestroyObject", RpcTarget.All);
            SoundManagerScript.PlaySound("sfx_sounds_powerup3");
        }
    }

    [PunRPC]
    void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
