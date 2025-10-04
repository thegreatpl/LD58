using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<ITickable> TimeEntities = new List<ITickable>();

    public bool IsPaused = false;


    public int MaxUpdated = 20;

    public float TimeBetweenTicks = 2.5f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeEntities.RemoveAll(x => x == null || x.Equals(null)); 
    }


    public IEnumerator RunTime()
    {
        while (true)
        {
            while (IsPaused) {
                yield return null;
            }

            var currentTick = TimeEntities.Where(x => x != null || !x.Equals(null));
            int currentupdated = 0; 
            foreach (var t in currentTick)
            {
                if (t == null || t.Equals(null))
                    continue;

                try
                {
                    t.Tick(); 
                }
                catch(Exception e)
                {
                    Debug.LogError(e);
                }


                currentupdated++;
                if (currentupdated > MaxUpdated)
                {
                    yield return null; 
                    currentupdated = 0;
                }
            }

            yield return new WaitForSeconds(TimeBetweenTicks);
            
        }
    }
}
