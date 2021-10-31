using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class NetManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField _nickNameInput;
    [SerializeField] Button button;
    private void Start()
    {
        button.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        button.interactable = true;
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
    public void ConnectRoom() 
    {
        if (string.IsNullOrEmpty(_nickNameInput.text) || string.IsNullOrWhiteSpace(_nickNameInput.text)) return;
        PhotonNetwork.NickName = _nickNameInput.text;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("Room", options, TypedLobby.Default);
        button.interactable = false;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        button.interactable = true;
    }
}
