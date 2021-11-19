﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [SerializeField] private float _numberOfPlayers;
    [SerializeField] private float _timer = 90;
    [SerializeField] private TMP_Text _timerLabel;
    private bool _startGame = false;

    private bool _endGame = false;

    Dictionary<Player, int> _scores = new Dictionary<Player, int>();

    public bool StartGame { set => _startGame = value; }

    public void AddScore(Player client, int score = 1)
    {
        if (!_scores.ContainsKey(client))
        {
            _scores[client] = 0;
        }
        _scores[client] += score;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && _startGame && _timer > 0)
        {
            ExecuteTimer();
        }
    }

    private void ExecuteTimer()
    {
        _timer -= Time.deltaTime;

        if (_timerLabel != null)
        {
            _timerLabel.text = _timer.ToString().Substring(0, 4);

            if (_timer <= 0)
            {
                if (!_endGame)
                {
                    _endGame = true;
                    Player maxPointClient = null;
                    int maxPoints = 0;

                    foreach (var item in _scores)
                    {
                        if (item.Value > maxPoints)
                        {
                            maxPoints = item.Value;
                            maxPointClient = item.Key;
                        }
                    }

                    Win(maxPointClient);
                }
            }
        }
    }

    // public bool IsAWinner(Player client)
    // {
    //     if (_scores.ContainsKey(client))
    //     {
    //         return _scores[client] >= maxScore;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

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
