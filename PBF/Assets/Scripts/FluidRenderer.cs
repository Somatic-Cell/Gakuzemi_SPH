using System.Collections;
using UnityEngine;

namespace Yokota.Fluid {
    [RequireComponent(typeof(Fluid3D))]
    public class FluidRenderer : MonoBehaviour
    {

        public Fluid3D solver;
        public Material RenderParticleMat;
        public Color WaterColor;

        void OnRenderObject()
        {
            DrawParticle();
        }

        void DrawParticle()
        {
            RenderParticleMat.SetPass(0);
            RenderParticleMat.SetColor("_WaterColor", WaterColor);
            RenderParticleMat.SetBuffer("_ParticlesBuffer", solver.ParticlesBufferRead);
            Graphics.DrawProceduralNow(MeshTopology.Points, solver.NumParticles);
        }
    }
}
