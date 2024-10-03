public class HealthBarFast : HealthBarAbstract 
{
    public override void ChangeSlider(float healthValue)
    {
        Slider.value = healthValue;
    }
}
