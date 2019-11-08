using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarchingSquares : MonoBehaviour
{
    public byte LineLookUp(bool[,] square)
    {
        int res = 0;
        
        res += square[0, 0] ? 1 : 0;
        res = res << 1;
        res += square[0, 1] ? 1 : 0;
        res = res << 1;
        res += square[1, 1] ? 1 : 0;
        res = res << 1;
        res += square[1, 0] ? 1 : 0;

        return (byte)res;
    }
}

