namespace PTMPersonalTaskManager.Client.Services
{
    public class PageState
    {
     public bool ShowProfile { get; private set; }
        public event Action? OnChange;

        public void Show()
        {
            Reset();
            ShowProfile = true;
            NotifyChanges();
        }
        public void Reset()
        {
            ShowProfile = false;
            NotifyChanges();
        }
        private void NotifyChanges() => OnChange?.Invoke();
    }
}
