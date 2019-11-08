using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class MarchingSquares : MonoBehaviour
{
    // TODO(Rok Kos): Change to private when done testing
    public byte LineLookUp(bool[,] square)
    {
        Assert.IsTrue(square.GetLength(0) >= 2, "Square Lenght must be at least 2");
        Assert.IsTrue(square.GetLength(1) >= 2, "Square Height must be at least 2");
        
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

    public byte[,] ParseGrid(bool[,] grid)
    {
        Assert.IsTrue(grid.GetLength(0) >= 2, "Grid Lenght must be at least 2");
        Assert.IsTrue(grid.GetLength(1) >= 2, "Grid Height must be at least 2");
        
        byte[,] res = new byte[grid.GetLength(0) - 1,grid.GetLength(1) - 1];
        for (int y = 0; y < grid.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < grid.GetLength(1) - 1; x++)
            {
                bool[,] square = new bool[2, 2]
                {
                    {grid[y, x], grid[y, x + 1]},
                    {grid[y + 1, x], grid[y + 1, x + 1]}
                };
                res[y, x] = LineLookUp(square);
            }
        }

        return res;    
    }

    public String ResultToString(byte[,] res)
    {
        String s = "";
        for (int y = 0; y < res.GetLength(0); y++)
        {
            for (int x = 0; x < res.GetLength(1); x++)
            {
                s += String.Format("{0, 0:D2} ", res[y,x]);
            }

            s += "\n";
        }

        return s;
    }
}

