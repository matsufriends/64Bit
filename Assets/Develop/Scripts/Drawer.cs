using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Develop.Scripts {
    public class Drawer {
        private readonly ButtonMono[,] mBitArrays = new ButtonMono[8, 8];

        private readonly LightType[,] mStartColors = new LightType[8, 8];
        private readonly LightType[,] mDrewColors  = new LightType[8, 8];

        private readonly ReactiveProperty<int> mCount = new ReactiveProperty<int>();

        public IObservable<int> OnCountChanged => mCount;

        public Drawer() {
            //初期化
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mBitArrays[x, y] = SerializeManagerMono.Instance.GenerateBit(x, y);
                }
            }

            //方向キー
            InputManager.OnDirectionInput.Subscribe(
                _dir => {
                    DrawCell(SlideCell(mDrewColors, _dir));
                    mCount.Value++;
                }
            );

            //スペースキー
            InputManager.OnSpaceInput.Subscribe(
                _ => {
                    DrawCell(AroundCell(mDrewColors));
                    mCount.Value++;
                }
            );

            //リスタート
            SerializeManagerMono.Instance.OnRestartButton.Subscribe(
                _ => {
                    DrawCell(mStartColors);
                    mCount.Value = 0;
                }
            );

            //仮表示
            DrawCell(mStartColors);
        }

        private static LightType[,] SlideCell(in LightType[,] _baseColors, in Vector2Int _dir) {
            var tmpColors = new LightType[8, 8];
            for (var y = 8; y < 16; y++) {
                for (var x = 8; x < 16; x++) {
                    tmpColors[x % 8, y % 8] = _baseColors[(x - _dir.x) % 8, (y + _dir.y) % 8];
                }
            }

            return tmpColors;
        }

        private static LightType[,] AroundCell(in LightType[,] _baseColors) {
            var tmpColors = new LightType[8, 8];

            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    tmpColors[x, y] = x == 0 || y == 0 || x == 7 || y == 7 ? _baseColors[x, y].Reverse() : _baseColors[x, y];
                }
            }

            return tmpColors;
        }

        private void DrawCell(in LightType[,] _colorTypes) {
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mBitArrays[x, y].SetStatus(_colorTypes[x, y]==LightType.On);
                    mDrewColors[x, y] = _colorTypes[x, y];
                }
            }
        }

        public void Randomize(in int _depth, in int _slideCount, int _reverse) {
            //初期化
            var tmpColors = new LightType[8, 8];
            var dirVector = new[] {
                Vector2Int.left
              , Vector2Int.up
              , Vector2Int.right
              , Vector2Int.down
            };

            var beforeDir = Random.Range(0, 4);
            
            //スコア初期化
            mCount.Value = 0;


            //ランダム化
            tmpColors = AroundCell(tmpColors);

            for (var i = 0; i < _depth; i++) {
                //スライド
                beforeDir = (beforeDir + 1 + Random.Range(0, 2) * 2) % 4;
                for (var j = Random.Range(1, _slideCount); j > 0; j--) tmpColors = SlideCell(tmpColors, dirVector[beforeDir]);

                //反転
                if (Random.Range(0, 100) >= _reverse) tmpColors = AroundCell(tmpColors);
            }

            //初期の設定
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mStartColors[x, y] = tmpColors[x, y];
                }
            }

            DrawCell(tmpColors);
        }
    }
}