using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public Button btnSure;
    public TMP_InputField name;
    public TMP_Text time;

    public override void Init()
    {
        HideMe();
        btnSure.onClick.AddListener((() =>
        {
            Time.timeScale = 1;
            GameDataMgr.Instance.AddRank(name.text.ToString(),(int)GamePanel.Instance.nowTime);
            SceneManager.LoadScene("BeginScene");
        }));

        
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
        time.text = GamePanel.Instance.labTime.text;
    }
}
