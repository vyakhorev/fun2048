
// Instanced from a prefab / pool component
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Fun2048
{

    [Game]
    public class ChipIDComponent : IComponent
    {
        [EntityIndex] public int Value;
    }

    [Game]
    public class ChipPrimaryIDComponent : IComponent
    {
        [PrimaryEntityIndex] public int Value;
    }

    [Game]
    public class ViewComponent : IComponent
    {
        public GameObject gameObject;
    }

    // Marks logical (grid) position
    [Game]
    public class GridPositionComponent : IComponent
    {
        public Vector2Int logicalPosition;
    }

    [Game]
    public class PositionRotationComponent : IComponent
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    // Something that's about to be destroyed
    [Game]
    public class LifespanComponent : IComponent
    {
        public float timeToDestroy;
    }

    // A component indicating that we need to spawn a new view
    [Game]
    public class ViewPrefabComponent : IComponent
    {
        public GameObject prefabGameObject;
    }

    // A way to distinguish objects that should be pooled (mass objects) from unique objects
    [Game]
    public class PooledObjectTagComponent : IComponent
    {

    }

    // A component indicating that we need to spawn a new view
    [Game]
    public class ChipViewDetailsComponent : IComponent
    {
        public AChip chip;
    }

    // A global component for a single entity responsible for object pooling
    [Game]
    public class ViewsPoolComponent : IComponent
    {
        public GameObjectPools gameObjectPools;
    }

}