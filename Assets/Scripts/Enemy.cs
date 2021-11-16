using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Enemy : MonoBehaviourPun
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;
    private float _currentHealth;
    private Rigidbody2D _rb;
    private GameManager _gm;
    public delegate void myDelegate(Enemy enemy);
    public myDelegate OnDestroyEnemy = delegate { };
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _gm = FindObjectOfType<GameManager>();
    }
    [PunRPC]
    public void GetDamage(float Damage, Player Instigator) 
    {
        _currentHealth -= Damage;
        if (_currentHealth <= 0) 
        {
            if (_gm != null) 
            {
                _gm.AddScore(Instigator);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        OnDestroyEnemy(this);
    }
}
