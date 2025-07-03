Shader "Custom/WireframeTransparentGreen"
{
    Properties
    {
        _WireColor ("Wire Color", Color) = (0, 1, 0, 1) // Зелёный
        _WireWidth ("Wire Width", Range(0, 0.1)) = 0.01 // Толщина рёбер
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha // Прозрачность

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha

        struct Input
        {
            float3 worldPos;
        };

        fixed4 _WireColor;
        float _WireWidth;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Оставляем только рёбра (проволочная сетка)
            float3 barys = float3(IN.worldPos.x % 1.0, IN.worldPos.y % 1.0, IN.worldPos.z % 1.0);
            float closest = min(min(barys.x, barys.y), barys.z);
            if (closest > _WireWidth)
                discard; // Удаляем пиксели вне рёбер

            o.Albedo = _WireColor.rgb;
            o.Alpha = _WireColor.a;
        }
        ENDCG
    }
    FallBack "Transparent"
}