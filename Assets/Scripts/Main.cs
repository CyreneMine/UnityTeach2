using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        RoleInfo roleInfo = GameDataMgr.Instance.GetNowHeroData();
        GameObject obj = Instantiate(Resources.Load<GameObject>(roleInfo.resName));
        PlayerObject playerObject = obj.AddComponent<PlayerObject>();
        playerObject.moveSpeed = roleInfo.speed*40;
        playerObject.roundSpeed = 20;
        playerObject.maxHp = roleInfo.hp;
        obj.tag = "Player";
        GamePanel.Instance.ChangeHp(roleInfo.hp);
    }
}
