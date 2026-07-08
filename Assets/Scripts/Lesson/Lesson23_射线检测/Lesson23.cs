using System;
using UnityEngine;

public class Lesson23 : MonoBehaviour
{
    public float offsetY;
    private RaycastHit hitInfo;
    private Transform nowPos;
    private void Update()
    {
        #region 第一题

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray r1 = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            ResourcesMgr.Instance.LoadRes<GameObject>("Effect/HitEff",(a)=>
            {
                if ( Physics.Raycast(r1,out RaycastHit hit,1000,1<<LayerMask.NameToLayer("Monster")))
                {
                    Destroy(Instantiate(a,hit.point+hit.normal*0.5f,Quaternion.identity),4);
                    Destroy(Instantiate(Resources.Load<GameObject>("Effect/DDD"),hit.point+hit.normal*0.1f,Quaternion.identity),10);
                }
            });
        }

        #endregion
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo,1000,1<<LayerMask.NameToLayer("Player")))
            {
                nowPos = hitInfo.transform;
            }
        }

        if (Input.GetMouseButton(0)&&nowPos !=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo,1000,1<<LayerMask.NameToLayer("Default")))
            {
                nowPos.position = hitInfo.point+Vector3.up*offsetY;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            nowPos = null;
        }
    }
}
