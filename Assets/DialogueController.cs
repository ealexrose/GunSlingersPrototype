using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{

    public List<DialogueOption> dialogueOptions;
    public float evilMeter = 0;
    public TextMeshProUGUI evilDisplay;
    public bool optionsActive;
    public float minTimeBetween;
    public float maxTimeBetween;
    public float timeLeft = 6f;
    public static DialogueController instance;
    public float evilDecay;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (DialogueOption dialogueOption in dialogueOptions)
        {
            dialogueOption.HideDialogueOption();
        }
    }

    // Update is called once per frame
    void Update()
    {
        evilDisplay.text = evilMeter.ToString("0.00");
        if (!optionsActive)
            timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && !optionsActive) 
        {
            SetOptions();
        }
        if (Input.GetButtonDown("Jump"))
            SceneManager.LoadScene(0);

        if (evilMeter < 0) 
        {
            evilMeter = Mathf.Min(0, evilMeter += evilDecay * Time.deltaTime);
        }
    }
    

    public void PickOption(bool correctOption) 
    {
        foreach (DialogueOption dialogueOption in dialogueOptions)
        {
            dialogueOption.HideDialogueOption();
        }

        if (correctOption)
            evilMeter -= 0.35f;
        else
            evilMeter += 0.1f;

        timeLeft = Random.Range(minTimeBetween, maxTimeBetween);
        optionsActive = false;
    }

    public void SetOptions() 
    {
        float maxRed = UnityEngine.Random.Range(0.5f, 1f);
        float midRed = UnityEngine.Random.Range(0.25f, maxRed * 0.75f);
        float minRed = UnityEngine.Random.Range(0f, midRed * 0.75f);
        List<float> redColors = new List<float> { maxRed, midRed, minRed };
        List<int> numOptions = new List<int> { 0, 1, 2 };
        numOptions.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < 3; i++) 
        {
            dialogueOptions[numOptions[i]].ShowDialogueOption(redColors[i], Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), i == 0);
        }

        optionsActive = true;
    }
}
