namespace TestNAudio.Model.Light
{
    public class LightTimeAnimation : LightDecorator
    {
        public LightTimeAnimation(ILightProvider lightProvider, string name = null)
            : base(lightProvider, name)
        {

        }
    }
}