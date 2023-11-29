using DG.Tweening;
using UnityEngine;

namespace Utility.UI
{
    public interface ITransition<T> where T : Component
    {
        Tween Play(T component);
    }
}
