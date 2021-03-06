﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UIFramework;
using UnityStandardAssets.CrossPlatformInput;

public class PlanetController : MonoSingleton<PlanetController>
{
    [SerializeField] Camera _Camera;
    [SerializeField] float _ScrollFactor = 50;
    float _HealthLapseSpeed = 5;
    public float HealthLapseSpeed
    {
        set
        {
            _HealthLapseSpeed = value;
        }
        get
        {
            return _HealthLapseSpeed;
        }
    }

    [SerializeField] Planet _planet;
    uint _DelayCallID;

    private void OnDestroy()
    {
        if (_DelayCallID != 0)
        {
            GameManager.instance.CancelDelayCall(_DelayCallID);
            _DelayCallID = 0;
        }
        EventDispatcher.instance.UnRegisterEvent(EventID.AddHealth, this, "AddHealth");
        EventDispatcher.instance.UnRegisterEvent(EventID.CreateTurret, this, "CreateTurret");
    }

    public void Init()
    {
        _planet.Init();
        EventDispatcher.instance.RegisterEvent(EventID.CreateTurret, this, "CreateTurret");
        EventDispatcher.instance.RegisterEvent(EventID.AddHealth, this, "AddHealth");
        _DelayCallID = GameManager.instance.DelayCall(HealthLapseSpeed, () =>
        {
            EventDispatcher.instance.DispatchEvent(EventID.AddHealth, -1);
            _DelayCallID = 0;
        }, true);
    }

    void Rotate(bool value)
    {
        _planet.rotate.enabled = value;
    }

    void CreateTurret(int degree, int turrectId)
    {
        _planet.CreateCannon(degree, turrectId);
    }

    public GameObject GetTurretPivot()
    {
        return _planet.GetTurretPivot();
    }

    void AddHealth(int value)
    {
        if (_planet.HP + value < _planet.MaxHP)
        {
            _planet.HP += value;
        }
        else
        {
            _planet.HP = _planet.MaxHP;
        }
    }

    void Update()
    {
        if (_planet == null)
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Rotate"))
        {
            Rotate(true);
        }
        if (CrossPlatformInputManager.GetButtonUp("Rotate"))
        {
            Rotate(false);
        }
#if UNITY_EDITOR || UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Rotate(false);
        }
        var wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
        {
            wheel *= _ScrollFactor;
            _Camera.fieldOfView = Mathf.Clamp(_Camera.fieldOfView + wheel, GameConfig.instance._MinFOV, GameConfig.instance._MaxFOV);
        }
#endif
        if (Input.touchCount == 1)
        {
            Ray ray = CameraController.Instance._MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Turret")))
            {
                var turret = hit.transform.GetComponent<Turret>();
                if (turret != null)
                {
                    turret.OnClick();
                }
                Debugger.Log("hit.name=" + hit.transform.name);
            }
        }
    }

    public bool IsInVisualField(Vector3 pos)
    {
        if (_planet == null)
        {
            return false;
        }
        return _planet.IsInVisualField(pos);
    }

    public float GetHP()
    {
        if (_planet == null)
        {
            return 0;
        }
        return _planet.HP;
    }

    public bool IsHpLessThanMax()
    {
        if (_planet == null)
        {
            return false;
        }
        return _planet.HP < _planet.MaxHP;
    }
}
