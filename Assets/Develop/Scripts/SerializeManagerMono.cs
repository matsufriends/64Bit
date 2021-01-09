using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Develop.Scripts {
    public class SerializeManagerMono : SingletonMono<SerializeManagerMono> {
        [SerializeField] private ButtonMono mLeftButton;
        [SerializeField] private ButtonMono mUpButton;
        [SerializeField] private ButtonMono mRightButton;
        [SerializeField] private ButtonMono mDownButton;
        [SerializeField] private ButtonMono mReverseButton;

        [SerializeField] private GameObject mCellPreviousButton;
        [SerializeField] private GameObject mCellNextButton;
        [SerializeField] private GameObject mCellResetButton;
        [SerializeField] private GameObject mHelpButton;
        
        //[SerializeField] private Text mStageNumText;
        [SerializeField] private Text mCountText;

        [SerializeField] private ButtonMono mBitPrefab;

        [SerializeField] private int mRandomizeDepth;
        [SerializeField] private int mRandomizeSlide;
        [SerializeField] private int mRandomizeReverse;

        private       Drawer mDrawer;
        private const float  cCellSize = 0.2f;

        public  IObservable<Unit> OnRestartButton  => mCellResetButton.OnMouseDownAsObservable();

        private void Awake() {
            mDrawer = new Drawer();

            //カウント更新
            mDrawer.OnCountChanged.Subscribe(_count => mCountText.text = _count.ToString());

        }

        public ButtonMono GenerateBit(int _x, int _y) {
            var bit      = Instantiate(mBitPrefab, transform);
            bit.SetStatus(false);
            var cellTrans = bit.transform;
            cellTrans.localPosition = new Vector2(_x - 3.5f, -_y + 3.5f) * cCellSize;
            cellTrans.name          = $"({_x},{_y})";
            return bit;
        }

        [Button("Randomize")]
        private void Randomize() {
            mDrawer.Randomize(mRandomizeDepth, mRandomizeSlide, mRandomizeReverse);
        }
    }
}