using UniRx;
using UnityEngine;

namespace Develop.Scripts {
    public class Drawer {
        private readonly CellMono[,] mCellArrays = new CellMono[8, 8];

        public Drawer() {
            //初期化
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mCellArrays[x, y] = SerializeManagerMono.Instance.GenerateCell(x, y);
                }
            }

            //クリック
            SerializeManagerMono.Instance.OnCellButtonClick.Subscribe(
                _pos => {
                    Debug.Log(_pos);
                }
            );

            //方向キー
            InputManager.OnDirectionInput.Subscribe(
                _dir => {
                    var tmpColors = new ColorType[8, 8];
                    for (var y = 0; y < 8; y++) {
                        for (var x = 0; x < 8; x++) {
                            tmpColors[x, y] = Color(x - _dir.x, y + _dir.y);
                        }
                    }

                    for (var y = 0; y < 8; y++) {
                        for (var x = 0; x < 8; x++) {
                            SerializeManagerMono.Instance.ScaleUp(Cell(x, y), tmpColors[x, y]);
                        }
                    }
                }
            );

            //スペースキー
            InputManager.OnSpaceInput.Subscribe(
                _ => {
                    for (var y = 0; y < 8; y++) {
                        for (var x = 0; x < 8; x++) {
                            if (x == 0 || y == 0 || x == 7 || y == 7) {
                                SerializeManagerMono.Instance.ScaleUp(Cell(x, y), Color(x, y).Reverse());
                            }
                        }
                    }
                }
            );
        }

        private CellMono Cell(int _x, int _y) {
            return mCellArrays[_x.Surplus(8), _y.Surplus(8)];
        }

        private ColorType Color(int _x, int _y) {
            return mCellArrays[_x.Surplus(8), _y.Surplus(8)].Color;
        }
    }
}