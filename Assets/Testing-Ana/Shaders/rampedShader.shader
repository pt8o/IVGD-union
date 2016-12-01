Shader "Custom/rampedShader" {
	Properties {
		_Color("Diffuse Material Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Ramp ("Shading Ramp",2D) = "gray" {}
		
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		
		CGPROGRAM
		
		#pragma surface surf Ramp

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _Ramp;
		fixed4 _Color;
	   
	half4 LightingRamp(SurfaceOutput s, half3 lightDirection, half atten) {
		half NdotL = dot(s.Normal, lightDirection);
		half diff = NdotL * 0.5 + 0.5;
		half3 ramp = tex2D(_Ramp, float2(diff,1)).rgb;
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
		c.a = s.Alpha;
		return c;

		}

		struct Input {
			float2 uv_MainTex;
		};

		

		sampler2D _MainTex;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			
		}
		ENDCG
	}
	//FallBack "Diffuse"
	FallBack "VertexLit"
}
