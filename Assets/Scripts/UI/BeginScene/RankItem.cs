using System;
using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public TMP_Text labRanking;
    public TMP_Text labName;
    public TMP_Text labTime;

    public void InitInfo(int rank, string labname, int labtime)
    {
        labRanking.text = rank+"";
        labName.text = labname;
        string time = "";
        if (labtime/3600 > 0)
            time += labtime / 3600+"h";
        if (labtime % 3600/60 > 0 || time !="")
            time += labtime % 3600 / 60 + "m";
        if (labtime % 60 > 0 || time !="")
            time += labtime % 60+"s";
        labTime.text = time;
    }
}
