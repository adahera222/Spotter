Shader "Custom/GridShader" {

    Category {
   	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
   	    Blend SrcAlpha OneMinusSrcAlpha
        BindChannels { 
            Bind "Color", color 
            Bind "Vertex", vertex
        }
        SubShader { Pass { } }
    }
}