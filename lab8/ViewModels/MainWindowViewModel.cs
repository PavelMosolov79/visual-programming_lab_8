using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using lab8.Models;
using ReactiveUI;
using System.IO;

namespace lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //��������� ��������������� ������
        ObservableCollection<Note> PLAN;
        public ObservableCollection<Note> plan
        {
            get => PLAN;
            set => this.RaiseAndSetIfChanged(ref PLAN, value);
        }
        //��������� ������ � ��������
        ObservableCollection<Note> INPROGRESS;
        public ObservableCollection<Note> inprogress
        {
            get => INPROGRESS;
            set => this.RaiseAndSetIfChanged(ref INPROGRESS, value);
        }
        //��������� ����������� ������
        ObservableCollection<Note> COMPLETE;
        public ObservableCollection<Note> complete
        {
            get => COMPLETE;
            set => this.RaiseAndSetIfChanged(ref COMPLETE, value);
        }

        //������� ����� ������
        public MainWindowViewModel()
        {
            plan = new ObservableCollection<Note>();
            inprogress = new ObservableCollection<Note>();
            complete = new ObservableCollection<Note>();
        }

        //�������
        public void Clear()
        {
            plan.Clear();
            inprogress.Clear();
            complete.Clear();
        }

        //��������� � ���� ���� ������
        public void SaveFile(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(plan.Count);
                foreach(var note in plan)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index>=0; index = note.Task.IndexOf('\n', index+1), lines++);
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
                sw.WriteLine(inprogress.Count);
                foreach (var note in inprogress)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index >= 0; index = note.Task.IndexOf('\n', index + 1), lines++) ;
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
                sw.WriteLine(complete.Count);
                foreach (var note in complete)
                {
                    sw.WriteLine(note.Header);
                    var lines = 1;
                    for (int index = note.Task.IndexOf('\n'); index >= 0; index = note.Task.IndexOf('\n', index + 1), lines++) ;
                    sw.WriteLine(lines);
                    sw.WriteLine(note.Task);
                    sw.WriteLine(note.Path);
                }
            }
        }

        //��������� �� ����� ���� ������
        public void LoadFile(string path)
        {
            if (!File.Exists(path)) return;
            Clear();

            using(StreamReader sr = File.OpenText(path))
            {
                var planCount = int.Parse(sr.ReadLine());
                for(int i=0; i< planCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for(int j=0; j<taskLines; ++j)
                    {
                        task += sr.ReadLine()+"\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    plan.Add(item);
                }

                var inprogressCount = int.Parse(sr.ReadLine());
                for (int i = 0; i < inprogressCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for (int j = 0; j < taskLines; ++j)
                    {
                        task += sr.ReadLine() + "\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    inprogress.Add(item);
                }

                var completeCount = int.Parse(sr.ReadLine());
                for (int i = 0; i < completeCount; ++i)
                {
                    var title = sr.ReadLine();
                    var taskLines = int.Parse(sr.ReadLine());
                    var task = "";
                    for (int j = 0; j < taskLines; ++j)
                    {
                        task += sr.ReadLine() + "\n";
                    }
                    task = task.Substring(0, task.Length - 1);
                    var img = sr.ReadLine();
                    var item = new Note();
                    item.Header = title;
                    item.Task = task;
                    item.setImage(img);
                    complete.Add(item);
                }
            }
        }

        //������� ������
        public void DeleteClk(Note note)
        {
            if(!plan.Remove(note))
            {
                if(!inprogress.Remove(note))
                {
                    complete.Remove(note);
                }
            }
        }

        //��������� � ������� ��������������� ������ ����� ������
        public void AddNotePlan()
        {
            plan.Add(new Note());
        }
        //��������� � ������� ������ � �������� ����� ������
        public void AddNoteInProgress()
        {
            inprogress.Add(new Note());
        }
        //��������� � ������� ����������� ������ ����� ������
        public void AddNoteComplete()
        {
            complete.Add(new Note());
        }
    }
}
