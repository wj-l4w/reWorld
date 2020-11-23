using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TalentTree : NetworkBehaviour
{
    public int points = 0;
    public uint playerId;

    public Talent[] talents;

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

        for(int i = 0; i < talents.Length; i++)
        {
            int x = i;
            Button talentButtons = talents[i].GetComponent<Button>();
            talentButtons.onClick.AddListener(() => UseTalent(talentButtons.GetComponent<Talent>(), x));
        }
    }


    public void UseTalent(Talent talent, int talentId)
    {
        if (MyPoints > 0 )
        {
            switch(talentId){
                case 0:
                    talent.GetComponent<DmgBuff>().Click();
                    break;

                case 1:
                    talent.GetComponent<HealthRegen>().Click();
                    break;

                case 2:
                    talent.GetComponent<MoveSpeedBuff>().Click();
                    break;

                default:
                    Debug.Log("Trying to call Use Talent with out of range talentId (" + talentId + ")");
                    break;

            }


            MyPoints--;
        }
    }
    
    private void resetTalents()
    {
        UpdateTalentPointText();
    }

    public void UpdateTalentPointText()
    {
        talentPointText.text = points.ToString();
    }

    public void closeTalentTree()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void levelUp()
    {
        points++;
        UpdateTalentPointText();
    }
}
