using Avalonia.Controls;
using lab8.Models;

namespace lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("ExitMenu").Click += delegate
            {
                this.Close();
            };
            this.FindControl<MenuItem>("NewMenu").Click += delegate
            {
                var ctx = this.DataContext as lab8.ViewModels.MainWindowViewModel;
                ctx.Clear();
            };
            this.FindControl<MenuItem>("SaveMenu").Click += async delegate
            {
                var dlg = new SaveFileDialog()
                {
                    Title = "Save File",
                    Filters = null
                }.ShowAsync(this.VisualRoot as MainWindow);
                string path = await dlg;

                var ctx = this.DataContext as lab8.ViewModels.MainWindowViewModel;
                if (path != null) ctx.SaveFile(path);
            };
            this.FindControl<MenuItem>("LoadMenu").Click += async delegate
            {
                var dlg = new OpenFileDialog()
                {
                    Title = "Open File",
                    Filters = null
                }.ShowAsync(this.VisualRoot as MainWindow);
                string[] path = await dlg;

                var ctx = this.DataContext as lab8.ViewModels.MainWindowViewModel;
                if (path != null) ctx.LoadFile(path[0]);
            };
            this.FindControl<MenuItem>("AboutMenu").Click += async delegate
            {
                await new AboutWindow().ShowDialog(this.VisualRoot as MainWindow);
            };
        }

        public async void AddClk(Note note)
        {
            var filter = new FileDialogFilter()
            {
                Name = "Images",
                Extensions = { "png", "bmp", "jpg" }
            };
            var dlg = new OpenFileDialog()
            {
                Title = "Open Image",
                Filters = { filter }
            }.ShowAsync(this.VisualRoot as MainWindow);
            var path = await dlg;
            if (path != null) note.setImage(path[0]);
        }
    }
}
