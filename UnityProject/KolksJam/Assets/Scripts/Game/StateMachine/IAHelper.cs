using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public static class IAHelper
{
    /// <summary>
    /// gets a distance based only on X and Z. IGNORE Y
    /// </summary>
    /// <param name="pPos1"></param>
    /// <param name="pPos2"></param>
    /// <returns></returns>
    static public float Distance2D(Vector3 pPos1, Vector3 pPos2)
    {
        pPos1.y = 0;
        pPos2.y = 0;
        return Vector3.Distance(pPos1, pPos2);
    }


    #region Random
    /// <summary>
    /// a managed random (on the future, it will be a pseudo random)
    /// </summary>
    /// <param name="iMin"></param>
    /// <param name="iMax"></param>
    /// <returns></returns>
    public static int RandomRange(int iMin, int iMax)
    {
        return UnityEngine.Random.Range(iMin, iMax);
    }

    #endregion


    /// <summary>
    /// normalizes the vector (+optimized than Vector3.normalize)
    /// </summary>
    /// <param name="vVec"></param>
    /// <returns></returns>
    static public Vector3 Normalize(Vector3 vVec)
    {
        float distance = Mathf.Sqrt(vVec.x * vVec.x + vVec.y * vVec.y + vVec.z * vVec.z);
        return new Vector3(vVec.x / distance, vVec.y / distance, vVec.z / distance);
    }



    /// <summary>
    /// get magnetude
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    static public float XZSqrMagnitude(Vector3 a, Vector3 b)
    {
        float dx = b.x - a.x;
        float dz = b.z - a.z;
        return dx * dx + dz * dz;
    }


    /// <summary>
    /// gets the bigger value between 2 
    /// </summary>
    /// <param name="fValueA"></param>
    /// <param name="fValueB"></param>
    /// <returns></returns>
    static public float GetMaxf(float fValueA, float fValueB)
    {
        return fValueA > fValueB ? fValueA : fValueB;
    }
    static public float GetMinf(float fValueA, float fValueB)
    {
        return fValueA < fValueB ? fValueA : fValueB;
    }
    static public int GetMinI(int fValueA, int fValueB)
    {
        return fValueA < fValueB ? fValueA : fValueB;
    }
    /// <summary>
    /// gets abs
    /// </summary>
    /// <param name="fValue"></param>
    /// <returns></returns>
    static public float GetAbs(float fValue)
    {
        return fValue > 0 ? fValue : -fValue;
    }


    /// <summary>
    /// shuffles the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

