using DG.Tweening;
using UnityEngine;

namespace Develop.Scripts {
    public class DrawerMono : SingletonMono<DrawerMono> {
        [SerializeField] private CellMono mCellPrefab;


        [SerializeField] private float mScaleDuration;
        [SerializeField] private Ease  mScaleEase;

        private CellMono[,] mCellArrays;
        private int         mCacheX;
        private int         mCacheY;

        private bool mGenerateFlag;

        private const float cCellSize = 1 / 4f;

        private void Awake() {
            mCacheX = -1;
            mCacheY = -1;
            OnInstanceMade();
        }

        protected override void OnInstanceMade() {
            if (mGenerateFlag) return;
            mCellArrays = new CellMono[8, 8];

            var offset = new Vector2(-3.5f, 3.5f) * cCellSize;
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    var cell = Instantiate(mCellPrefab, transform);
                    cell.ChangeInstantly(ColorType.Black);
                    var cellTrans = cell.transform;
                    cellTrans.localPosition = offset + new Vector2(x, -y) * cCellSize;
                    cellTrans.name          = $"({x},{y})";
                    mCellArrays[x, y]       = cell;
                }
            }

            mGenerateFlag = true;
        }

        private void ScaleUp(int _x, int _y, ColorType _colorType) {
            var cell = mCellArrays[_x % 8, _y % 8];
            cell.ScaleUp(_colorType, mScaleDuration, mScaleEase);
        }

        private void Update() {
            var left  = Input.GetKeyDown(KeyCode.LeftArrow);
            var up    = Input.GetKeyDown(KeyCode.UpArrow);
            var right = Input.GetKeyDown(KeyCode.RightArrow);
            var down  = Input.GetKeyDown(KeyCode.DownArrow);

            if (Input.GetMouseButton(0)) {
                var mousePos = Input.mousePosition;
                var x        = Mathf.RoundToInt(mousePos.x / (Screen.width / 8f) - 0.5f);
                var y        = 7 - Mathf.RoundToInt(mousePos.y / (Screen.height / 8f) - 0.5f);
                if ((x != mCacheX || y != mCacheY) && 0 <= x && x <= 7 && 0 <= y && y <= 7) {
                    ScaleUp(x, y, Color(x, y).Reverse());
                    mCacheX = x;
                    mCacheY = y;
                }
            } else if (Input.GetMouseButtonUp(0)) {
                mCacheX = -1;
                mCacheY = -1;
            }

            if (left || up || right || down) {
                var colors = new ColorType[8, 8];
                for (var y = 0; y < 8; y++) {
                    for (var x = 0; x < 8; x++) {
                        colors[x, y] = Color(x + (left ? 1 : right ? 7 : 0), y + (up ? 1 : down ? 7 : 0));
                    }
                }

                for (var y = 0; y < 8; y++) {
                    for (var x = 0; x < 8; x++) {
                        ScaleUp(x, y, colors[x, y]);
                    }
                }
            }
        }

        private ColorType Color(int _x, int _y) {
            return mCellArrays[_x % 8, _y % 8].Color;
        }
    }
}