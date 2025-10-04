using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class ArmyScript : MonoBehaviour, ITickable
{

    public string Faction;

    public ArmyController ArmyController; 

    /// <summary>
    /// Units that are part of this army. 
    /// </summary>
    public List<BaseUnit> Units = new List<BaseUnit>();


    public Battle CurrentBattle; 


    #region Bonuses
    public float FireAttkBonus
    {
        get
        {
            return 0; 
        }
    }

    public float ShockAttkBonus
    {
        get { return 0; }
    }

    public float MoraleAttkBonus
        { get { return 0; } }

    #endregion


    public float TotalSize { get
        {
            return Units.Sum(x => x.CurrentSize); 
        } }

    /// <summary>
    /// how fast this army moves. Lower is better. 
    /// </summary>
    public float MovementSpeed
    {

        get
        {
           return Units.Sum(x => x.MovementSpeed);
        }
    }


    public float SightDistance
    {
        get
        {
            return 3; 
        }
    }


    protected int coolDown; 

    public int Cooldown { get { return coolDown; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         coolDown = 0;
        ArmyController = GetComponent<ArmyController>(); 
        GameManager.instance.TimeManager.TimeEntities.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DestroyArmy()
    {
        //need to insert stuff here to handle destroying armies. 
        Destroy(gameObject); 
    }

    public void Tick()
    {
        if (CurrentBattle != null)
        {
            return;
        }

        coolDown = ArmyController.RunTick(); 
    }

    public void EndTick()
    {
        if (coolDown > 0)
        {
            coolDown--;
        }
    }

    public void MonthTick()
    {
    }

    public void YearTick()
    {
    }
}
