namespace ZeroPrep.MineBuddies
{
    public class HazardExternal : HazardBase
    {
        private Managers.HazardType _type;
        
        public Managers.HazardType Type => _type;
        
        private float _speed;
        
        public HazardExternal(float speed, Managers.HazardType type) : base()
        {
            _type = type;
            _speed = speed;
        }
        
        public override void Advance(float delta)
        {
            base.Advance(delta * _speed);
        }
    }
}