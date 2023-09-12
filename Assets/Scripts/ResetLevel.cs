using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviourPunCallbacks
{
    PhotonView view;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //view = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>();
        view = GetComponent<PhotonView>();
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneName()
    {
        string sceneName = currentScene.name;
        if(sceneName == "Level 1")
        {
            PhotonNetwork.LoadLevel("Level 1");
        }
        if(sceneName == "Level 2")
        {
            PhotonNetwork.LoadLevel("Level 2");
        }
        if(sceneName == "Level 3")
        {
            PhotonNetwork.LoadLevel("Level 3");
        }
    }

    public void OnRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("ConnectToServer");
        base.OnLeftRoom();
    }

    [PunRPC]
    void Restart()
    {
        SceneName();
    }
}
