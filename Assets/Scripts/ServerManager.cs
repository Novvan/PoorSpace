using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPun
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _enemySpawner;
    private GameManager _gm;
    private Player _server;
    public Player GetServer => _server;
    private Dictionary<Player, Character> _characters = new Dictionary<Player, Character>();

    void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
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
        if (_characters.Count >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }
    [PunRPC]
    public void StartGame()
    {
        if (_gm != null)
        {
            _gm.StartGame = true;
            _enemySpawner.SetActive(true);
        }
    }

    [PunRPC]
    public void StopGame()
    {
        if (_gm != null)
        {
            _gm.StartGame = false;
            _enemySpawner.SetActive(false);
        }
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
        GameObject obj = PhotonNetwork.Instantiate(_prefab.name, Vector3.zero, Quaternion.identity);
        Character character = obj.GetComponent<Character>();
        _characters[client] = character;
        int ID = character.photonView.ViewID;
        photonView.RPC("RequestRegisterPlayer", RpcTarget.Others, client, ID);
        ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();
        table["Team"] = 1;
        client.CustomProperties = table;
        int team = (int)client.CustomProperties["Team"];
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
