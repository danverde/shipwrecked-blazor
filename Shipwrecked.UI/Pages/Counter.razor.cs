using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Store.Counter;

namespace Shipwrecked.UI.Pages;

// TODO can I inherit FluxorComponent here???
public partial class Counter : FluxorComponent
{
    [Inject]
    private IState<CounterState> CounterState { get; set; }
    
    [Inject]
    private IDispatcher Dispatcher { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        // Dispatcher.Dispatch(new FetchDataAction()); // TODO not sure where the FetchDataAction is supposed to come from...
    }

    private void IncrementCounter()
    {
        var action = new IncrementCounterAction();
        Dispatcher.Dispatch(action);
    }
}