using BlazorState.ViewModel;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorState.Shared
{
    public class HealthModelBase : ComponentBase, IDisposable
    {
        [Inject]
        public IHealthModel Model { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Model.PropertyChanged += Model_PropertyChanged;
            }
            base.OnAfterRender(firstRender);
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Model.PropertyChanged -= Model_PropertyChanged;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }
}
