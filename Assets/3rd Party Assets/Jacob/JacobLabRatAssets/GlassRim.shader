Shader "Custom/GlassRim" {
 
     Properties 
     {
       _InnerColor ("Inner Color", Color) = (1.0, 1.0, 1.0, 1.0)
       _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
       _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
_Alpha ("Alpha (A)", 2D) = "white" {}
     }
     SubShader 
     {
       Tags {  "RenderType" = "Transparent" "Queue" = "Transparent" }
       
     //  Cull Back
       ZWrite Off
      Blend One One
    // Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB





       CGPROGRAM
       #pragma surface surf Lambert
       
       struct Input 
       {
           float3 viewDir;
       };


        
       float4 _InnerColor;
       float4 _RimColor;
       float _RimPower;
       
       void surf (Input IN, inout SurfaceOutput o) 
       {
           o.Albedo = _InnerColor.rgb;
           half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
           o.Emission = _RimColor.rgb * pow (rim, _RimPower);




       }




       ENDCG

        Pass {
            SetTexture[_MainTex] {
                Combine texture
            }
        SetTexture[_Alpha] {
    Combine previous * texture 
}
        }

        
     } 
     Fallback "Diffuse"
   }