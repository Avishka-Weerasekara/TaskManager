using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager.Services
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _nextId = 1;

        public List<TaskItem> GetAll() => _tasks;

        public TaskItem AddTask(string name)
        {
            var task = new TaskItem { Id = _nextId++, Name = name, Completed = false };
            _tasks.Add(task);
            return task;
        }
    }
}
