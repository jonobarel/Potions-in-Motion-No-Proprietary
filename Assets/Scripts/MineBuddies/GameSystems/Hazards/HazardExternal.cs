namespace ZeroPrep.MineBuddies
{
    public class HazardExternal : HazardBase
    {

        private float _speed;
        
        
        public HazardExternal(float speed, HazardType type, HazardManagerGO.InteractionType interactionType, float startingHealth = 1f) : base(type, interactionType, startingHealth)
        {
            _speed = speed;
        }
        
        public override void AdvanceAction(float delta)
        {
            base.AdvanceAction(delta * _speed);
        }
    }
}