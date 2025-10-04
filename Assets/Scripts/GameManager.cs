using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    public TimeManager TimeManager;

    public UnitManager UnitManager;

    public PrefabManager PrefabManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        instance = this;

        TimeManager = GetComponent<TimeManager>();
        PrefabManager = GetComponent<PrefabManager>();
        UnitManager = GetComponent<UnitManager>();

        StartCoroutine(StartGame()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartGame()
    {
        StartCoroutine(TimeManager.RunTime()); 
        yield return null;
    }
}
