Shader "Custom/shaderTest" {

	SubShader {
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f{
				float4 pos:POSITION;
			};
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos=mul(UNITY_MATRIX_MVP,v.vertex);//投影矩阵运算是不可逆的
				return o;
			}			
			fixed4 frag(v2f IN):COLOR
			{
				return fixed4(1,1,1,0.8);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}