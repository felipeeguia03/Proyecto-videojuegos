Shader "Custom/AguaOndulante"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaterColor ("Water Color", Color) = (0.0, 0.5, 1.0, 0.8)
        _DeepWaterColor ("Deep Water Color", Color) = (0.0, 0.2, 0.6, 1.0)
        
        // Propiedades de las ondas
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.02
        _WaveFrequency ("Wave Frequency", Float) = 5.0
        
        // Ondas secundarias para más realismo
        _SecondWaveSpeed ("Second Wave Speed", Float) = 0.7
        _SecondWaveAmplitude ("Second Wave Amplitude", Float) = 0.015
        _SecondWaveFrequency ("Second Wave Frequency", Float) = 8.0
        
        // Distorsión
        _DistortionStrength ("Distortion Strength", Float) = 0.1
        
        // Transparencia
        _Alpha ("Alpha", Range(0, 1)) = 0.8
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue"="Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float2 worldPos : TEXCOORD1;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _WaterColor;
                float4 _DeepWaterColor;
                float _WaveSpeed;
                float _WaveAmplitude;
                float _WaveFrequency;
                float _SecondWaveSpeed;
                float _SecondWaveAmplitude;
                float _SecondWaveFrequency;
                float _DistortionStrength;
                float _Alpha;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                
                // Obtener la posición mundial para las ondas
                float3 worldPos = TransformObjectToWorld(input.positionOS.xyz);
                
                // Crear ondas usando funciones seno y coseno para movimiento natural
                float time = _Time.y;
                
                // Primera capa de ondas
                float wave1 = sin((worldPos.x * _WaveFrequency) + (time * _WaveSpeed)) * _WaveAmplitude;
                float wave2 = cos((worldPos.z * _WaveFrequency * 0.8) + (time * _WaveSpeed * 1.2)) * _WaveAmplitude * 0.7;
                
                // Segunda capa de ondas para más complejidad
                float wave3 = sin((worldPos.x * _SecondWaveFrequency * 1.3) + (time * _SecondWaveSpeed)) * _SecondWaveAmplitude;
                float wave4 = cos((worldPos.z * _SecondWaveFrequency * 0.9) + (time * _SecondWaveSpeed * 0.8)) * _SecondWaveAmplitude * 0.6;
                
                // Ondas diagonales para más naturalidad
                float wave5 = sin(((worldPos.x + worldPos.z) * _WaveFrequency * 0.6) + (time * _WaveSpeed * 0.9)) * _WaveAmplitude * 0.5;
                
                // Combinar todas las ondas
                float finalWave = wave1 + wave2 + wave3 + wave4 + wave5;
                
                // Aplicar el desplazamiento vertical
                input.positionOS.y += finalWave;
                
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.color = input.color;
                output.worldPos = worldPos.xz;
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float time = _Time.y;
                
                // Crear distorsión en las UVs para movimiento de superficie
                float2 distortion1 = float2(
                    sin((input.worldPos.x * 3.0) + (time * 2.0)) * _DistortionStrength,
                    cos((input.worldPos.y * 4.0) + (time * 1.5)) * _DistortionStrength
                );
                
                float2 distortion2 = float2(
                    cos((input.worldPos.x * 5.0) + (time * -1.8)) * _DistortionStrength * 0.5,
                    sin((input.worldPos.y * 6.0) + (time * -2.2)) * _DistortionStrength * 0.5
                );
                
                float2 finalUV = input.uv + distortion1 + distortion2;
                
                // Muestrear la textura con las UVs distorsionadas
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, finalUV);
                
                // Crear efecto de profundidad
                float depth = sin((input.worldPos.x * 2.0) + (input.worldPos.y * 2.0) + (time * 0.5)) * 0.5 + 0.5;
                
                // Interpolar entre colores de agua superficial y profunda
                half4 waterColor = lerp(_WaterColor, _DeepWaterColor, depth * 0.3);
                
                // Combinar con la textura
                half4 finalColor = texColor * waterColor * input.color;
                
                // Aplicar transparencia
                finalColor.a = _Alpha * input.color.a;
                
                return finalColor;
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/2D/Sprite-Unlit-Default"
} 