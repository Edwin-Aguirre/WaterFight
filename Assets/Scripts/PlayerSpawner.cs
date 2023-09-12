using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;


public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private Transform[] spawnPoints;

    private GameObject clone;

    private GameObject[] players;

    [SerializeField]
    private int amountOfPlayers;

    [SerializeField]
    private Text winnerText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button leaveGameButton;

    [SerializeField]
    private Text buttonText;

    [SerializeField]
    private Text leaveButtonText;

    private Color textColor;

    PhotonView winner;

    PhotonView photonView;

    private GameObject playerToSpawn;

    [SerializeField]
    private Text spectatorText;

    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake() 
    {
        winnerText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        leaveGameButton.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if(photonView.IsMine)
            {
                photonView.RPC("SpawnPlayers", playerList[i], spawnPoints[i].position, spawnPoints[i].rotation);
            }
        }
        // int randomNumber = Random.Range(0, spawnPoints.Length);
        // Transform spawnPoint = spawnPoints[randomNumber];
        // GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        // clone = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        //The thing above is set to a clone so that when a player spawns it will be assigned the follow/lookat
        // cinemachineVirtualCamera.Follow = clone.transform;
        // cinemachineVirtualCamera.LookAt = clone.transform;
    }

    [PunRPC]
    void SpawnPlayers(Vector3 spawnPos, Quaternion spawnRot)
    {
        if(PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] == null)
        {
            playerToSpawn = playerPrefabs[0];
            clone = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPos, spawnRot, 0);
            cinemachineVirtualCamera.Follow = clone.transform;
            cinemachineVirtualCamera.LookAt = clone.transform;
        }
        else
        {
            playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            clone = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPos, spawnRot, 0);
            cinemachineVirtualCamera.Follow = clone.transform;
            cinemachineVirtualCamera.LookAt = clone.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        amountOfPlayers = players.Length;
        if(amountOfPlayers == 1)
        {
            winner = GameObject.FindGameObjectWithTag("Player").GetPhotonView();
            Cursor.visible = true;
            
            winnerText.gameObject.SetActive(true);
            SetTextColors();
            winnerText.text = winner.Owner.NickName + " is the winner!";
            if(winner.IsMine)
            {
                restartButton.gameObject.SetActive(true);
            }
            leaveGameButton.gameObject.SetActive(true);
        }
        //This is temp!!!!
        PhotonView activePlayer = GameObject.FindGameObjectWithTag("Player").GetPhotonView();
        if(clone.GetComponent<CharacterController>().gameObject.activeSelf == false)
        {
            cinemachineVirtualCamera.Follow = activePlayer.transform;
            cinemachineVirtualCamera.LookAt = activePlayer.transform;
            spectatorText.gameObject.SetActive(true);
            spectatorText.text = "Spectating " + activePlayer.Owner.NickName;
        }
        if(activePlayer.gameObject.activeSelf == false)
        {
            cinemachineVirtualCamera.Follow = activePlayer.transform;
            cinemachineVirtualCamera.LookAt = activePlayer.transform;
        }
    }

    private void SetTextColors()
    {
        if(winner.name == "Player Red(Clone)")
        {
            textColor =  new Color32(196, 36, 48, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Orange(Clone)")
        {
            textColor =  new Color32(237, 118, 20, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Yellow(Clone)")
        {
            textColor =  new Color32(255, 200, 37, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Green(Clone)")
        {
            textColor =  new Color32(30, 111, 80, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Blue(Clone)")
        {
            textColor =  new Color32(0, 57, 109, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Purple(Clone)")
        {
            textColor =  new Color32(98, 36, 97, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Pink(Clone)")
        {
            textColor =  new Color32(243, 137, 245, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Light Blue(Clone)")
        {
            textColor =  new Color32(148, 253, 255, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Black(Clone)")
        {
            textColor =  new Color32(19, 19, 19, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
        if(winner.name == "Player Gray(Clone)")
        {
            textColor =  new Color32(180, 180, 180, 255);
            winnerText.color = textColor;
            buttonText.color = textColor;
            leaveButtonText.color = textColor;
        }
    }
}
