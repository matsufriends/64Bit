using System;
using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Develop.Scripts {
    public class SerializeManagerMono : SingletonMono<SerializeManagerMono> {
        [SerializeField] private GameObject mCellButton;
        [SerializeField] private GameObject mPreviousButton;
        [SerializeField] private GameObject mNextButton;
        [SerializeField] private GameObject mRestartButton;
        [SerializeField] private GameObject mRankingButton;

        [SerializeField] private CellMono mCellPrefab;

        [SerializeField] private float mScaleDuration;
        [SerializeField] private Ease  mScaleEase;


        private       Drawer mDrawer;
        private const float  cCellSize = 1 / 4f;

        public IObservable<Vector2Int> OnCellButtonClick =>
            mCellButton.OnMouseDownAsObservable()
                       .Select(
                           _ => {
                               var pos = Input.mousePosition;
                               var x   = Mathf.FloorToInt(pos.x         * 2 / Screen.width / cCellSize);
                               var y   = 7 - Mathf.FloorToInt(pos.y * 2 / Screen.height / cCellSize);
                               return new Vector2Int(x, y);
                           }
                       );

        public IObservable<Unit> OnPreviousButton => mPreviousButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnNextButton     => mNextButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnRestartButton  => mRestartButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnRankingButton  => mRankingButton.OnMouseDownAsObservable();

        private void Awake() {
            mDrawer = new Drawer();
        }

        public CellMono GenerateCell(int _x, int _y) {
            var cell = Instantiate(mCellPrefab, transform);
            cell.SetUp(ColorType.Black);
            var cellTrans = cell.transform;
            cellTrans.localPosition = new Vector2(_x - 3.5f, -_y + 3.5f) * cCellSize;
            cellTrans.name          = $"({_x},{_y})";
            return cell;
        }

        public void ScaleUp(CellMono _cell, ColorType _colorType) {
            _cell.ScaleUp(_colorType, mScaleDuration, mScaleEase);
        }
    }
}