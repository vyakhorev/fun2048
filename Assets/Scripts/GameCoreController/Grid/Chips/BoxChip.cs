
namespace GameCoreController
{
    public class BoxChip : AChip
    {
        private int _health;

        public BoxChip(int health)
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
