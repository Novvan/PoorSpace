using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPun
{
    [SerializeField] GameObject _prefab;
    private Player _server;
    private Dictionary<Player, Character> _characters = new Dictionary<Player, Character>();
    public Player GetServer => _server;

    // Start is called before the first frame update
    void Awake()
    {
        _server = PhotonNetwork.MasterClient;
    }

    // Update is called once per frame
    void Update()
    {

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
