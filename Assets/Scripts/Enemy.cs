using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Enemy : MonoBehaviourPun
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _speed;
    private float _currentHealth;
    private Rigidbody2D _rb;
    public delegate void myDelegate(Enemy enemy);
    public myDelegate OnDestroyEnemy = delegate { };
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnDestroy()
    {
        OnDestroyEnemy(this);
    }
}
