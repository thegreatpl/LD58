using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class ArmyController : MonoBehaviour
{
    public static float EngagementRange = 1;  

    public enum ArmyBehaviour
    {
        Defend, //remain in place no matter what. 
        Guard, //wait until an enemy army gets close then close. 
        MoveTo //move to the specific position. 
            //probably ought to have attack move here. 
    }


    public ArmyBehaviour currentBehaviour;

    public ArmyScript ArmyScript;


    public Vector3 MoveToLocation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBehaviour = ArmyBehaviour.Defend;
        ArmyScript = GetComponent<ArmyScript>();
        MoveToLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int RunTick()
    {
        var nearby = GetAllVisionObjects(ArmyScript.SightDistance).Select(x => x.GetComponent<ArmyScript>());
        var nearestEnemy = nearby.Where(x => x.Faction != ArmyScript.Faction)
                    .OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();

        if (nearestEnemy != null)
        { 
            if (Vector3.Distance(transform.position, nearestEnemy.transform.position) < EngagementRange)
            {
                var prefab = GameManager.instance.PrefabManager.GetPrefab("Battle");
                var newobj = Instantiate(prefab); 
                newobj.transform.position = Vector3.Lerp(transform.position, nearestEnemy.transform.position, 0.5f);
                newobj.GetComponent<Battle>().CreateBattle(ArmyScript, nearestEnemy);
            }
        }



        switch (currentBehaviour)
        {
            case ArmyBehaviour.Defend:
                return 0;
            case ArmyBehaviour.Guard:

               
                if (nearestEnemy != null)
                {
                    return MoveTo(nearestEnemy.transform.position); 
                }

                return MoveTo(MoveToLocation); //go back to where it is supposed to be guarding. 


                break;
            case ArmyBehaviour.MoveTo:
                if (MoveToLocation != null)
                    return MoveTo(MoveToLocation);

                break;
            default:
                return 0;
        }


        return 0;
    }


    public int MoveTo(Vector3 targetPos)
    {
        var distance = targetPos - transform.position; 
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            return 0;

        transform.position += Vector3.ClampMagnitude(distance, 0.1f); 

        return (int)ArmyScript.MovementSpeed; //need to add in a map penalty. 
    }


    protected IEnumerable<GameObject> GetAllVisionObjects(float radius)
    {
        var results = Physics2D.OverlapCircleAll(transform.position, radius); 

        foreach (var obj in results)
        {
            yield return obj.gameObject;
        }

    }
}
