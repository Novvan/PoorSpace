using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public delegate void myDelegate(Enemy enemy);
    public myDelegate OnDestroyPoke = delegate { };
    void OnDestroy()
    {
        OnDestroyPoke(this);
    }
}
