using Entitas;

namespace Fun2048
{
    public class ExpiredInputCleanupSystem : ICleanupSystem
    {
        readonly IGroup<InputEntity> expiredInput;

        public ExpiredInputCleanupSystem(Contexts contexts)
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