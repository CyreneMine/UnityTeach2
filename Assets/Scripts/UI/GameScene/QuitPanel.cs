using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitPanel : BasePanel<QuitPanel>
{
    public Button btnQuit,btnSure;
    public override void Init()
    {
        HideMe();
        btnQuit.onClick.AddListener((() =>
        {
            HideMe();
            Time.timeScale = 1;
        }));
        btnSure.onClick.AddListener((() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        }));
    }
}
