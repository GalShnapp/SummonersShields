using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _lifetime;
    private bool _isTargetingPlayer;
    
    private float _stopWatchTime;
    private GameObject _go;
    
    public void Init(float lifetime, float projectileSpeed, bool isUp)
    {
        _speed = projectileSpeed;
        if (!isUp)
        {
            _speed = -_speed;
        }
        _lifetime = lifetime;
        FireProjectile(this.gameObject);
    }

    public void FireProjectile(GameObject shot)
    {
        _go = shot;
        StartCoroutine(ProjectileCoroutine());
    }
    
    IEnumerator ProjectileCoroutine()
    {
        while (_stopWatchTime < _lifetime)
        {
            var myTransform = _go.transform;
            var position = myTransform.position;
            position = new Vector3(position.x, position.y +_speed, position.z);

            myTransform.position = position;

            _stopWatchTime += Time.deltaTime;
            
            yield return null;
        }

        if (_stopWatchTime >= _lifetime)
        {
            Destroy(_go);
        }
    }
    
}
