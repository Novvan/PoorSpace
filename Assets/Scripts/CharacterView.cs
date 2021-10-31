using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterView : MonoBehaviourPun
{
    [SerializeField] TextMesh _nickNamePrefab;
    TextMesh _nickname;
    Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        _nickname = Instantiate<TextMesh>(_nickNamePrefab);
    }
}
