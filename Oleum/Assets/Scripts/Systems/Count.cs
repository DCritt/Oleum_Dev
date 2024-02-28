using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;

public class Count
{

    private int min;
    private int max;

    public Count(int min, int max)
    {

        this.min = min;
        this.max = max;

    }

    public int GetMin()
    {

        return min;

    }

    public int GetMax()
    {

        return max;

    }

    public bool CheckMax(int num)
    {

        return (num < max);

    }

    public bool CheckMin(int num)
    {

        return (num > min);

    }

}
