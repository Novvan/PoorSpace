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
    private CharacterView _characterView;
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
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        float curPosX = _character.transform.position.x;
        float curPosY = _character.transform.position.y;

        if ((curPosX >= 8.55f && dir.x > 0) || (curPosX <= -8.55f && dir.x < 0)) dir.x = 0;
        if ((curPosY >= 4.55f && dir.y > 0) || (curPosY <= -4.55f && dir.y < 0)) dir.y = 0;
        if (dir.x != 0 || dir.y != 0)
        {
            _character.Anim.SetBool("moving", true);
        }
        else 
        {
            _character.Anim.SetBool("moving", false);
        }

        _server.photonView.RPC("RequestMove", _localPlayer, _localPlayer, dir);

        if (_recorder != null)
        {
            if (Input.GetKey(KeyCode.V))
            {
                _recorder.TransmitEnabled = true;
                _server.photonView.RPC("Talking", RpcTarget.All, _localPlayer, true);
            }
            else if(Input.GetKeyUp(KeyCode.V))
            {
                _recorder.TransmitEnabled = false;
                _server.photonView.RPC("Talking", RpcTarget.All, _localPlayer, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _server.photonView.RPC("RequestShoot", _server.GetServer, _localPlayer);
        }
    }
    

    public Character SetCharacter
    {
        set
        {
            _character = value;
        }
    }
    public CharacterView SetCharacterView 
    {
        set 
        {
            _characterView = value;
        }
    }
}
