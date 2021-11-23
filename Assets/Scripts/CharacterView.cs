using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CharacterView : MonoBehaviourPun
{
    [SerializeField] public TextMeshProUGUI nickNamePrefab;
    [SerializeField] public Vector3 offSet;
    private TextMeshProUGUI _nickname;
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _nickname = Instantiate(nickNamePrefab, GameObject.Find("PlayerUI").transform);
        _nickname.text = photonView.Owner.NickName;
    }

    void Update()
    {
        _nickname.transform.position = _camera.WorldToScreenPoint(transform.position + offSet);
    }

    void OnDestroy()
    {
        if (_nickname != null)
            Destroy(_nickname.gameObject);
    }
}
