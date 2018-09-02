﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using UnityEngine.SceneManagement;

class StartView : BaseUI
{
    public StartView()
    {
        _NaviData._Type = EUIType.FullScreen;
        _NaviData._Layer = EUILayer.FullScreen;
        _NaviData._IsCloseCoexistingUI = true;
    }
    
    public override void Open(NavigationData data = null)
    {
        base.Open(data);
    }

    internal override void Show()
    {
        base.Show();
        UIManager.Instance.Close<TopResidentUI>(); 
    }

    public void OnClickStart(int level)
    {
        UIManager.Instance.Close(this); 
        UIManager.Instance.ChangeScene(); 
        SceneManager.LoadScene(GameConfig.instance._PlaySceneName);
    }
}
