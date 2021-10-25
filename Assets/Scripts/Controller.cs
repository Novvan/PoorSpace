using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Controller : MonoBehaviour
{
    [SerializeField] ServerManager _server;
    private Player _localPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        Player clientServer = _server.GetServer;
        _server.photonView.RPC("InitializedPlayer", clientServer, _localPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
