using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class NetManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("Room", options, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay"):
    }
}
