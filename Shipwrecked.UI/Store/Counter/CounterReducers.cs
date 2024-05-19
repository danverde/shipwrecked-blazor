using Fluxor;

namespace Shipwrecked.UI.Store.Counter;

public class CounterReducers
{
    [ReducerMethod]
    public static CounterState IncrementCounterState(CounterState state, IncrementCounterAction action)
    {
        Console.WriteLine("Reducer called!");
        return new CounterState(clickCount: state.ClickCount + 1);
    }
}