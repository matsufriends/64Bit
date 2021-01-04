using System;
using Sirenix.OdinInspector;
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


        private       Drawer mDrawer;
        private const float  cCellSize = 1 / 4f;

        public IObservable<Unit> OnPreviousButton => mPreviousButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnNextButton     => mNextButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnRestartButton  => mRestartButton.OnMouseDownAsObservable();
        public IObservable<Unit> OnRankingButton  => mRankingButton.OnMouseDownAsObservable();

        private void Awake() {
            mDrawer = new Drawer();
        }

        public CellMono GenerateCell(int _x, int _y) {
            var cell      = Instantiate(mCellPrefab, transform);
            var cellTrans = cell.transform;
            cellTrans.localPosition = new Vector2(_x - 3.5f, -_y + 3.5f) * cCellSize;
            cellTrans.name          = $"({_x},{_y})";
            return cell;
        }

        [Button("Randomize")]
        private void Randomize(int _count) {
            mDrawer.Randomize(_count);
        }
    }
}