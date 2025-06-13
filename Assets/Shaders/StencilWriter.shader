Shader "URP/StencilWriter2D"
{
    Properties
    {
        _BaseMap ("BaseMap", 2D) = "white" {}
    }


    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent" }

        Pass
        {
            Name "StencilPass"
            Tags { "LightMode"="SRPDefaultUnlit" }

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            ColorMask 0
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }
    }
}
