using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Faction PlayerFaction;

    public List<ArmyScript> SelectedArmies = new List<ArmyScript>();

    public List<CityScript> SelectedCities = new List<CityScript>();


    public Camera Camera;


    InputAction RightClick;

    InputAction Click;

    InputAction MousePosition; 


    bool selectInProgress;

    Vector3 selectStartPoint; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RightClick = InputSystem.actions.FindAction("RightClick");

        Click = InputSystem.actions.FindAction("Click");

        MousePosition = InputSystem.actions.FindAction("MousePosition");

        selectInProgress = false; 

        Camera = Camera.main;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Click.WasPressedThisFrame())
        {
            selectInProgress = true;
            selectStartPoint = Camera.ScreenToWorldPoint(MousePosition.ReadValue<Vector2>());
            SelectedArmies.Clear();
            SelectedCities.Clear();

        }

        if (!Click.IsPressed() && selectInProgress)
        {
            selectInProgress = false;
            var currentPos = Camera.ScreenToWorldPoint(MousePosition.ReadValue<Vector2>());

            var gameobj = GetAllWithinArea(selectStartPoint, currentPos);
            var armies = gameobj.Select(x => x.GetComponent<ArmyScript>()).Where(x => x?.Faction == PlayerFaction.Name);
            var citie = gameobj.Select(x => x.GetComponent<CityScript>()).Where(x => x?.FactionName == PlayerFaction.Name);
            SelectedArmies = armies.ToList(); 
            SelectedCities = citie.ToList();

        }
    }



    public IEnumerable<GameObject> GetAllWithinArea(Vector3 startPoint, Vector3 EndPoint)
    {
        var objs = Physics2D.OverlapBoxAll(startPoint, startPoint - EndPoint, 0);

        foreach (var obj in objs)
        {
            yield return obj.gameObject;

        }

    }
}
