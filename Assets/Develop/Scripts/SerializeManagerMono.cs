using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Develop.Scripts {
    public class SerializeManagerMono : SingletonMono<SerializeManagerMono> {
        [SerializeField] private GameObject mCellButton;
        [SerializeField] private GameObject mPreviousButton;
        [SerializeField] private GameObject mNextButton;
        [SerializeField] private GameObject mRestartButton;
        [SerializeField] private GameObject mNewButton;
        [SerializeField] private GameObject mRankingButton;
        [SerializeField] private Text       mCountText;

        [SerializeField] private CellMono mCellPrefab;

        [SerializeField] private int mRandomizeDepth;
        [SerializeField] private int mRandomizeSlide;
        [SerializeField] private int mRandomizeReverse;


        private       Drawer mDrawer;
        private const float  cCellSize = 1 / 4f;

        public  IObservable<Unit> OnPreviousButton => mPreviousButton.OnMouseDownAsObservable();
        public  IObservable<Unit> OnNextButton     => mNextButton.OnMouseDownAsObservable();
        public  IObservable<Unit> OnRestartButton  => mRestartButton.OnMouseDownAsObservable();
        private IObservable<Unit> OnNewButton      => mNewButton.OnMouseDownAsObservable();
        public  IObservable<Unit> OnRankingButton  => mRankingButton.OnMouseDownAsObservable();

        private void Awake() {
            mDrawer = new Drawer();
            
            //カウント更新
            mDrawer.OnCountChanged.Subscribe(_count => mCountText.text = _count.ToString());

            //新規作成
            OnNewButton.Subscribe(_ => Randomize());
        }

        public CellMono GenerateCell(int _x, int _y) {
            var cell      = Instantiate(mCellPrefab, transform);
            var cellTrans = cell.transform;
            cellTrans.localPosition = new Vector2(_x - 3.5f, -_y + 3.5f) * cCellSize;
            cellTrans.name          = $"({_x},{_y})";
            return cell;
        }

        [Button("Randomize")]
        private void Randomize() {
            mDrawer.Randomize(mRandomizeDepth, mRandomizeSlide, mRandomizeReverse);
        }
    }
}