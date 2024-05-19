using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Store;
using Shipwrecked.UI.Store.Counter;

namespace Shipwrecked.UI.Pages;

public partial class Counter
{
    [Inject]
    private IState<CounterState> CounterState { get; set; }
    
    [Inject]
    private IDispatcher Dispatcher { get; set; }
    
    private void IncrementCounter()
    {
        Console.WriteLine("OnClick called!");
        var action = new IncrementCounterAction();
        Dispatcher.Dispatch(action);
    }
}