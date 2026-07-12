using System;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    private PlayerObject()
    {
        instance = this;
    }
    private float hValue;
    private float wValue;
    private bool isDead = false;
    private Vector3 nowPos;
    private Vector3 frontPos;
    private Quaternion targetRot;
    public float moveSpeed = 150f;
    public float roundSpeed = 0.5f;
    public int nowHp;
    public  int maxHp = 10;
    
    private void Start()
    {
        nowHp = maxHp;
    }

    private void Update()
    {
        wValue = Input.GetAxisRaw("Horizontal");
        hValue = Input.GetAxisRaw("Vertical");
        frontPos = transform.position;
        transform.Translate(Vector3.right * (wValue * Time.deltaTime * moveSpeed), Space.World);
        transform.Translate(Vector3.forward * (hValue * Time.deltaTime * moveSpeed));
        nowPos = Camera.main.WorldToScreenPoint(transform.position);
        if (nowPos.x <=0 || nowPos.x >=Screen.width)
        {
            transform.position = new Vector3(frontPos.x, transform.position.y,transform.position.z);
        }

        if (nowPos.y <= 0 || nowPos.y >= Screen.height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, frontPos.z);
        }
        if (wValue == 0)
            targetRot = Quaternion.identity;
        else
            targetRot = wValue < 0 ? Quaternion.AngleAxis(20f, Vector3.forward) : Quaternion.AngleAxis(-20f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot,roundSpeed * Time.deltaTime);
        
        
    }
    
    public void Wound()
    {
        if (isDead)
            return;
        nowHp--;
        GamePanel.Instance.ChangeHp(nowHp);
        if (nowHp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        GameOverPanel.Instance.ShowMe();
    }
}
