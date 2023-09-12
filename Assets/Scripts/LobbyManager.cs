using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField roomInputField;

    [SerializeField]
    private GameObject lobbyPanel;

    [SerializeField]
    private GameObject roomPanel;

    [SerializeField]
    private Text roomName;

    [SerializeField]
    private RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();

    [SerializeField]
    private Transform contentObject;

    [SerializeField]
    private float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    [SerializeField]
    private List<PlayerItem> playerItemsList = new List<PlayerItem>();

    [SerializeField]
    private PlayerItem playerItemPrefab;
    
    [SerializeField]
    private Transform playerItemParent;

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject levelButton;

    [SerializeField]
    private Text lobbyText;

    private string LevelName;

    [SerializeField]
    private GameObject levelPanel;

    [SerializeField]
    private GameObject level1HighlightPanel;
    [SerializeField]
    private GameObject level2HighlightPanel;
    [SerializeField]
    private GameObject level3HighlightPanel;

    private void Start() 
    {
        PhotonNetwork.JoinLobby();
        LevelName = "Level 1";
        level1HighlightPanel.SetActive(true);
    }

    public void OnClickLevel1Button()
    {
        LevelName = "Level 1";
        level1HighlightPanel.SetActive(true);
        level2HighlightPanel.SetActive(false);
        level3HighlightPanel.SetActive(false);
    }

    public void OnClickLevel2Button()
    {
        LevelName = "Level 2";
        level1HighlightPanel.SetActive(false);
        level2HighlightPanel.SetActive(true);
        level3HighlightPanel.SetActive(false);
    }

    public void OnClickLevel3Button()
    {
        LevelName = "Level 3";
        level1HighlightPanel.SetActive(false);
        level2HighlightPanel.SetActive(false);
        level3HighlightPanel.SetActive(true);
    }

    public void OnClickChangeLevel()
    {
        roomPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void OnClickBackToRoom()
    {
        roomPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void OnClickCreate()
    {
        if(roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 6, BroadcastPropsChangeToAll = true});
        }
    }

    public void onClickMainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Title");
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        lobbyText.gameObject.SetActive(false);
        roomName.text = "Room Name:  " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >=  nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        lobbyText.gameObject.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update() 
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
            levelButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
            levelButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel(LevelName);
    }
}
