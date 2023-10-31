using Entitas;

namespace Fun2048
{
    public class ExpiredCleanupSystem : ICleanupSystem
    {
        readonly IGroup<InputEntity> expiredInput;

        public ExpiredCleanupSystem(Contexts contexts)
        {
            expiredInput = contexts.input.GetGroup(
                InputMatcher.AllOf(
                    InputMatcher.ExpiredTag
                )
            );
        }

        public void Cleanup()
        {
            foreach (InputEntity e in expiredInput.GetEntities())
            {
                e.Destroy();
            }
        }
    }
}