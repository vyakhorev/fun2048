

using UnityEngine;

namespace GameInput
{
    public class TapEventArgs
    {
        public readonly Vector2 At;

        public TapEventArgs(Vector2 at)
        {
            At = at;
        }

    }
}