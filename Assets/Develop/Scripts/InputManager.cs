using System;
using UniRx;
using UnityEngine;

namespace Develop.Scripts {
    public static class InputManager {
        private static bool Left  => Input.GetKeyDown(KeyCode.LeftArrow);
        private static bool Up    => Input.GetKeyDown(KeyCode.UpArrow);
        private static bool Right => Input.GetKeyDown(KeyCode.RightArrow);
        private static bool Down  => Input.GetKeyDown(KeyCode.DownArrow);

        public static IObservable<Vector2Int> OnDirectionInput =>
            Observable.EveryUpdate()
                      .Where(_ => Left || Up || Right || Down)
                      .Select(
                          _ => Left  ? Vector2Int.left :
                               Up    ? Vector2Int.up :
                               Right ? Vector2Int.right :
                               Down  ? Vector2Int.down : Vector2Int.zero
                      );

        public static IObservable<Unit> OnSpaceInput =>
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space)).Select(_ => Unit.Default);
    }
}