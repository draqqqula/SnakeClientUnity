using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapBuilder : MonoBehaviour, IMapBuilder
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _tileBase;
    private HexagonBounds _mapBounds = HexagonBounds.Empty;

    public void Reset()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void Build(TerrainMap map, float scale)
    {
        var tileArray = new TileBase[map.Data.Length];
        Array.Fill(tileArray, _tileBase);
        _tilemap.SetTiles(map.Data.Select(it => (Vector3Int)it).ToArray(), tileArray);
        _mapBounds = map.Bounds;

        var targetScale = (scale * 4f / MathF.Sqrt(3)) * 0.15f;
        _tilemap.transform.localScale = Vector3.one * targetScale;
    }

    public void Break(int[] tiles)
    {
        var tileArray = new TileBase[tiles.Length];
        _tilemap.SetTiles(tiles.Select(it => ToVector3Int(it)).ToArray(), tileArray);
    }

    private Vector3Int ToVector3Int(int tileIndex)
    {
        return new Vector3Int(_mapBounds.MinR + tileIndex % _mapBounds.SizeR, _mapBounds.MaxQ - tileIndex / _mapBounds.SizeR + 1);
    }
}
