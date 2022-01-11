using UniRx;
using UnityEngine;

namespace minigame.Battle.Inputs
{
    public interface IInputEventProvider {
        IReadOnlyReactiveProperty<Vector3>  MovePosition { get; }
        IReadOnlyReactiveProperty<bool>  OnClicked { get; }
    }
}