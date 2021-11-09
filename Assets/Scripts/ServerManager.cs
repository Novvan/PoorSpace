﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPun
{
    [SerializeField] GameObject _prefab;
    Player _server;
    Dictionary<Player, Character> _characters = new Dictionary<Player, Character>();
    public Player GetServer => _server;

    void Awake()
    {
        _server = PhotonNetwork.MasterClient;
    }
    [PunRPC]
    public void RequestRegisterPlayer(Player client, int ID)
    {
        PhotonView pv = PhotonView.Find(ID);
        if (pv == null) return;
        var character = pv.GetComponent<Character>();
        if (character == null) return;
        _characters[client] = character;
    }

    [PunRPC]
    public void RequestMove(Player client, Vector2 dir)
    {
        if (_characters.ContainsKey(client))
        {
            var character = _characters[client];
            character.Move(dir);
        }
    }
    [PunRPC]
    public void RequestShoot(Player client)
    {
        if (_characters.ContainsKey(client))
        {
            var character = _characters[client];
            character.Shoot(client);
        }
    }

    [PunRPC]
    public void InitializedPlayer(Player client)
    {
        GameObject obj = PhotonNetwork.Instantiate(_prefab.name,Vector3.zero,Quaternion.identity);
        Character character = obj.GetComponent<Character>();
        _characters[client] = character;
    }
    [PunRPC]
    public void RequestGetPlayer(Player client) 
    {
        if (_characters.ContainsKey(client)) 
        {
            var character = _characters[client];
            int ID = character.photonView.ViewID;
            photonView.RPC("SetPlayer", client, ID);
        }
    }
    [PunRPC]
    public void SetPlayer(int ID)
    {
        PhotonView pv = PhotonView.Find(ID);
        if (pv == null) return;
        var character = pv.GetComponent<Character>();
        if (character == null) return;
        var controller = GameObject.FindObjectOfType<Controller>();
        if (controller == null) return;
        controller.SetCharacter = character;
    }
}
