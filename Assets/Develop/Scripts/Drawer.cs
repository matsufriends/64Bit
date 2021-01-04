﻿using UniRx;
using UnityEngine;

namespace Develop.Scripts {
    public class Drawer {
        private readonly CellMono[,] mCellMonoArrays = new CellMono[8, 8];

        private readonly ColorType[,] mStartColors = new ColorType[8, 8];
        private readonly ColorType[,] mDrewColors  = new ColorType[8, 8];


        public Drawer() {
            //初期化
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mCellMonoArrays[x, y] = SerializeManagerMono.Instance.GenerateCell(x, y);
                }
            }

            //方向キー
            InputManager.OnDirectionInput.Subscribe(
                _dir => {
                    DrawCell(SlideCell(mDrewColors, _dir));
                }
            );

            //スペースキー
            InputManager.OnSpaceInput.Subscribe(
                _ => {
                    DrawCell(AroundCell(mDrewColors));
                }
            );

            //仮表示
            DrawCell(mStartColors);
        }

        private static ColorType[,] SlideCell(in ColorType[,] _baseColors, in Vector2Int _dir) {
            var tmpColors = new ColorType[8, 8];
            for (var y = 8; y < 16; y++) {
                for (var x = 8; x < 16; x++) {
                    tmpColors[x % 8, y % 8] = _baseColors[(x - _dir.x) % 8, (y + _dir.y) % 8];
                }
            }

            return tmpColors;
        }

        private static ColorType[,] AroundCell(in ColorType[,] _baseColors) {
            var tmpColors = new ColorType[8, 8];

            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    tmpColors[x, y] = x == 0 || y == 0 || x == 7 || y == 7 ? _baseColors[x, y].Reverse() : _baseColors[x, y];
                }
            }

            return tmpColors;
        }

        private void DrawCell(in ColorType[,] _colorTypes) {
            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    mCellMonoArrays[x, y].SetColor(_colorTypes[x, y]);
                    mDrewColors[x, y] = _colorTypes[x, y];
                }
            }
        }

        public void Randomize(in int _depth) {
            //初期化
            var tmpColors = new ColorType[8, 8];
            var dir = new[] {
                Vector2Int.left
              , Vector2Int.up
              , Vector2Int.right
              , Vector2Int.down
            };

            var beforeDir = 0;

            
            //ランダム化
            tmpColors = AroundCell(tmpColors);
            tmpColors = SlideCell(tmpColors, dir[Random.Range(0, 4)]);

            for (var i = 0; i < _depth; i++) {
                if (Random.Range(0, 2) == 0) tmpColors = AroundCell(tmpColors);
                else tmpColors                         = SlideCell(tmpColors, dir[Random.Range(0, 4)]);
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