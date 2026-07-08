using System;
using UnityEngine;

public class Lesson22 : MonoBehaviour
{
    public float moveSpeed = 10;
    public float roundSpeed = 10;
    private Collider[] colliders;
    private void Update()
    {
        // transform.Translate(Vector3.right * (Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed));
        transform.Translate(Vector3.forward * (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed));
        transform.Rotate(Vector3.up * (Input.GetAxis("Horizontal") * Time.deltaTime * roundSpeed));
        if (Input.GetKeyDown(KeyCode.J))
        {
            colliders = null;
            colliders = Physics.OverlapBox(
                transform.forward+transform.position, 
                Vector3.one*0.5f,
                transform.rotation,
                1<<LayerMask.NameToLayer("Monster"));
            AtkEnemy(colliders);
            print(colliders.Length);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            colliders = null;
            colliders = Physics.OverlapCapsule(
                transform.position, 
                transform.position+transform.forward*5,0.5f,
                1<<LayerMask.NameToLayer("Monster"));
            AtkEnemy(colliders);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            colliders = null;
            colliders = Physics.OverlapSphere(
                transform.position,
                10,
                1<<LayerMask.NameToLayer("Monster"));
            AtkEnemy(colliders);
        }
    }

    private void AtkEnemy(Collider[] enemies)
    {
        foreach (Collider enemy in enemies)
        {
            print($"怪物: {enemy.name}受伤");
        }
    }
}
