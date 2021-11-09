using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Controller : MonoBehaviour
{
    [SerializeField] ServerManager _server;
    private Player _localPlayer;
    private Character _character;
    private bool _isLocked;

    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        Player clientServer = _server.GetServer;
        _server.photonView.RPC("InitializedPlayer", clientServer, _localPlayer);
        _server.photonView.RPC("RequestGetPlayer", _localPlayer, _localPlayer);
        var chatManager = FindObjectOfType<ChatManager>();
        if (chatManager)
        {
            chatManager.OnSelect += Lock;
            chatManager.OnDeselect += UnLock;
        }
    }
    void Lock()
    {
        _isLocked = true;
    }
    void UnLock()
    {
        _isLocked = false;
    }

    void Update()
    {
        if (_character == null) return;
        if (_isLocked) return;
        Debug.Log(_isLocked);
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");
        _character.Move(dir);
    }

    public Character SetCharacter
    {
        set 
        {
            _character = value;
        }
    }
}
