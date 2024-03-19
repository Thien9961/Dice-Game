

public class TarotCell : Cell
{
    public TarotSelectionWindow window;
    public override void Trigger()
    {
        Instantiate(window,UIManager.instance.transform);
    }


}
