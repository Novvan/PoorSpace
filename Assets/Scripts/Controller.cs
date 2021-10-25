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
    // Start is called before the first frame update
    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        Player clientServer = _server.GetServer;
        _server.photonView.RPC("InitializedPlayer", clientServer, _localPlayer);
        _server.photonView.RPC("RequestGetPlayer", _localPlayer, _localPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (_character == null) return;
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
