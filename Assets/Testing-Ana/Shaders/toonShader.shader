// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/toonShader" {
	//I've been following this tutorial to do this: https://www.youtube.com/watch?v=3qBDTh9zWrQ

	Properties{
		//all colors use RGBA values, here are the default properties.
		_Color("Diffuse Material Color", Color) = (1,1,1,1)
		_UnlitColor("Unlit Color", Color) = (0.5,0.5,0.5,1)
		_DiffuseThreshold("Lighting Threshold", Range(-1.1,1)) = 0.1
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_Shiny("Shiny", Range(0.5,1)) = 1
		_OutlineThickness("Outline Thickness", Range(0,1)) = 0.1
		_OutlineColor("Outline Color", Color)= (0,0,0,1)
		_MainTex("Main Texture", 2D) = "defaultTexture" {}
		

	}

		SubShader{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		

		CGPROGRAM

		//calls vertex shader
		#pragma vertex vert
		
		//calls fragment shader
		#pragma fragment frag
		
		

		//Toon shading uniform  properties 
		//this can be defined + changed by the user.
		uniform float4 _Color;
		uniform float4 _UnlitColor;
		uniform float _DiffuseThreshold;
		uniform float4 _SpecColor;
		uniform float _Shiny;
		uniform float4 _OutlineColor;
		uniform float _OutlineThickness;

		//unity defined properties 
		uniform float4 _LightColor0;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;

	struct vertexInput {

		//Toon shading variable properties
		float4 vertex : POSITION;
		float3 normal : NORMAL;
		float4 texcoord : TEXCOORD0;

	};

	struct vertexOutput {

		float4 pos : SV_POSITION;
		float3 normalDir : TEXCOORD1;
		float4 lightDir : TEXCOORD2;
		float3 viewDir : TEXCOORD3;
		float2 uv : TEXCOORD0;
	};

	vertexOutput vert(vertexInput input)
	{
		vertexOutput output;

		//normal directions
		output.normalDir = normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);

		//World positions
		float4 posWorld = mul(unity_ObjectToWorld, input.vertex);

		//a Vector from the object to the camera to calculate the view direction.
		output.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz); 

		
		//light direction vectors
		float3 fragmentToLightSource = (_WorldSpaceCameraPos.xyz - posWorld.xyz);
		output.lightDir = float4(
			normalize(lerp(_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w)),
			lerp(1.0 , 1.0 / length(fragmentToLightSource), _WorldSpaceLightPos0.w)
			);

		
		output.pos = mul(UNITY_MATRIX_MVP, input.vertex);

		//UV Mapping (if applicable)
		output.uv = input.texcoord;

		return output;

	}

	float4 frag(vertexOutput input) : COLOR
	{

	float nDotL = saturate(dot(input.normalDir, input.lightDir.xyz));
		

	//Diffuse threshold
	float diffuseCutoff = saturate((max(_DiffuseThreshold, nDotL) - _DiffuseThreshold) * 1000);
	

	//Specular threshold
	float specularCutoff = saturate(max(_Shiny, dot(reflect(-input.lightDir.xyz, input.normalDir), input.viewDir)) - _Shiny) * 1000;
	

	//Outlines
	float outlineStrength = saturate((dot(input.normalDir, input.viewDir) - _OutlineThickness) * 1000);


	//ambient illumination calculations
	float3 ambientLight = (1 - diffuseCutoff) * _UnlitColor.xyz; 
	float3 diffuseReflection = (1 - specularCutoff) * _Color.xyz * diffuseCutoff;
	float3 specularReflection = _SpecColor.xyz * specularCutoff;
	float3 outlineColor = _OutlineColor.xyz * outlineStrength;
	

	float3 combinedLight = (ambientLight + diffuseReflection) * outlineStrength + specularReflection; 

	return float4(combinedLight, 1.0)  +tex2D(_MainTex, input.uv); //commented out the texture addition, as it doesn't really work that well.


	}

		ENDCG

	}


	}

}