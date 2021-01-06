using DG.Tweening;
using UnityEngine;

namespace Develop.Scripts {
    public class CellMono : MonoBehaviour {
        [SerializeField] private SpriteRenderer mSpriteRenderer;

        private LightType mCachedLight;

        private Tween mAnimTween;

        public void SetLight(LightType _lightType) {
            if (mCachedLight == _lightType) return;

            //Tween停止
            mAnimTween.Kill();

            //ボタン点く 
            
            //アニメーション
            switch (_lightType) {
                case LightType.Off:
                    mSpriteRenderer.sprite                  = SerializeManagerMono.Instance.LightOff;
                    mSpriteRenderer.transform.localPosition =Vector3.zero;
                    break;
                case LightType.On:
                    mSpriteRenderer.sprite                  = SerializeManagerMono.Instance.LightOn;
                    mSpriteRenderer.transform.localPosition = Vector3.up * SerializeManagerMono.Instance.LightOnOffset;
                    break;
            }

            mCachedLight = _lightType;
        }
    }
}