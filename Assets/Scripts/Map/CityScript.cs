using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CityScript : MonoBehaviour, ITickable
{
    public Faction Faction; 

    public string FactionName;

    public int Cooldown => 0 ;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetFaction(Faction faction)
    {
        Faction = faction;
        FactionName = faction.Name;
    }


    public void EndTick()
    {

    }

    public void MonthTick()
    {
        //income here. 
    }

    public void Tick()
    {

    }

    public void YearTick()
    {
    }
}
