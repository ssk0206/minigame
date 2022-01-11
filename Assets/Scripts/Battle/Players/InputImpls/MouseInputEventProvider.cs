using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

using minigame.Battle.Inputs;

namespace minigame.Battle.Players.InputImpls
{
    public class MouseInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<Vector3> _movePosition = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<bool> _onClicked = new BoolReactiveProperty(false);

        public IReadOnlyReactiveProperty<Vector3> MovePosition => _movePosition;
        public IReadOnlyReactiveProperty<bool> OnClicked => _onClicked;

        protected void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.mousePosition)
                .Subscribe(x => _movePosition.SetValueAndForceNotify(x)); // 同じ値でも通知

            this.UpdateAsObservable()
                .Select(_ => Input.GetMouseButtonDown(0))
                .DistinctUntilChanged()
                // .Subscribe(x => _onClicked.Value = x);
                .Subscribe(x => Debug.Log(x));
        }
    }
}

