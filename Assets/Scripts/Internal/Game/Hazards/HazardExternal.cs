namespace ZeroPrep.MineBuddies
{
    public class HazardExternal : HazardBase
    {

        

        
        private float _speed;
        
        public HazardExternal(float speed, Managers.HazardType type) : base(type)
        {
            _speed = speed;
        }
        
        public override void Advance(float delta)
        {
            base.Advance(delta * _speed);
        }
    }
}