using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class RankPanel : BasePanel<RankPanel>
{
    public ScrollRect svList;
    public Button btnClose;
    private List<RankItem> rankItems = new List<RankItem>();
    public override void Init()
    {
        HideMe();
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        for (int i = 0; i < list.Count; i++)
        {
            if (rankItems.Count > i)
            {
                rankItems[i].InitInfo(i+1, list[i].name,list[i].time);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"), svList.content, false);
                RankItem item = obj.GetComponent<RankItem>();
                item.InitInfo(i+1, list[i].name, list[i].time);
                rankItems.Add(item);
            }
        }
    }
}
