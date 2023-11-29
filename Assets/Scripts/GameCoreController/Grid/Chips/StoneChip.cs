
namespace GameCoreController
{
    public class StoneChip : AChip
    {
        private int _health;

        public StoneChip(int health)
        {
            _health = health;
        }

        public bool IsAlive()
        {
            return _health > 0;
        }

        public void SetHealth(int health)
        {
            _health = health;
        }

        public void DecreaseHealth()
        {
            _health -= 1;
        }

        public int GetHealth()
        {
            return _health;
        }

    }
}
