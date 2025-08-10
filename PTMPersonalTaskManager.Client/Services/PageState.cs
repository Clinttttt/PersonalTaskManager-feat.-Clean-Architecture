using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Identity.Client;
using PTMPersonalTaskManager.Client.Components.Pages.Menupage.Note;
using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Client.Services
{
    public class PageState
    {
        public bool ShowProfile { get; private set; }
        public bool HideCard { get; set; } = true;
        public bool DisplayNotes { get; set; } = false;
        public bool Login { get; private set; }
        public bool ShowRegister { get; private set; }
       

        public event Action? OnChange;
        public enum PageView
        {
            Note,
            AddProfile,
            CalendarPage,
            CompletedPage,
            HomePage,
            MenuOptions,
            PriorityPage,
            ProfilePage,
            SearchPage,
            Account,
            Card,
            NoteSpecificDisplay, 
            NoteCardDisplay,
            None,
            ShowHeader,
        
            
        }
    


        public PageView CurrentView { get; set; } = PageView.HomePage;

        public void SetView(PageView view)
        {
            CurrentView = view;
            OnChange?.Invoke();
        }





        public event Func<Task>? Onchanges;
        public Guid RenderKey { get; private set; } = Guid.NewGuid();

        public async Task SetNote(PageView view)
        {
            await Task.Delay(100);
            CurrentView = view;
            RenderKey = Guid.NewGuid(); 

            if (Onchanges is not null)
            {
                await Onchanges.Invoke();
            }
        }

        public async Task Back()
        {
            await Task.Delay(50);
            await SetNote(PageState.PageView.NoteCardDisplay);
        }
        public void ResetView()
        {
            CurrentView = PageView.None;
            OnChange?.Invoke();
        }

    
        public void Reset()
        {
           
            Login = false;
            ShowRegister = false;
            NotifyChanges();
        }
        private void NotifyChanges() => OnChange?.Invoke();

        public void Hide()
        {
            Login = false;
            HideCard = false;
            DisplayNotes = true; 
            NotifyChanges();
        }
        public void HideLogin()
        {
            Login = false;
            ShowRegister = true;
            NotifyChanges();
        }

        public enum Account
        {
            Login,
            Register,
            
        }
        public Account AccountView { get; set; } = Account.Login;
             
        public void ViewAccount(Account view)
        {
            AccountView = view;
            OnChange?.Invoke();
        }

    

    }
}


