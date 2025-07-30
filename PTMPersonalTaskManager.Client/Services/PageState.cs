using PTMPersonalTaskManager.Client.Components.Pages.Menupage.Note;

namespace PTMPersonalTaskManager.Client.Services
{
    public class PageState
    {
        public bool ShowProfile { get; private set; }
        public bool HideCard { get; private set; }
        public bool DisplayNotes { get; private set; }
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
            HideCard = true;
            DisplayNotes = false;
            NotifyChanges();
        }
        private void NotifyChanges() => OnChange?.Invoke();

        public void Hide()
        {
            HideCard = false;
            DisplayNotes = true;
            NotifyChanges();
        }
    }
}
