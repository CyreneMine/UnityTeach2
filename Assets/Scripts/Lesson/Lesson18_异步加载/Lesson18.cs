using UnityEngine;
using UnityEngine.UI;

public class Lesson18 : MonoBehaviour
{
    public Image img;
    void Start()
    {
        ResourcesMgr.Instance.LoadRes<Sprite>("BeLovedCyrene", (arg0 =>
        {
            img.sprite = arg0;
            img.SetNativeSize();
        }));
        StartCoroutine(ResourcesMgr.Instance.LoadResToIEnumerator<Sprite>("BeLovedCyrene", (arg0 =>
        {
            img.sprite = arg0;
            img.SetNativeSize();
        })));

    }
}
