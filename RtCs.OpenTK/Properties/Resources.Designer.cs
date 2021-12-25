﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace RtCs.OpenGL.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RtCs.OpenGL.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///out vec4 outColor;
        ///
        ///layout (location = 2) uniform vec4 inColor;
        ///
        ///void main()
        ///{
        ///    outColor = inColor;
        ///    return;
        ///} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Color_fragment_glsl {
            get {
                return ResourceManager.GetString("Color.fragment.glsl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///layout (location = 0) uniform mat4 inProjectionMatrix;
        ///layout (location = 1) uniform mat4 inModelviewMatrix;
        ///
        ///layout (location = 0) in vec3 inPosition;
        ///
        ///out gl_PerVertex
        ///{
        ///	vec4 gl_Position;
        ///};
        ///
        ///void main()
        ///{
        ///    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);	
        ///    return;
        ///} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Color_vertex_glsl {
            get {
                return ResourceManager.GetString("Color.vertex.glsl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   layout (std140) uniform BuiltInMatrix4 {
        ///	mat4 Matrix;
        ///} BuiltIn_Model, BuiltIn_View, BuiltIn_Projection, BuiltIn_ModelView, BuiltIn_ViewProjection, BuiltIn_ModelViewProjection;
        ///
        ///layout (std140) uniform BuiltInMatrix3 {
        ///	mat3 Matrix;
        ///} BuiltIn_Normal;
        ///
        ///mat4 ModelMatrix;
        ///mat4 ViewMatrix;
        ///mat4 ProjectionMatrix;
        ///mat4 ModelViewMatrix;
        ///mat4 ViewProjectionMatrix;
        ///mat4 ModelViewProjectionMatrix;
        ///mat3 NormalMatrix;
        ///
        ///void ReadBuiltInVariables()
        ///{
        ///	ModelMatrix = BuiltIn_Model.Matrix;
        ///	ViewMatrix =  [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ShaderBuiltInSource_h {
            get {
                return ResourceManager.GetString("ShaderBuiltInSource.h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///out vec4 outColor;
        ///
        ///layout (location = 0) in vec2 inTexCoord;
        ///layout (location = 2) uniform sampler2D inTexture;
        ///
        ///void main()
        ///{
        ///    outColor = texture(inTexture, inTexCoord);
        ///    return;
        ///}
        /// に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Texture_fragment_glsl {
            get {
                return ResourceManager.GetString("Texture.fragment.glsl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///layout (location = 0) uniform mat4 inProjectionMatrix;
        ///layout (location = 1) uniform mat4 inModelviewMatrix;
        ///
        ///layout (location = 0) in vec3 inPosition;
        ///layout (location = 1) in vec2 inTexCoord;
        ///
        ///out gl_PerVertex
        ///{
        ///	vec4 gl_Position;
        ///};
        ///layout (location = 0) out vec2 outTexCoord;
        ///
        ///void main()
        ///{
        ///    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);
        ///    outTexCoord = inTexCoord;
        ///    return;
        ///} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string Texture_vertex_glsl {
            get {
                return ResourceManager.GetString("Texture.vertex.glsl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///out vec4 outColor;
        ///
        ///layout (location = 0) in vec4 inColor;
        ///
        ///void main()
        ///{
        ///    outColor = inColor;
        ///    return;
        ///} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string VertexColor_fragment_glsl {
            get {
                return ResourceManager.GetString("VertexColor.fragment.glsl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   #version 460
        ///
        ///layout (location = 0) uniform mat4 inProjectionMatrix;
        ///layout (location = 1) uniform mat4 inModelviewMatrix;
        ///
        ///layout (location = 0) in vec3 inPosition;
        ///layout (location = 1) in vec4 inColor;
        ///
        ///out gl_PerVertex
        ///{
        ///	vec4 gl_Position;
        ///};
        ///layout (location = 0) out vec4 outColor;
        ///
        ///void main()
        ///{
        ///    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);	
        ///    outColor = inColor;
        ///    return;
        ///} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string VertexColor_vertex_glsl {
            get {
                return ResourceManager.GetString("VertexColor.vertex.glsl", resourceCulture);
            }
        }
    }
}
