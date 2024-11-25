using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _shootPoint;
    public void RotateToTarget(Transform target, float rotationSpeed)
    {
        Vector3 dir = (target.position - transform.position);
        float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion desireRotation = Quaternion.Euler(0, 0, _angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, desireRotation, rotationSpeed * Time.deltaTime);
    }

    public void ShootFrom(Transform bullet)
    {
        bullet.position = _shootPoint.position;
        bullet.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
    }
}
