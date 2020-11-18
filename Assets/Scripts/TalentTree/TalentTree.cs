using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentTree : MonoBehaviour
{
    private int points = 10;

    [SerializeField]
    private Talent[] talents;

    [SerializeField]
    private Text talentPointText;

    

    // Start is called before the first frame update

    public int MyPoints
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
            UpdateTalentPointText();
        }
    }

    void Start()
    {
        resetTalents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseTalent(Talent talent)
    {
        if (MyPoints > 0 && talent.Click())
        {
            MyPoints--;
        }
    }
    
    private void resetTalents()
    {
        UpdateTalentPointText();
        /* foreach (Talent talent in talents)
        {
            talent.Lock();
        } */
    }

    private void UpdateTalentPointText()
    {
        talentPointText.text = points.ToString();
    }
}
