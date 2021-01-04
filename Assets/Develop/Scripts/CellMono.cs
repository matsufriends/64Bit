using DG.Tweening;
using UnityEngine;

namespace Develop.Scripts {
    public class CellMono : MonoBehaviour {
        [SerializeField] private Transform mBlackPanel;
        [SerializeField] private Transform mWhitePanel;

        private ColorType mCachedColor;

        private Tween mBlackTween;
        private Tween mWhiteTween;

        private static readonly Vector3 sFront = new Vector3(0, 0, -0.1f);
        private static readonly Vector3 sBack  = new Vector3(0, 0, 0.1f);

        private const float cScaleDuration = 0.2f;
        private const Ease  cScaleEase     = Ease.OutQuart;

        public void SetColor(ColorType _colorType) {
            if (mCachedColor == _colorType) return;

            //Tween停止
            mBlackTween.Kill();
            mWhiteTween.Kill();

            //ベース
            switch (mCachedColor) {
                case ColorType.Black:
                    mBlackPanel.localScale    = Vector3.one;
                    mBlackPanel.localPosition = sBack;
                    break;
                case ColorType.White:
                    mWhitePanel.localScale    = Vector3.one;
                    mWhitePanel.localPosition = sBack;
                    break;
            }

            //上
            switch (_colorType) {
                case ColorType.Black:
                    mBlackPanel.localScale    = Vector3.zero;
                    mBlackPanel.localPosition = sFront;
                    mBlackTween               = mBlackPanel.DOScale(Vector3.one, cScaleDuration).SetEase(cScaleEase);
                    break;
                case ColorType.White:
                    mWhitePanel.localScale    = Vector3.zero;
                    mWhitePanel.localPosition = sFront;
                    mWhiteTween               = mWhitePanel.DOScale(Vector3.one, cScaleDuration).SetEase(cScaleEase);
                    break;
            }

            mCachedColor = _colorType;
        }
    }
}