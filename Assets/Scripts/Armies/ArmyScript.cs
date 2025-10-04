using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class ArmyScript : MonoBehaviour, ITickable
{
    /// <summary>
    /// Units that are part of this army. 
    /// </summary>
    public List<BaseUnit> Units = new List<BaseUnit>();



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




    public float TotalSize { get
        {
            return Units.Sum(x => x.CurrentSize); 
        } }


    protected int coolDown; 

    public int Cooldown { get { return coolDown; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         coolDown = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DestroyArmy()
    {
        //need to insert stuff here. 
        Destroy(gameObject); 
    }

    public void Tick()
    {
    }

    public void EndTick()
    {
        if (coolDown > 0)
        {
            coolDown--;
        }
    }
}
