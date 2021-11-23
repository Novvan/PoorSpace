using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Character : MonoBehaviourPun
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private Animator _anim;

    public Animator Anim { get => _anim; set => _anim = value; }

    // Start is called before the first frame update
    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector2 dir)
    {
        dir = dir.normalized;
        _rb.velocity = new Vector2(dir.x * _speed, dir.y * _speed);
    }
    public void Shoot(Player owner)
    {
        var temp = PhotonNetwork.Instantiate("PlayerBullet", transform.position, Quaternion.identity);
        temp.gameObject.GetComponent<Bullet>().Owner = owner;
    }
}
