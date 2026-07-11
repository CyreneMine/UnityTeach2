using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    private GameDataMgr()
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("music");
        rankData = JsonMgr.Instance.LoadData<RankData>("rank");
    }
    public MusicData musicData;
    public RankData rankData;

    #region 音频相关

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "music");
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.musicIsOpen = isOpen;
        BKMusic.Instance.SetMusicIsMute(musicData.musicIsOpen);
    }
    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.soundIsOpen = isOpen;
    }

    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        BKMusic.Instance.SetMusicValue(musicData.musicValue);
    }

    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;
    }

    #endregion

    #region 排行榜相关

    public void AddRank(string name,int time)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);
        rankData.rankList.Sort((a, b) =>
        {
            if (a.time > b.time)
                return -1;
            return 1;
        });
        if (rankData.rankList.Count > 20)
            rankData.rankList.RemoveAt(20);
        JsonMgr.Instance.SaveData(rankData, "rank");
    }

    #endregion
    
}
