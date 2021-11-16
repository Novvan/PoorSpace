using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class NetManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public InputField nickNameInput;
    [SerializeField] public Button button;
    private bool _joinedLobby = false;

    private void Start()
    {
        button.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        if (nickNameInput != null)
        {
            if (nickNameInput.text.Length > 0 && _joinedLobby) button.interactable = true;
            else button.interactable = false;
        }
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby() => _joinedLobby = true;

    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("Gameplay");

    public void ConnectRoom()
    {
        if (string.IsNullOrEmpty(nickNameInput.text) || string.IsNullOrWhiteSpace(nickNameInput.text)) return;
        PhotonNetwork.NickName = nickNameInput.text;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Room", options, TypedLobby.Default);
        button.interactable = false;
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => button.interactable = true;

    public override void OnJoinRoomFailed(short returnCode, string message) => button.interactable = true;
}
