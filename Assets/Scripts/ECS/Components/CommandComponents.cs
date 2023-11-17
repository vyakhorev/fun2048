using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;


namespace Fun2048
{
    [Command]
    public class ChipGameComponent : IComponent
    {
        public Chip2048Game chip2048Game;
    }

}