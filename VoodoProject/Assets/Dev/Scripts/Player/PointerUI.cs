using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerUI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 _distance;
    [SerializeField] bool isOffScreen;

    float _angle;

    private void FixedUpdate()
    {
        Vector3 toPos = target.position;
        Vector3 fromPos = Camera.main.transform.position;
        fromPos.z = 0;

        Vector2 borderSize = _distance*100;
        Vector3 targetOnScreen = Camera.main.WorldToScreenPoint(target.position);
        isOffScreen = targetOnScreen.x <= borderSize.x || targetOnScreen.x >= Screen.width- borderSize.x || targetOnScreen.y <= borderSize.y || targetOnScreen.y >= Screen.height- borderSize.y;

        if (isOffScreen)
        {
            //rotate to
            Vector3 dir = (toPos - fromPos);
            float _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localEulerAngles = new Vector3(0, 0, _angle - 90);

            Vector3 targetScreenPos = targetOnScreen;
            if (targetScreenPos.x <= borderSize.x) targetScreenPos.x = borderSize.x;
            if (targetScreenPos.y <= borderSize.y) targetScreenPos.y = borderSize.y;
            if (targetScreenPos.x >= Screen.width- borderSize.x) targetScreenPos.x = Screen.width- borderSize.x;
            if (targetScreenPos.y >= Screen.height- borderSize.y) targetScreenPos.y = Screen.height- borderSize.y;
            Vector3 pointWorldPos = Camera.main.ScreenToWorldPoint(targetScreenPos);
            transform.position = new Vector3(pointWorldPos.x, pointWorldPos.y, 0);
        }
        else
        {
            Vector3 targetScreenPos = targetOnScreen;
            Vector3 pointWorldPos = Camera.main.ScreenToWorldPoint(targetScreenPos);
            transform.position = new Vector3(pointWorldPos.x, pointWorldPos.y, 0);
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
    }
}
