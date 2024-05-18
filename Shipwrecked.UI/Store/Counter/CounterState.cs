using Fluxor;

namespace Shipwrecked.UI.Store.Counter;

[FeatureState] // allows setup to find this class
public class CounterState
{
    public int ClickCount { get; }

    private CounterState() {} // Required for creating initial state

    public CounterState(int clickCount)
    {
        ClickCount = clickCount;
    }
}