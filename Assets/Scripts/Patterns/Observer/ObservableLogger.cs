using System.Collections.Generic;
using UnityEngine;

public class ObservableLogger : MonoBehaviour, IObserver
{
    private List<IObservable> _observables;

    public ObservableLogger()
    {
        _observables = new List<IObservable>();
    }

    public ObservableLogger(IObservable observable)
    {
        _observables = new List<IObservable>{observable};
        observable.OnChanged += OnChanged;
    }
    public ObservableLogger(IObservable[] observables)
    {
        _observables = new List<IObservable>(observables);

        foreach (IObservable observable in _observables)
        {
            observable.OnChanged += OnChanged;
        }
    }
    
    public void AddObservable(IObservable observable)
    {
        if (_observables.Contains(observable))
            return;

        _observables.Add(observable);
        observable.OnChanged += OnChanged;
    }
    
    public void Dispose()
    {
        foreach (IObservable observable in _observables)
        {
            observable.OnChanged -= OnChanged;
        }
    }
    
    protected virtual void OnChanged(object obj)
    {
        
    }
}
