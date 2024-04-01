using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _lifetime;
    private bool _isTargetingPlayer;
    private Rigidbody2D rigidbody;
    
    private float _stopWatchTime;
    private GameObject _go;
    
    public void Init(float lifetime, float projectileSpeed)
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _speed = projectileSpeed;
        _lifetime = lifetime;
        FireProjectile(this.gameObject);
    }

    public void FireProjectile(GameObject shot)
    {
        _go = shot;
        rigidbody.AddForce(transform.up * _speed, ForceMode2D.Impulse);
        StartCoroutine(ProjectileCoroutine());
    }
    
    IEnumerator ProjectileCoroutine()
    {
        while (_stopWatchTime < _lifetime)
        {
            _stopWatchTime += Time.deltaTime;
            
            yield return null;
        }

        if (_stopWatchTime >= _lifetime)
        {
            Destroy(_go);
        }
    }
    
}
