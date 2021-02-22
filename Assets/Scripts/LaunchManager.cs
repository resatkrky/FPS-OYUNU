using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update //Panellerin başlangıçta nasıl açılacağını ayarlar
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;
    
    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectToPhotonServer()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); //Photon Serverlerına baglanma
            EnterGamePanel.SetActive(false);
            ConnectionStatusPanel.SetActive(true);
        }
    }

    public override void OnConnectedToMaster() //Photon Serverına baglanır
    {
        Debug.Log(PhotonNetwork.NickName + "Photon Server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnConnected() //Network'e bağlanır
    {
        Debug.Log("Connected to Internet");
    }

    public void JoinRandomRoom() // Var olan odaya katıl
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode,string message) //Oda yoksa mesaj ver
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateandJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    void CreateandJoinRoom()
    {
        string randomRoomName = "Room" + randomRoomName.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayer = 20;
        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }
}
