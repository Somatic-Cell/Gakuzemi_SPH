using UnityEngine;
using System.Runtime.InteropServices;

namespace Yokota.Fluid
{
    public struct FluidParticle
    {
        public Vector3 Position;
        public float dummy0;
        public Vector3 Velocity;
        public float dummy1;
    }


    public class Fluid3D : FluidBase<FluidParticle>
    {
        [SerializeField] private float ballRadius = 0.1f;           // 粒子位置初期化時の球半径．
        [SerializeField] private float MouseInteractionRadius = 2f; // マウスインタラクションの範囲の広さ．

        private bool isMouseDown;
        private Vector3 screenToWorldPointPos;

        /// <summary>
        /// パーティクル初期位置の設定
        /// </summary>
        /// <param name="particles"></param>
        protected override void InitParticleData(ref FluidParticle[] particles)
        {
            for(int i = 0; i < NumParticles; i++)
            {
                particles[i].Velocity = Vector3.zero;
                particles[i].Position = range / 2f + Random.insideUnitSphere * ballRadius;  // 球形に粒子を初期化する．
            }
        }

        /// <summary>
        /// ComputeShader の定数バッファに追加する
        /// </summary>
        /// <param name="cs"></param>
        protected override void AdditionalCSParams(ComputeShader cs)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
            }

            if (isMouseDown)
            {
                Vector3 mousePos = Input.mousePosition;
                GameObject camObj = GameObject.Find("Main Camera");
                mousePos.z =  Vector3.Distance(camObj.transform.position, Vector3.zero);
                //Debug.Log(mousePos.z);
                screenToWorldPointPos = Camera.main.ScreenToWorldPoint(mousePos);
            }

            cs.SetVector("_MousePos", screenToWorldPointPos);
            //Debug.Log(screenToWorldPointPos);
            cs.SetFloat("_MouseRadius", MouseInteractionRadius);
            cs.SetBool("_MouseDown", isMouseDown);
        }

    }
}
