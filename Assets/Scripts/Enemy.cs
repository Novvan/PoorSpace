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
    private float _direction = 0;
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

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_direction != 0)
            {
                Move(_direction);
            }
            else
            {
                _direction = Random.Range(-1, 1);
            }
        }
    }

    public void Move(float dirx)
    {
        float curPosX = gameObject.transform.position.x;

        if ((curPosX >= 8.55f && _direction > 0) || (curPosX <= -8.55f && _direction < 0)) _direction = _direction * -1;
        else
        {
            _rb.velocity = new Vector2(dirx * _speed, 0);
        }
    }

    private void OnDestroy()
    {
        OnDestroyEnemy(this);
    }
}
