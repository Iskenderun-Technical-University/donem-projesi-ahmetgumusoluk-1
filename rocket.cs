using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject vfx;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 pos = transform.position;
        Collider[] sphere = Physics.OverlapSphere(pos, 3);

        foreach (Collider hitObject in sphere)
        {
            if (hitObject.transform.CompareTag("Enemy"))
            {
                hitObject.GetComponent<Spider>().HealthDown(100);
            }
        }
        GameObject newVfx = Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(newVfx, 3.0f);
        Destroy(this.gameObject);
    }
}
