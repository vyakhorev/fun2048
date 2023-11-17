using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;


namespace Fun2048
{
    [Input]
    public class SwipeActionComponent : IComponent
    {
        public GridDirection gridDirection;
    }

    [Input]
    public class ExpiredTagComponent : IComponent
    {

    }

}