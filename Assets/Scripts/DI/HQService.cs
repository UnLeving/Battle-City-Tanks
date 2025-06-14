using System;
using UnityEngine;

namespace DI
{
    public class HQService
    {
        private readonly HQView hqView;
    
        public event Action OnHQDestroyed;
    
        public bool IsDestroyed { get; private set; }

        public HQService(HQView hqView)
        {
            this.hqView = hqView;
            this.hqView.Initialize(this);
        }

        public void TakeDamage()
        {
            if (IsDestroyed) return;
        
            Debug.Log("HQ destroyed!");
        
            IsDestroyed = true;
            hqView.ShowDestroyedState();
        
            OnHQDestroyed?.Invoke();
        }
    }
}