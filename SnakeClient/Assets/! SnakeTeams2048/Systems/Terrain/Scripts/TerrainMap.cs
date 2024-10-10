using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TerrainMap
{
    public HexagonBounds Bounds { get; }
    public Vector2Int[] Data { get; }

    public TerrainMap(HexagonBounds bounds, Vector2Int[] data)
    {
        Bounds = bounds;
        Data = data;
    }

    public void BuildWith(IMapBuilder builder, float scale)
    {
        builder.Build(this, scale);
    }
}
