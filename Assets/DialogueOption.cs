using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    public DialogueController DialogueController;
    int id;
    SpriteRenderer sr;
    public bool correctAnswer;
    // Start is called before the first frame update
    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckForClickedOnObject())
            {
                Debug.Log("Picked this one!");
                ChooseOption();
            }
        }
    }

    private void ChooseOption()
    {
        DialogueController.instance.PickOption(correctAnswer);
    }

    public void ShowDialogueOption(float r, float g, float b, bool correctOption)
    {
        sr.enabled = true;
        sr.color = new Color(r, g, b);
        correctAnswer = correctOption;
    }

    public void HideDialogueOption()
    {
        sr.enabled = false;
    }

    private bool CheckForClickedOnObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        return hit.Any(h => h.collider.transform == this.transform);
    }
}


