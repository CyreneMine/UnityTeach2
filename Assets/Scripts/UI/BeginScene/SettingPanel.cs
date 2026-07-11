using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel<SettingPanel>
{
    public Button btnClose;
    public Slider sldMusic,sldSound;
    public Toggle togMusic,togSound;
    public override void Init()
    {
        HideMe();
        btnClose.onClick.AddListener((() =>
        {
            HideMe();
        }));
        sldMusic.onValueChanged.AddListener(arg0 =>
        {
            GameDataMgr.Instance.SetMusicValue(arg0);
        });
        sldSound.onValueChanged.AddListener(arg0 =>
        {
            GameDataMgr.Instance.SetSoundValue(arg0);
        });
        togMusic.onValueChanged.AddListener(arg0 =>
        {
            GameDataMgr.Instance.SetMusicIsOpen(arg0);
        });
        togSound.onValueChanged.AddListener(arg0 =>
        {
            GameDataMgr.Instance.SetSoundIsOpen(arg0);
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        sldMusic.value = GameDataMgr.Instance.musicData.musicValue;
        sldSound.value = GameDataMgr.Instance.musicData.soundValue;
        togMusic.isOn = GameDataMgr.Instance.musicData.musicIsOpen;
        togSound.isOn = GameDataMgr.Instance.musicData.soundIsOpen;
    }

    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}
