using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Develop.Scripts {
    public class SerializeManagerMono : SingletonMono<SerializeManagerMono> {
        [SerializeField] private GameObject mStagePreviousButton;
        [SerializeField] private GameObject mStageNextButton;

        [SerializeField] private GameObject mCellPreviousButton;
        [SerializeField] private GameObject mCellNextButton;
        [SerializeField] private GameObject mCellResetButton;
        [SerializeField] private GameObject mHelpButton;

        [SerializeField] private GameObject mLeftButton;
        [SerializeField] private GameObject mUpButton;
        [SerializeField] private GameObject mRightButton;
        [SerializeField] private GameObject mDownButton;
        [SerializeField] private GameObject mReverseButton;

        [SerializeField] private Text mStageNumText;
        [SerializeField] private Text mCountText;

        [SerializeField] private CellMono mCellPrefab;

        [SerializeField] private int mRandomizeDepth;
        [SerializeField] private int mRandomizeSlide;
        [SerializeField] private int mRandomizeReverse;


        private       Drawer mDrawer;
        private const float  cCellSize = 0.2f;

        public  IObservable<Unit> OnPreviousButton => mCellPreviousButton.OnMouseDownAsObservable();
        public  IObservable<Unit> OnNextButton     => mCellNextButton.OnMouseDownAsObservable();
        public  IObservable<Unit> OnRestartButton  => mCellResetButton.OnMouseDownAsObservable();
        private IObservable<Unit> OnNewButton      => mLeftButton.OnMouseDownAsObservable();

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