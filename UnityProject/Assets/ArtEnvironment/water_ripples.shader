// Shader created with Shader Forge Beta 0.23 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.23;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,uamb:True,mssp:True,ufog:False,aust:True,igpj:True,qofs:0,lico:1,qpre:3,flbk:,rntp:2,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0;n:type:ShaderForge.SFN_Final,id:1,x:32213,y:32694|emission-123-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:2,x:33311,y:32784,ptlb:node_2,tex:d0e3430620122dd468dc25974f6d05fb;n:type:ShaderForge.SFN_Tex2d,id:3,x:32936,y:32718,tex:d0e3430620122dd468dc25974f6d05fb,ntxv:0,isnm:False|UVIN-37-UVOUT,TEX-2-TEX;n:type:ShaderForge.SFN_Tex2d,id:4,x:32892,y:32944,tex:d0e3430620122dd468dc25974f6d05fb,ntxv:0,isnm:False|UVIN-14-UVOUT,TEX-2-TEX;n:type:ShaderForge.SFN_Panner,id:14,x:33148,y:32969,spu:-0.01,spv:-0.052|UVIN-78-UVOUT;n:type:ShaderForge.SFN_Panner,id:37,x:33129,y:32611,spu:0.01,spv:-0.0751|UVIN-69-UVOUT;n:type:ShaderForge.SFN_Add,id:38,x:32748,y:32799|A-3-RGB,B-4-RGB;n:type:ShaderForge.SFN_TexCoord,id:69,x:33411,y:32590,uv:0;n:type:ShaderForge.SFN_Rotator,id:78,x:33374,y:32984|UVIN-89-UVOUT,ANG-94-OUT;n:type:ShaderForge.SFN_Sin,id:87,x:33758,y:33074|IN-88-TSL;n:type:ShaderForge.SFN_Time,id:88,x:33921,y:33067;n:type:ShaderForge.SFN_TexCoord,id:89,x:33545,y:32919,uv:0;n:type:ShaderForge.SFN_Divide,id:94,x:33532,y:33094|A-87-OUT,B-99-OUT;n:type:ShaderForge.SFN_Vector1,id:99,x:33710,y:33294,v1:10;n:type:ShaderForge.SFN_Tex2d,id:110,x:32720,y:32997,ptlb:node_110,tex:3a5a96df060a5cf4a9cc0c59e13486b7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:123,x:32489,y:32942|A-38-OUT,B-110-RGB,C-135-OUT;n:type:ShaderForge.SFN_ValueProperty,id:135,x:32648,y:33249,ptlb:node_135,v1:0.75;proporder:2-110-135;pass:END;sub:END;*/

Shader "Custom/water_ripples" {
    Properties {
        _node2 ("node_2", 2D) = "white" {}
        _node110 ("node_110", 2D) = "white" {}
        _node135 ("node_135", Float ) = 0.75
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node2; uniform float4 _node2_ST;
            uniform sampler2D _node110; uniform float4 _node110_ST;
            uniform float _node135;
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
                float4 node_148 = _Time + _TimeEditor;
                float2 node_37 = (i.uv0.rg+node_148.g*float2(0.01,-0.0751));
                float4 node_88 = _Time + _TimeEditor;
                float node_78_ang = (sin(node_88.r)/10.0);
                float node_78_spd = 1.0;
                float node_78_cos = cos(node_78_spd*node_78_ang);
                float node_78_sin = sin(node_78_spd*node_78_ang);
                float2 node_78_piv = float2(0.5,0.5);
                float2 node_78 = (mul(i.uv0.rg-node_78_piv,float2x2( node_78_cos, -node_78_sin, node_78_sin, node_78_cos))+node_78_piv);
                float2 node_14 = (node_78+node_148.g*float2(-0.01,-0.052));
                float2 node_149 = i.uv0;
                float3 emissive = ((tex2D(_node2,TRANSFORM_TEX(node_37, _node2)).rgb+tex2D(_node2,TRANSFORM_TEX(node_14, _node2)).rgb)*tex2D(_node110,TRANSFORM_TEX(node_149.rg, _node110)).rgb*_node135);
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
