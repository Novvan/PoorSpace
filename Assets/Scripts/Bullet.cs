﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    private float _currentLifeTime;
    private Player _owner;
    public Player Owner { get => _owner; set => _owner = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (!photonView.IsMine) Destroy(this);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (_currentLifeTime < _lifeTime) _currentLifeTime += Time.deltaTime;
            else PhotonNetwork.Destroy(photonView);
            _rb.velocity = transform.up * _speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        

    }
}