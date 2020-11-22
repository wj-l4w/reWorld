using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TalentTree : NetworkBehaviour
{
    private int points = 2;
    public uint playerId;

    [SerializeField]
    private Talent[] talents;

    [SerializeField]
    private Text talentPointText;

    

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

    void Update()
    {
        
    }

    public void UseTalent(Talent talent)
    {
        if (MyPoints > 0 && /*(*/talent.GetComponent<DmgBuff>().Click() /*|| talent.GetComponent<DmgBuff>().Click())*/)
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

    public void closeTalentTree()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
