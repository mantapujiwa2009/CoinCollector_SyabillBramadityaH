Shader "Custom/Shader_baru"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (48, 181, 71, 1)
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"}

        Pass
        {
            Name "Unlit2D"
            Tags { "LightMode" = "Universal2D" }
            
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            CBUFFER_START(UnityPerMaterial)
                float4 _Warna;
            CBUFFER_END


            struct Attributes
            {
                float4 posisiObjek : POSITION;
            };

            struct KeFragment
            {
                float4 posisiClip : SV_POSITION;
            };

            KeFragment vert(Attributes IN)
            {
                KeFragment OUT;
                OUT.posisiClip = TransformObjectToHClip(IN.posisiObjek.xyz);
                return OUT;
            }

            half4 frag(KeFragment IN) : SV_Target
            {
                return (half4)_Warna;
            }
            ENDHLSL
        }
    }
}
