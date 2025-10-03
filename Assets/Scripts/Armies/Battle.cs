using System.Collections;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public ArmyScript AttackerArmy; 

    public ArmyScript DefenderArmy;

    public int CurrentPhase; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentPhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator CombatPhase()
    {
        if (CurrentPhase == 0)
        {
            //fire

            CurrentPhase++;
        }
        else if (CurrentPhase == 1)
        {
            //shock


            CurrentPhase++;
        }
        else if (CurrentPhase == 2)
        {
            //morale


            CurrentPhase = 0; 
        }


            yield return null;
    }
}
