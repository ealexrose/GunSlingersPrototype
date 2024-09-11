using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FingerController : MonoBehaviour
{

    public bool followingMouse;
    public bool lockedIn;
    public float maxMoveSpeed;
    public float mouseMoveScale;
    public float moveBackSpeed;
    public float startY;
    public float endY;
    public float evilScale;
    public float evilBase;
    public float evilDivider;
    public float evilAdjust;
    public float evilMultiplier;
    public Vector2 mouseVec;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckForClickedOnObject())
            {
                EnableMouseFollow();
            }
        }
        if ((Input.GetMouseButtonUp(0) || DialogueController.instance.optionsActive) && followingMouse)
        {
            DisableMouseFollow();
        }

        if (followingMouse && !lockedIn)
        {
            MoveTowardsMouse();
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

    private void MoveTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseOffset = mousePosition - transform.position;
        mouseVec = mouseOffset;
        float speed = mouseVec.y * mouseMoveScale * Time.deltaTime;
        speed = Mathf.Clamp(speed, -maxMoveSpeed, maxMoveSpeed);
        //float distanceToEnd = endY - transform.position.y;
        //speed = Mathf.Clamp(speed, -distanceToEnd, distanceToEnd);
        transform.position += Vector3.up * speed;
        float evilAdjustment = Mathf.Pow((evilBase * Time.deltaTime) + Mathf.Abs(speed * evilMultiplier), evilScale) / evilDivider;
        evilAdjust = evilAdjustment;
        DialogueController.instance.evilMeter += evilAdjust;
    }

    private void DisableMouseFollow()
    {
        followingMouse = false;
    }

    private void EnableMouseFollow()
    {
        followingMouse = true;
    }

    private bool CheckForClickedOnObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        return hit.Any(h => h.collider.transform == this.transform);
    }

}
