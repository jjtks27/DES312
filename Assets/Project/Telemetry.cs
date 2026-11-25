using UnityEngine;
using System.Collections.Generic;


public static class Telemetry
{
    static int shots;
    static int rejected;
    static int teleports;
    static int deaths;
    static bool finished;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResetRun()
    {
        shots = 0;
        rejected = 0;
        teleports = 0;
        deaths = 0;
        finished = false;

    }
    static float Elapsed
    {
        get { return Mathf.Round(Time.timeSinceLevelLoad * 100f) / 100f; }
    }
    static int PartOf(float z)
    {
        if (z < 67f) return 1;
        if (z < 131f) return 2;
        if (z < 219f) return 3;
        if (z < 323f) return 4;
        if (z < 415f) return 5;
        return 6;

    }
    static void Send(string eventName, Dictionary<string, object> data)
    {
        
    }
    public static void ShotFired(Vector3 playerPos)
    {
        shots++;
        Send("ShotsFired", new Dictionary<string, object>
        {
            {"Shot",shots },{"Part",PartOf(playerPos.z)},{"ElapsedTime",Elapsed},{"PlayerPos",playerPos}
        });

    }
    public static void ShotRejected(Vector3 playerPos)
    {
        rejected++;
        Send("ShotRejected",new Dictionary<string,object>{
            { "Rejected" ,rejected},
{ "Part",PartOf(playerPos.z)},
{"ElapsedTime",Elapsed}
        });
    }
    public static void Teleported(Vector3 from,Vector3 to)
    {
        teleports++;
        Send("Teleport", new Dictionary<string, object>{
            {"Teleports",teleports },{"Part",PartOf(to.z)},
            {"Distance",Mathf.Round(Vector3.Distance(from,to)*10f)/10f },
            {"HeightGained",Mathf.Round((to.y-from.y)*10f)/10f },
            {"ElapsedTime",Elapsed }
        } );
    }
    public static void Death(Vector3 playerPos)
    {
        deaths++;
        Send("PlayerDeath", new Dictionary<string, object>
        {
            {"Deaths",deaths },{"Part",PartOf(playerPos.z)},{"ElapsedTime",Elapsed},{"PlayerPos",playerPos}
        });
       
    }
    public static void LevelComplete()
    {
        if (finished) return;
        finished = true;
        Send("LevelComplete", new Dictionary<string, object>
        {
            {"TotalTime",Elapsed },
            {"Deaths",deaths },
            {"Shots",shots },
            {"Teleports",teleports },
            {"RejectedShots",rejected }
        });
    }
}