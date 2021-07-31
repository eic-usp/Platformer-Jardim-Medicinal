using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Experimental.Rendering.Universal {
    public class ShadowMaker : MonoBehaviour {
        [SerializeField] bool m_HasRenderer = false;
        [SerializeField] bool m_UseRendererSilhouette = true;
        [SerializeField] bool m_CastsShadows = true;
        [SerializeField] bool m_SelfShadows = false;
        [SerializeField] int[] m_ApplyToSortingLayers = null;
        [SerializeField] Vector3[] m_ShapePath = null;
        [SerializeField] int m_ShapePathHash = 0;
        [SerializeField] Mesh m_Mesh;
        [SerializeField] int m_InstanceId;

        internal ShadowCasterGroup2D m_ShadowCasterGroup = null;
        internal ShadowCasterGroup2D m_PreviousShadowCasterGroup = null;

        internal Mesh mesh => m_Mesh;
        internal Vector3[] shapePath => m_ShapePath;
        internal int shapePathHash { get { return m_ShapePathHash; } set { m_ShapePathHash = value; } }

        int m_PreviousShadowGroup = 0;
        bool m_PreviousCastsShadows = true;
        int m_PreviousPathHash = 0;


        /// <summary>
        /// If selfShadows is true, useRendererSilhoutte specifies that the renderer's sihouette should be considered part of the shadow. If selfShadows is false, useRendererSilhoutte specifies that the renderer's sihouette should be excluded from the shadow
        /// </summary>
        public bool useRendererSilhouette
        {
            set { m_UseRendererSilhouette = value; }
            get { return m_UseRendererSilhouette && m_HasRenderer;  }
        }

        /// <summary>
        /// If true, the shadow casting shape is included as part of the shadow. If false, the shadow casting shape is excluded from the shadow.
        /// </summary>
        public bool selfShadows
        {
            set { m_SelfShadows = value; }
            get { return m_SelfShadows; }
        }

        /// <summary>
        /// Specifies if shadows will be cast.
        /// </summary>
        public bool castsShadows
        {
            set { m_CastsShadows = value; }
            get { return m_CastsShadows; }
        }

        static int[] SetDefaultSortingLayers()
        {
            int layerCount = SortingLayer.layers.Length;
            int[] allLayers = new int[layerCount];

            for(int layerIndex=0;layerIndex < layerCount;layerIndex++)
            {
                allLayers[layerIndex] = SortingLayer.layers[layerIndex].id;
            }

            return allLayers;
        }

        internal bool IsShadowedLayer(int layer)
        {
            return m_ApplyToSortingLayers != null ? Array.IndexOf(m_ApplyToSortingLayers, layer) >= 0 : false;
        }

        
        public void Generate() {
            CompositeCollider2D tilemapCollider = GetComponent<CompositeCollider2D>();
            GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
            if(shadowCasterContainer == null) shadowCasterContainer = new GameObject("shadow_casters");
            for (int i = 0; i < tilemapCollider.pathCount; i++) {
                Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
                tilemapCollider.GetPath(i, pathVertices);
                GameObject shadowCaster = new GameObject("shadow_caster_" + i);
                PolygonCollider2D shadowPolygon = (PolygonCollider2D)shadowCaster.AddComponent(typeof(PolygonCollider2D));
                shadowCaster.transform.parent = shadowCasterContainer.transform;
                shadowPolygon.points = pathVertices;
                shadowPolygon.enabled = false;
                ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
                shadowCasterComponent.selfShadows = true;
            }
        }

        
        private Vector3 vec2To3(Vector2 inputVector) {
            return new Vector3(inputVector.x, inputVector.y, 0);
        }


#if UNITY_EDITOR

        [CustomEditor(typeof(ShadowMaker))]
        public class ShadowCaster2DEditor : Editor {

            public override void OnInspectorGUI() {
                DrawDefaultInspector();

                if (GUILayout.Button("Generate")) {
                    var generator = (ShadowMaker)target;

                    generator.Generate();
                }
            }
        }
#endif

    }
}