using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct HexagonBounds
{
    public readonly int MinQ;
    public readonly int MinR;
    public readonly int MaxQ;
    public readonly int MaxR;
    public HexagonBounds(int startQ, int startR, int endQ, int endR)
    {
        MinQ = startQ;
        MinR = startR;
        MaxQ = endQ;
        MaxR = endR;
    }
    public int SizeQ => MaxQ - MinQ;
    public int SizeR => MaxR - MinR;

    public static HexagonBounds Empty => new HexagonBounds(0, 0, 0, 0);
}