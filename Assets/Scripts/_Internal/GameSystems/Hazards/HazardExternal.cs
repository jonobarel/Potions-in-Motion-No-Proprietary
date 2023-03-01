namespace ZeroPrep.MineBuddies
{
    public class HazardExternal : HazardBase
    {

        

        
        private float _speed;
        
        public HazardExternal(float speed, Managers.HazardType type, float startingHealth = 1f) : base(type, startingHealth)
        {
            _speed = speed;
        }
        
        public override void AdvanceAction(float delta)
        {
            base.AdvanceAction(delta * _speed);
        }
    }
}