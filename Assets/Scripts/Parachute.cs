using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Parachute : MonoBehaviour
{

    PhotonView view;

    [SerializeField]
    private ParticleSystem prefabExplosion;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject[] randomPowerups;

    [SerializeField]
    private float[] percentages;

    private GameObject powerupSpawn;

    private int randomIndex;

    //private Vector3 offSet =  new Vector3(0f, 0.5f, 0f);

    private void Awake() 
    {
        prefabExplosion.Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        randomIndex = Random.Range(0, randomPowerups.Length);
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
        if(other.gameObject.tag == "Splash" && gameObject.tag == "Crate")
        {
            view.RPC("DestroyObject", RpcTarget.All);
        }
        if(other.gameObject.tag == "Ground" && gameObject.tag == "Crate")
        {
            view.RPC("DestroyObject", RpcTarget.All);
        }
    }

    [PunRPC]
    void DestroyObject()
    {
        prefabExplosion.gameObject.SetActive(true);
        prefabExplosion.Play();
        gameObject.SetActive(false);
        if(view.IsMine)
        {
            powerupSpawn = PhotonNetwork.Instantiate(randomPowerups[GetRandomPowerup()].name, spawnPoint.position, Quaternion.identity);
        }
    }

    //This is temp and might not use, but works well
    private int GetRandomPowerup()
    {
        float random = Random.Range(0f, 1f);
        float numForAdding = 0;
        float total = 0;
        for (int i = 0; i < percentages.Length; i++)
        {
            total += percentages[i];
        }

        for (int i = 0; i < randomPowerups.Length; i++)
        {
            if(percentages[i] / total + numForAdding >= random)
            {
                return i;
            }
            else
            {
                numForAdding += percentages[i] / total;
            }
        }
        return 0;
    }
}
