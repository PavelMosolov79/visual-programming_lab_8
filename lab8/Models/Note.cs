using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Avalonia.Media.Imaging;
using System.IO;

namespace lab8.Models
{
    public class Note : ReactiveObject
    {
        string header;
        public string Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }
        string task;
        public string Task
        {
            get => task;
            set => this.RaiseAndSetIfChanged(ref task, value);
        }

        Bitmap image;
        public Bitmap Image
        {
            get => image;
            private set => this.RaiseAndSetIfChanged(ref image, value);
        }
        
        public string Path
        {
            get;
            private set;
        }

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => this.RaiseAndSetIfChanged(ref isSelected, value);
        }

        public void setImage(string path)
        {
            if (!File.Exists(path)) return;
            Path = path;
            Image = new Bitmap(path);
        }
        public Note()
        {
            header = "Новая задача";
            Task = "Опишите задачу";
            Path = "";
        }
    }
}
