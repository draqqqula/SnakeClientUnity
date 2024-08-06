Shader "Custom/TerrainMask"
{
    Properties
    {
        [IntRange] _StencilRef("StencilRef", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "Queue"="Geometry-1"
        }

        Blend Zero One
        Zwrite  Off

        Stencil
        {
            Ref [_StencilRef]
            Comp Always
            Pass Replace
        }

        Pass
        {
        }
    }
}
