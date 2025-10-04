using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitManager : MonoBehaviour
{
    public List<UnitTemplate> Units = new List<UnitTemplate>();

    Dictionary<string, UnitTemplate> UnitTypes = new Dictionary<string, UnitTemplate>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadUnits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadUnits()
    {
        foreach (var unit in Units)
        {
            UnitTypes.Add(unit.TypeName, unit);
        }
        
    }

    public BaseUnit BuildUnit(string unitType)
    {
        if (UnitTypes.ContainsKey(unitType))
        {
            var unit = UnitTypes[unitType].BaseUnit;
            return new BaseUnit()
            {
                Name = unit.Name,
                FireAttk = unit.FireAttk,
                CurrentSize = unit.MaxSize, 
                MaxSize = unit.MaxSize,
                FireDefense = unit.FireDefense,
                MoraleAttk = unit.MoraleAttk,
                MoraleDefense = unit.MoraleDefense,
                ShockAttk = unit.ShockAttk,
                ShockDefense = unit.ShockDefense,
                Type = unit.Type    
            };
        }
        return null;
    }


}
