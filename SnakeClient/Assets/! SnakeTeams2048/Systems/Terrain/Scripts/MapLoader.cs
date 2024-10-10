using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

internal class MapLoader : MonoBehaviour, IMapLoader
{
    [SerializeField] private TextAsset[] _mapAssets;
    private Dictionary<string, TextAsset> _mapAssetsByName;

    private void Start()
    {
        _mapAssetsByName = _mapAssets.ToDictionary(it => it.name, it => it);
    }

    public TerrainMap Load(string name)
    {
        if (_mapAssetsByName.TryGetValue(name, out var map))
        {
            using var stream = new MemoryStream(map.bytes);
            using var reader = new BinaryReader(stream);

            var startQ = reader.ReadInt32();
            var startR = reader.ReadInt32();
            var endQ = reader.ReadInt32();
            var endR = reader.ReadInt32();
            var length = (endQ - startQ) * (endR - startR);

            var buffer = reader.ReadBytes(length / 8);

            var bounds = new HexagonBounds(startQ, startR, endQ, endR);
            var bitArray = new BitArray(buffer);

            var points = EnumeratePoints(bounds, bitArray).ToArray();

            return new TerrainMap(bounds, points);
        }
        return new TerrainMap(HexagonBounds.Empty, new Vector2Int[0]);
    }

    private IEnumerable<Vector2Int> EnumeratePoints(HexagonBounds bounds, BitArray bitArray)
    {
        var c = 0;
        for (int q = bounds.MaxQ + 1; q > bounds.MinQ + 1; q -= 1)
        {
            for (int r = bounds.MinR; r < bounds.MaxR; r += 1)
            {
                if (bitArray[c])
                {
                    yield return new Vector2Int(r, q);
                }
                c++;
                if (c >= bitArray.Length)
                {
                    yield break;
                }
            }
        }
    }
}