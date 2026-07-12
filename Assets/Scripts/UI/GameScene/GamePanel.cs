using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel<GamePanel>
{
    public Button btnBack;
    public TMP_Text labTime;
    public Toggle[] hps = new Toggle[10];
    public float nowTime = 0;
    public override void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            hps[i] = transform.Find($"hp/spr{i+1}").GetComponent<Toggle>();
        }
        btnBack.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            QuitPanel.Instance.ShowMe();
        });
        
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        string time = "";
        if ((int)nowTime/3600 > 0)
            time += (int)nowTime / 3600+"h";
        if ((int)nowTime % 3600/60 > 0 || time !="")
            time += (int)nowTime % 3600 / 60 + "m";
        if (nowTime % 60 > 0 || time !="")
            time += (int)nowTime % 60+"s";
        labTime.text = time;
    }

    public void ChangeHp(int hp)
    {
        for (int i = 0; i < hps.Length; i++)
        {
            hps[i].isOn = hp > i;
        }

        if (hp <=0)
        {
            GameOverPanel.Instance.ShowMe();
        }
    }
}
