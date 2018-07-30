﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSource : MonoBehaviour
{
    [SerializeField] float _generateInterval = 2;
    float _lastTime;

    void Update()
    {
        if (Time.time - _lastTime < _generateInterval)
        {
            return;
        }
        _lastTime = Time.time;
        var r = GameObject.Instantiate(GameAssets.instance._goldPrefab);
        var moveDir = Vector3.Normalize(PlanetController.instance.transform.position -
            new Vector3(1 - RandomUtil.instance.random.Next(10000) / 10000f * 2, 1 - RandomUtil.instance.random.Next(10000) / 10000f * 2, 0));
        r.SetData(this.transform.position, moveDir, 0.02f); 
    }
}