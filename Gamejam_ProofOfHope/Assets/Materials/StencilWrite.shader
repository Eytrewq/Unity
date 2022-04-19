// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Stencil/StencilWrite"
{
    Properties{}

    SubShader{

        Tags { 
            "RenderType" = "Opaque" 
        }
 
        Pass{
            ZWrite Off
        }
    }
}
