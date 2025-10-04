using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Battle : MonoBehaviour, ITickable
{
    public ArmyScript AttackerArmy; 

    public ArmyScript DefenderArmy;

    public int CurrentPhase;


    public CombatLine AttackerLine; 

    public CombatLine DefenderLine;

    public Queue<BaseUnit> AttackReserve; 

    public Queue<BaseUnit> DefenderReserve;

    //battles should never really have cooldown. 
    public int Cooldown => 0 ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentPhase = 0;
        AttackerLine = new CombatLine();
        DefenderLine = new CombatLine();
        AttackReserve = new Queue<BaseUnit>();
        DefenderReserve = new Queue<BaseUnit>();

        CreateBattle(AttackerArmy, DefenderArmy); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBattle(ArmyScript attacker, ArmyScript defender)
    {
        AttackerArmy = attacker;
        DefenderArmy = defender;

        foreach(var unit in attacker.Units)
            AttackReserve.Enqueue(unit);
        foreach(var unit in defender.Units)
            DefenderReserve.Enqueue(unit);


        RefillLines(AttackerLine, AttackReserve, 5);
        RefillLines(DefenderLine, DefenderReserve, 5);

        GameManager.instance.TimeManager.TimeEntities.Add(this); 

       // StartCoroutine(CombatPhase()); 
    }


    public int RollDice()
    {
        return Random.Range(1, 10); 
    }

    public void CombatPhase()
    {
        if (CurrentPhase == 0)
        {
            //fire
            foreach(var pos in AttackerLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.FireAttk + AttackerArmy.FireAttkBonus;

                var attacking = DefenderLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.FireDefense); 
            }
            foreach (var pos in DefenderLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.FireAttk + DefenderArmy.FireAttkBonus;

                var attacking = AttackerLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.FireDefense);

            }

            CurrentPhase++;
        }
        else if (CurrentPhase == 1)
        {
            //shock
            foreach (var pos in AttackerLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.ShockAttk + AttackerArmy.ShockAttkBonus;

                var attacking = DefenderLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.ShockDefense);
            }
            foreach (var pos in DefenderLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.ShockAttk + DefenderArmy.ShockAttkBonus;

                var attacking = AttackerLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.ShockDefense);
            }

            CurrentPhase++;
        }
        else if (CurrentPhase == 2)
        {
            //morale
            foreach (var pos in AttackerLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.MoraleAttk + AttackerArmy.MoraleAttkBonus;

                var attacking = DefenderLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.MoraleDefense);
            }
            foreach (var pos in DefenderLine.Line)
            {
                var unit = pos.Value.Unit;
                float attackStrength = RollDice() + unit.MoraleAttk + DefenderArmy.MoraleAttkBonus;

                var attacking = AttackerLine.GetTargetUnit(pos.Key);
                attacking.CurrentSize -= Mathf.Max(0, attackStrength - attacking.MoraleDefense);
            }

            CurrentPhase = 0;
        }

        foreach(var dead in AttackerLine.Line.Where(x => x.Value.Unit.CurrentSize < 1).ToList())
        {
            AttackerLine.Line.Remove(dead.Key); 
        }
        foreach (var dead in DefenderLine.Line.Where(x => x.Value.Unit.CurrentSize < 1).ToList())
        {
            DefenderLine.Line.Remove(dead.Key);
        }


        RefillLines(AttackerLine, AttackReserve, 5);
        RefillLines(DefenderLine, DefenderReserve, 5);

        if (AttackerLine.Line.Count < 1)
        {
            AttackerArmy.DestroyArmy();
        }
        if (DefenderLine.Line.Count < 1)
        {
            DefenderArmy.DestroyArmy();
        }

    }


    public void RefillLines(CombatLine combatLine, Queue<BaseUnit> Reserve, int linewidth)
    {
        for (int idx = -(linewidth / 2); idx < linewidth / 2; idx++)
        {
            if (!combatLine.Line.ContainsKey(idx) && Reserve.Count > 0)
            {
                combatLine.Line.Add(idx, new LinePosition()
                {
                    LinePositionLoc = idx,
                    Unit = Reserve.Dequeue()
                });
            }
        }
    }

    public void Tick()
    {
        CombatPhase(); 
    }

    public void EndTick()
    {
    }
}
public class CombatLine
{
    public Dictionary<int, LinePosition> Line = new Dictionary<int, LinePosition>(); 


    public BaseUnit GetTargetUnit (int attackpos)
    {
        if (Line.Count == 0)
            return null;

        if (Line.ContainsKey(attackpos))
            return Line[attackpos].Unit;

        int diff = 1; 

        while(true)
        {
            if (Line.ContainsKey(attackpos + diff))
                return Line[attackpos + diff].Unit;
            if (Line.ContainsKey(attackpos - diff))
                return Line[attackpos - diff].Unit;

            diff++;
        }
    }
}
public class LinePosition
{
    public BaseUnit Unit;

    public int LinePositionLoc; 
}