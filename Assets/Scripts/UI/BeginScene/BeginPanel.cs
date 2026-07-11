using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BeginPanel : BasePanel<BeginPanel>
{
    public Button btnStart,btnRank,btnSetting,btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener((() =>
        {
            HideMe();
        }));
        btnRank.onClick.AddListener((() =>
        {
            RankPanel.Instance.ShowMe();
        }));
        btnSetting.onClick.AddListener((() =>
        {
            SettingPanel.Instance.ShowMe();
        }));
        btnQuit.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
        
    }
}
