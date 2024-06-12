using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MyMath
{

    public static int RandRange2SidedGap(int max, int gap)
    {

        int rand = Random.Range(-max + gap, max - gap);

        if (rand >= 0)
        {

            return (rand + gap);

        }
        else
        {

            return (rand - gap);

        }
        
    }

    public static float RandRange2SidedGap(float max, float gap)
    {

        float rand = Random.Range(-max + gap, max - gap);

        if (rand >= 0)
        {

            return (rand + gap);

        }
        else
        {

            return (rand - gap);

        }

    }

    public static int Distance(Vector2Int point1, Vector2Int point2)
    {

        return (int)Mathf.Sqrt(Mathf.Pow((point2.x - point1.x), 2) + Mathf.Pow((point2.y - point1.y), 2));

    }

    public static float Distance(Vector2 point1, Vector2 point2)
    {

        return Mathf.Sqrt(Mathf.Pow((point2.x - point1.x), 2) + Mathf.Pow((point2.y - point1.y), 2));

    }

}
