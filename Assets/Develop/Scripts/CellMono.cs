using DG.Tweening;
using UnityEngine;

namespace Develop.Scripts {
    public class CellMono : MonoBehaviour {
        [SerializeField] private Transform mBlackPanel;
        [SerializeField] private Transform mWhitePanel;

        public ColorType Color { get; private set; }

        private static readonly Vector3 sFront = new Vector3(0, 0, -0.1f);
        private static readonly Vector3 sBack  = new Vector3(0, 0, 0.1f);

        private Tween mBlackTween;
        private Tween mWhiteTween;

        public void ChangeInstantly(ColorType _colorType) {
            mBlackPanel.gameObject.SetActive(_colorType == ColorType.Black);
            mWhitePanel.gameObject.SetActive(_colorType == ColorType.White);

            Color = _colorType;
        }

        public void ScaleUp(ColorType _colorType, float _duration, Ease _ease) {
            mBlackTween.Complete();
            mWhiteTween.Complete();
            
            mBlackPanel.gameObject.SetActive(true);
            mWhitePanel.gameObject.SetActive(true);
            
            if (_colorType == ColorType.Black) { //黒を拡大表示 白を裏に
                mBlackPanel.localScale    = Vector3.zero;
                mBlackPanel.localPosition = sFront;
                mBlackTween               = mBlackPanel.DOScale(Vector3.one, _duration).SetEase(_ease);

                mWhitePanel.localScale    = Vector3.one;
                mWhitePanel.localPosition = sBack;
            } else {//白を拡大表示 黒を裏に
                mWhitePanel.localScale    = Vector3.zero;
                mWhitePanel.localPosition = sFront;
                mWhiteTween               = mWhitePanel.DOScale(Vector3.one, _duration).SetEase(_ease);

                mBlackPanel.localScale    = Vector3.one;
                mBlackPanel.localPosition = sBack;
            }
            
            Color = _colorType;
        }
    }
}