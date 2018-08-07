﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using UnityEngine.UI;

class TopResidentUI : BaseUI
{
    [SerializeField] Text _goldText;
    int _goldCount;
    public TopResidentUI()
    {
        _NaviData._Type = EUIType.Resident;
        _NaviData._Layer = EUILayer.Resident;
    }

    public override void Open(NavigationData data = null)
    {
        base.Open(data);
        EventDispatcher.instance.RegisterEvent(EventID.UpdateGold, this, "UpdateGold");
        UpdateGold(GameData.instance.goldCount);
    }

    internal override void Close()
    {
        EventDispatcher.instance.UnRegisterEvent(EventID.UpdateGold, this, "UpdateGold");
        base.Close();
    }

    public void UpdateGold(int value)
    {
        _goldCount = value;
        _goldText.text = _goldCount.ToString();
    }

    public void OnClickBack()
    {
        UIManager.Instance.PopupLastFullScreenUI(); 
    }
}