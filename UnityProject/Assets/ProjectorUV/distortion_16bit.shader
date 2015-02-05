// Shader created with Shader Forge Beta 0.23 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.23;sub:START;pass:START;ps:lgpr:1,nrmq:0,limd:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,uamb:True,mssp:True,ufog:False,aust:False,igpj:False,qofs:0,lico:1,qpre:1,flbk:,rntp:1,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0;n:type:ShaderForge.SFN_Final,id:1,x:32526,y:32658|emission-3-RGB;n:type:ShaderForge.SFN_Tex2d,id:3,x:32861,y:32954,ptlb:Rendertexture,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False|UVIN-21-OUT;n:type:ShaderForge.SFN_Append,id:21,x:33025,y:32954|A-1355-R,B-1355-G;n:type:ShaderForge.SFN_Tex2d,id:43,x:34276,y:32943,ptlb:UVmap,tex:1eca14b2376c6f44db501b804071d80b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:126,x:34178,y:32482,ptlb:UVmap_detail,tex:cf2b27263e151b04dbc5406ce275f169,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Vector1,id:145,x:33704,y:33370,v1:255;n:type:ShaderForge.SFN_Multiply,id:169,x:34094,y:32943|A-43-RGB,B-145-OUT;n:type:ShaderForge.SFN_Fmod,id:770,x:34178,y:32640|A-1222-OUT,B-771-OUT;n:type:ShaderForge.SFN_Vector1,id:771,x:34416,y:32620,v1:2;n:type:ShaderForge.SFN_Divide,id:1012,x:33454,y:32944|A-1221-OUT,B-145-OUT;n:type:ShaderForge.SFN_Add,id:1221,x:33617,y:32944|A-1222-OUT,B-1634-OUT;n:type:ShaderForge.SFN_Floor,id:1222,x:33941,y:32943|IN-169-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1355,x:33189,y:32944,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1012-OUT;n:type:ShaderForge.SFN_Subtract,id:1633,x:33999,y:32640|A-126-RGB,B-770-OUT;n:type:ShaderForge.SFN_Abs,id:1634,x:33826,y:32640|IN-1633-OUT;proporder:3-43-126;pass:END;sub:END;*/

Shader "Custom/distortion_16bit" {
    Properties {
        _Rendertexture ("Rendertexture", 2D) = "white" {}
        _UVmap ("UVmap", 2D) = "white" {}
        _UVmapdetail ("UVmap_detail", 2D) = "black" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _Rendertexture; uniform float4 _Rendertexture_ST;
            uniform sampler2D _UVmap; uniform float4 _UVmap_ST;
            uniform sampler2D _UVmapdetail; uniform float4 _UVmapdetail_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_1684 = i.uv0;
                float node_145 = 255.0;
                float3 node_1222 = floor((tex2D(_UVmap,TRANSFORM_TEX(node_1684.rg, _UVmap)).rgb*node_145));
                float2 node_1355 = ((node_1222+abs((tex2D(_UVmapdetail,TRANSFORM_TEX(node_1684.rg, _UVmapdetail)).rgb-fmod(node_1222,2.0))))/node_145).rg;
                float2 node_21 = float2(node_1355.r,node_1355.g);
                float3 emissive = tex2D(_Rendertexture,TRANSFORM_TEX(node_21, _Rendertexture)).rgb;
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
