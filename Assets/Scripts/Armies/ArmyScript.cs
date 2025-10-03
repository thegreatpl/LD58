using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArmyScript : MonoBehaviour
{

    public List<BaseUnit> Units = new List<BaseUnit>();


    public int TotalSize { get
        {
            return Units.Sum(x => x.CurrentSize); 
        } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DestroyArmy()
    {
        
    }
}
