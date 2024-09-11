using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFingerController : MonoBehaviour
{
    public bool lockedIn;
    public float moveBackSpeed;
    public float startY;
    public float endY;
    public float baseSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueController.instance.evilMeter >0 && !lockedIn)
        {
            MoveTowardsCompletion();
            if (MathF.Abs(transform.position.y - endY) < 0.25f)
            {
                LockFinger();
            }
        }
        else if (!lockedIn)
        {
            MoveTowardsStart();
        }
    }

    private void LockFinger()
    {
        lockedIn = true;
        transform.position = new Vector3(transform.position.x, endY, transform.position.z);
    }

    private void MoveTowardsStart()
    {
        float distanceToStart = startY - transform.position.y;
        float speed = Mathf.Clamp(moveBackSpeed, -distanceToStart, distanceToStart);
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void MoveTowardsCompletion()
    {
        float speed = baseSpeed * DialogueController.instance.evilMeter * Time.deltaTime;

        transform.position += Vector3.up * speed;
    }

}
