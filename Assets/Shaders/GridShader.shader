Shader "Custom/GridShader" {

    Category {
        BindChannels { 
            Bind "Color", color 
            Bind "Vertex", vertex
        }
        SubShader { Pass { } }
    }
}