using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace Develop.Scripts {
    public class SliderMono : MonoBehaviour {
        [SerializeField] private GameObject mDetection;
        [SerializeField] private Transform mHandle;
        [SerializeField] private Transform  mMask;

        private const float cMaskMax = 2.89f;

        private const float cHandleMin = -0.22f;
        private const float cHandleMax = 0.52f;

        private void Awake() {
            mDetection.OnMouseDragAsObservable().Subscribe(
                _ => {
                    var pos = mHandle.position;
                    pos.x                      = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,cHandleMin,cHandleMax);
                    mMask.localScale = new Vector3(Mathf.Lerp(cMaskMax,0,(pos.x-cHandleMin)/(cHandleMax-cHandleMin)),1,1);
                    mHandle.position = pos;
                });
        }
    }
}