using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;


namespace Fun2048
{
    [Game]
    public class PrimaryIndexComponent : IComponent
    {
        [PrimaryEntityIndex] public int primaryIdx;
    }

    // Singleton with random context
    [Game]
    public class RandomContextComponent : IComponent
    {
        public System.Random random;
    }

    [Input]
    public class SwipeActionComponent : IComponent
    {
        public GridDirection gridDirection;
    }

    [Input]
    public class ExpiredTagComponent : IComponent
    {

    }

    // Instanced from a prefab / pool component
    [Game]
    public class ViewComponent : IComponent
    {
        public GameObject gameObject;
    }

    // Track position and rotation of this game object - controlled characters basically
    [Game]
    public class TrackGameObject : IComponent
    {
        public GameObject gameObject;
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

    // Logical in game position
    [Game]
    public class LogicalPositionComponent : IComponent
    {
        public Vector2Int position;
    }

    // Game itself
    [Command]
    public class ChipGameComponent : IComponent
    {
        public Chip2048Game chip2048Game;
    }


}