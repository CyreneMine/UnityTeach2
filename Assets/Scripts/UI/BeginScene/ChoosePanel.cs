using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public Button btnClose,btnLeft,btnRight,btnStart;
    public RectTransform heroPos;
    public Toggle[] togHps;
    public Toggle[] togSpeeds;
    public Toggle[] togVolumes;
    private GameObject nowHeroObj;
    public override void Init()
    {
        togHps =  new Toggle[10];
        togSpeeds = new Toggle[10];
        togVolumes = new Toggle[10];
        for (int i = 0; i < 10; i++)
        {
            togHps[i] = transform.Find($"sprPanel/labHp/sprs/spr{i+1}").GetComponent<Toggle>();
            togSpeeds[i] = transform.Find($"sprPanel/labSpeed/sprs/spr{i+1}").GetComponent<Toggle>();
            togVolumes[i] = transform.Find($"sprPanel/labVolume/sprs/spr{i+1}").GetComponent<Toggle>();
        }
        btnClose.onClick.AddListener((() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        btnStart.onClick.AddListener((() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        btnLeft.onClick.AddListener((() =>
        {
            GameDataMgr.Instance.nowSelHeroIndex--;
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.list.Count - 1;
            ChangeNowHero();
        }));
        btnRight.onClick.AddListener((() =>
        {
            GameDataMgr.Instance.nowSelHeroIndex++;
            if (GameDataMgr.Instance.nowSelHeroIndex >= GameDataMgr.Instance.roleData.list.Count-1)
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            ChangeNowHero();
        }));
        HideMe();
    }

    public void ChangeNowHero()
    {
        DestroyObj();
        RoleInfo nowHero = GameDataMgr.Instance.GetNowHeroData();
        nowHeroObj = Instantiate(Resources.Load<GameObject>($"Airplane/Airplane{GameDataMgr.Instance.nowSelHeroIndex+1}"), heroPos.transform, true);
        nowHeroObj.transform.localScale = Vector3.one * nowHero.scale;
        nowHeroObj.transform.localPosition = Vector3.zero;
        nowHeroObj.transform.localRotation = Quaternion.identity;
        nowHeroObj.layer = LayerMask.NameToLayer("UI");
        for (int i = 0; i < 10; i++)
        {
            togHps[i].isOn = nowHero.hp > i;
            togSpeeds[i].isOn = nowHero.speed > i;
            togVolumes[i].isOn = nowHero.volume > i;
        }
        
    }

    public void DestroyObj()
    {
        if (nowHeroObj != null)
        {
            Destroy(nowHeroObj);
            nowHeroObj = null;
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero();
    }

    public override void HideMe()
    {
        base.HideMe();
        DestroyObj();
    }

    private float time;
    private bool isSel = false;
    void Update()
    {
        time += Time.deltaTime;
        nowHeroObj.transform.Translate(Vector3.up*Mathf.Sin(time)*0.01f,Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 1000,1<<LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }

        if (Input.GetMouseButton(0)&&isSel)
        {
            heroPos.transform.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * 10, Vector3.up);
        }
    }
}
