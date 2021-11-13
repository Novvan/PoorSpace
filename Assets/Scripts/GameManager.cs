using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviourPun
{
    public int maxScore;
    Dictionary<Player, int> _scores = new Dictionary<Player, int>();

    public void AddScore(Player client, int score = 1)
    {
        if (!_scores.ContainsKey(client))
        {
            _scores[client] = 0;
        }
        _scores[client] += score;
        if (IsAWinner(client))
        {
            Win(client);
        }
    }
    public bool IsAWinner(Player client)
    {
        if (_scores.ContainsKey(client))
        {
            return _scores[client] >= maxScore;
        }
        else
        {
            return false;
        }
    }
    void Win(Player client)
    {
        photonView.RPC("PlayerWinLose", RpcTarget.All, client);
    }
    [PunRPC]
    public void PlayerWinLose(Player client)
    {
        if (PhotonNetwork.LocalPlayer == client)
        {
            print("GANASTE");
        }
        else
        {
            Application.Quit(1);
            print("Lose");
        }
    }

}
