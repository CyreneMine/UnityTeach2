using System;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo info;
    private float time;
    private AudioSource audioSource;
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        Invoke("LifeDead",info.lifeTime);
        
    }

    public void LifeDead()
    {
        Destroy(gameObject);
    }
    public void Dead()
    {
        GameObject eff = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        eff.transform.position = transform.position;
        audioSource = eff.gameObject.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.soundIsOpen;
        audioSource.Play();
        Destroy(eff, 1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObject.Instance.Wound();
            Dead();
        }
    }

    private void Update()
    {
        time  += Time.deltaTime;
        transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        switch (info.type)
        {
            case 2:
                transform.Translate(Vector3.right * Mathf.Sin(time * info.roundSpeed)*info.rightSpeed*10*Time.deltaTime);
                break;
            case 3:
                transform.rotation *= Quaternion.AngleAxis(info.roundSpeed*Time.deltaTime*0.5f, Vector3.up);
                break;
            case 4:
                transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed*Time.deltaTime*0.5f, Vector3.up);
                break;
            case 5:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position), info.roundSpeed * Time.deltaTime);
                break;
        }
    }
    
}
