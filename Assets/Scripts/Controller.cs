using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class Controller : MonoBehaviour
{
    [SerializeField] ServerManager _server;
    private Player _localPlayer;
    private Character _character;
    private Recorder _recorder;
    private bool _isLocked;

    void Start()
    {
        _localPlayer = PhotonNetwork.LocalPlayer;
        Player clientServer = _server.GetServer;
        _server.photonView.RPC("InitializedPlayer", _localPlayer, _localPlayer);
        _server.photonView.RPC("RequestGetPlayer", _localPlayer, _localPlayer);
        var chatManager = FindObjectOfType<ChatManager>();
        if (chatManager)
        {
            chatManager.OnSelect += Lock;
            chatManager.OnDeselect += UnLock;
        }
        _recorder = PhotonVoiceNetwork.Instance.PrimaryRecorder;
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
        if (_recorder != null)
        {
            if (Input.GetKey(KeyCode.V))
            {
                _recorder.TransmitEnabled = true;
            }
            else
            {
                _recorder.TransmitEnabled = false;
            }
        }
    }

    public Character SetCharacter
    {
        set 
        {
            _character = value;
        }
    }
}
