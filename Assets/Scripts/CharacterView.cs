using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CharacterView : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI nickNamePrefab;
    [SerializeField] Vector3 offSet;
    TextMeshProUGUI _nickname;
    Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        _nickname = Instantiate<TextMeshProUGUI>(nickNamePrefab, GameObject.Find("PlayerUI").transform);
        _nickname.text = photonView.Owner.NickName;
    }
    void Update()
    {
        _nickname.transform.position = _camera.WorldToScreenPoint(transform.position + offSet);
    }
}
