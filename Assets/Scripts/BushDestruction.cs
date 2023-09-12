using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BushDestruction : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bushParticles1;
    [SerializeField]
    private ParticleSystem bushParticles2;
    [SerializeField]
    private ParticleSystem bushParticles3;

    PhotonView view;

    private void Awake() 
    {
        bushParticles1.Pause();
        bushParticles2.Pause();
        bushParticles3.Pause();
    }

    private void Start() 
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!view.IsMine)
        {
            return;
        }
        if(other.gameObject.tag == "Splash" && gameObject.tag == "Bush1")
        {
            view.RPC("DestroyBush1", RpcTarget.All);
        }
        if(other.gameObject.tag == "Splash" && gameObject.tag == "Bush2")
        {
            view.RPC("DestroyBush2", RpcTarget.All);
        }
        if(other.gameObject.tag == "Splash" && gameObject.tag == "Bush3")
        {
            view.RPC("DestroyBush3", RpcTarget.All);
        }
    }

    [PunRPC]
    void DestroyBush1()
    {
        bushParticles1.gameObject.SetActive(true);
        bushParticles1.Play();
        gameObject.SetActive(false);
    }

    [PunRPC]
    void DestroyBush2()
    {
        bushParticles2.gameObject.SetActive(true);
        bushParticles2.Play();
        gameObject.SetActive(false);
    }

    [PunRPC]
    void DestroyBush3()
    {
        bushParticles3.gameObject.SetActive(true);
        bushParticles3.Play();
        gameObject.SetActive(false);
    }
}
