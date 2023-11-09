using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_8
{
    enum Position
    {
        Customer,
        TeamLeader,
        Executor
    }
    internal class Person
    {
        private string name { get; set; }
        private Position position { get; set; }

        private Task task { get; set; }
        public Person(string name, Position position)
        {
            this.name = name;
            this.position = position;
        }
        public void AddTask(Task task)
        {
            this.task = task;
        }
    }
    internal class Report
    {
        private string text { get; set; }
        private DateTime time;
        public DateTime Time
        {
            get
            {
                return time;
            }
        }
        private Person executor { get; set; }
        private bool approved { get; set; }
        public Report(string text, Person executor)
        {
            this.text = text;
            time = DateTime.Now;
            this.executor = executor;
        }
        public bool Approve()
        {
            approved = true;
            return approved;
        }

    }
    enum TaskStatus
    {
        Appointed,
        InProgress,
        Inspecion,
        Completed
    }
    internal class Task
    {
        private string description { get; set; }
        private DateTime deadline { get; set; }
        private Person team_leader { get; set; }
        private Person executor { get; set; }
        private TaskStatus status;
        public TaskStatus Status
        {
            get
            {
                return status;
            }
        }
        private List<Report> reports { get; set; }
        public Task(string description, DateTime deadline, Person team_leader)
        {
            this.description = description;
            this.deadline = deadline;
            this.team_leader = team_leader;
            status = TaskStatus.Appointed;
            reports = new List<Report>();
        }
        public void StartTask(Person executor)
        {
            status = TaskStatus.InProgress;
            this.executor = executor;
        }
        public void InspectTask()
        {
            status = TaskStatus.Inspecion;
        }
        public void CloseTask()
        {
            status = TaskStatus.Completed;
        }
        public void AddReport(Report report)
        {
            reports.Add(report);
        }
        public void DelegateTask(Person executor)
        {
            if (status == TaskStatus.Appointed)
            {
                this.executor = executor;
            }
        }
        public void RejectTask()
        {
            executor = null;
            status = TaskStatus.Appointed;
        }
        public void GenerateReports()
        {
            InspectTask();
                Report report = new Report("отчет", executor);
                AddReport(report);
                if (report.Approve())
                {
                    CloseTask();
                    Console.WriteLine("\nОтчет утвержден");
                }
                else
                {
                    StartTask(executor);
                    Console.WriteLine("\nОтчет отправлен на доработку");                
                }
            }
        }
    enum Status
    {
        Project,
        Execution,
        Closed
    }
    internal class Project
    {
        private string description { get; set; }
        private DateTime deadline { get; set; }
        private Person customer { get; set; }
        private Person team_leader { get; set; }
        private List<Task> tasks { get; set; }
        private Status status;
        public Status Status
        {
            get
            {
                return status;
            }
        }
        public Project(string description, DateTime deadline, Person customer, Person team_leader)
        {
            this.description = description;
            this.deadline = deadline;
            this.customer = customer;
            this.team_leader = team_leader;
            tasks = new List<Task>();
            status = Status.Project;
        }
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }
        public void StartProject()
        {
            status = Status.Execution;
        }
        public void CloseProject()
        {
            status = Status.Closed;
        }
        public void IsCompleted()
        {
            CloseProject();
            foreach (Task task in tasks)
            {
                if (task.Status != TaskStatus.Completed)
                {
                    StartProject();
                    break;
                }
            }
            
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person customer = new Person("Дух Андеграунда", Position.Customer);
            Person team_leader = new Person("Павел Ивлев", Position.TeamLeader);
            Person executor1 = new Person("Максим Синицын", Position.Executor);
            Person executor2 = new Person("Блев", Position.Executor);
            Person executor3 = new Person("Кальмар", Position.Executor);
            Person executor4 = new Person("Бразилец", Position.Executor);
            Person executor5 = new Person("Сптщ", Position.Executor);
            Person executor6 = new Person("Джед", Position.Executor);
            Person executor7 = new Person("Румын", Position.Executor);
            Person executor8 = new Person("Заги Бок", Position.Executor);
            Person executor9 = new Person("Оуджи Сан", Position.Executor);

            List<Project> projects = new List<Project>();
            Project project1 = new Project("Написать хип-хоп альбом", new DateTime(2099, 12, 20), customer, team_leader);
            projects.Add(project1);
            Task task1 = new Task("Написать текста себе и кальмару", new DateTime(2023, 11, 20), team_leader);
            Task task2 = new Task("Написать текста", new DateTime(2023, 11, 22), team_leader);
            Task task3 = new Task("Написать текста", new DateTime(2023, 11, 24), team_leader);
            Task task4 = new Task("Зачитать текста павла и не выпендриваться", new DateTime(2023, 11, 25), team_leader);
            Task task5 = new Task("Написать текста", new DateTime(2023, 11, 28), team_leader);
            Task task6 = new Task("Написать текста", new DateTime(2023, 11, 20), team_leader);
            Task task7 = new Task("Обеспечить комманду стаффом", new DateTime(2023, 12, 10), team_leader);
            Task task8 = new Task("Респектнуть за стафф", new DateTime(2023, 11, 25), team_leader);
            Task task9 = new Task("Сделать шикарное интро и  аутро", new DateTime(2023, 12, 5), team_leader);
            Task task10 = new Task("Сочинить биты", new DateTime(2023, 12, 15), team_leader);

            project1.AddTask(task1);
            team_leader.AddTask(task1);
            project1.AddTask(task2);
            executor1.AddTask(task2);
            project1.AddTask(task3);
            executor2.AddTask(task3);
            project1.AddTask(task4);
            executor3.AddTask(task4);
            project1.AddTask(task5);
            executor4.AddTask(task5);
            project1.AddTask(task6);
            executor5.AddTask(task6);
            project1.AddTask(task7);
            executor6.AddTask(task7);
            project1.AddTask(task8);
            executor7.AddTask(task9);
            project1.AddTask(task9);
            executor8.AddTask(task9);
            project1.AddTask(task10);
            executor9.AddTask(task10);

            project1.StartProject();

            task1.StartTask(team_leader);
            task2.StartTask(executor1);
            task3.StartTask(executor2);
            task4.StartTask(executor3);
            task5.StartTask(executor4);
            task6.StartTask(executor5);
            task7.StartTask(executor6);
            task8.StartTask(executor7);
            task9.StartTask(executor8);
            task10.StartTask(executor9);

            task1.GenerateReports();
            task2.GenerateReports();
            task3.GenerateReports();
            task4.GenerateReports();
            task5.GenerateReports();
            task6.GenerateReports();
            task7.GenerateReports();
            task8.GenerateReports();
            task9.GenerateReports();
            task10.GenerateReports();

            project1.IsCompleted();

            if(project1.Status == Status.Closed)
            {
                Console.WriteLine("\nВаш проект готов!");
            }
            else
            {
                Console.WriteLine("\nПроект еще не готов!");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
