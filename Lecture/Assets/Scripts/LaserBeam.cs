using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Transform tr;
    private LineRenderer line;
    private RaycastHit hit;
    private float fireTime = 0f;
    private float nextFire = 1f;

    private void Start()
    {
        tr = GetComponent<Transform>();
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.enabled = true;
        line.SetWidth(0.1f, 0.02f);
    }

    private void Update()
    {
        Ray ray = new Ray(tr.position + (Vector3.up * 0.02f), tr.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        if (Input.GetMouseButton(0))
        {
            fireTime += Time.deltaTime;

            if (fireTime >= nextFire)
            {
                line.SetPosition(0, tr.InverseTransformPoint(ray.origin));
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    line.SetPosition(0, tr.InverseTransformPoint(hit.point));
                }
                else line.SetPosition(0, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
                StartCoroutine(this.ShowLaserBeam());
            }
        }
    }

    private IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
        line.enabled = false;
    }
}