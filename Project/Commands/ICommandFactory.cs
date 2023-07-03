using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project
{
    public interface ICommandFactory
    {
        public ICommand Create(string[] args, University u,string[]args2=null);

    }
    public class QueueFactory:ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new QueueCommand();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class HistoryFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new HistoryCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class UndoFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new UndoCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class RedoFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new RedoCommand();
            command.Init(args, u, args2);
            return command;
        }
    }

    public class ExportFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ExportCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class ImportFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ImportCommand();
            command.Init(args, u, args2);
            return command;
        }
    }

    public class ExitFactory:ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ExitCommand();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class ListFactory:ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ListCommand();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class ListStudentsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ListStudents();
            command.Init(args, u,args2);
            return command;
        }

    }
    public class ListTeachersFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ListTeachers();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class ListClassesFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ListClasses();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class ListRoomsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new ListRooms();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class FindFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new FindCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class FindStudentsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new FindStudents();
            command.Init(args, u,args2);
            return command;
        }

    }
    public class FindTeachersFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new FindTeachers();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class FindClassesFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new FindClasses();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class FindRoomsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new FindRooms();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class AddFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u,string[] args2 = null)
        {
            ICommand command = new AddCommand();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class AddStudentFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new AddStudent();
            command.Init(args, u,args2);
            return command;
        }

    }
    public class AddTeacherFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new AddTeacher();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class AddClassFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new AddClass();
            command.Init(args, u,args2);
            return command;
        }
    }
    public class AddRoomFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u,string[] args2 = null)
        {
            ICommand command = new AddRoom();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class  EditFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new EditCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class EditStudentsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new EditStudents();
            command.Init(args, u,args2);
            return command;
        }

    }
    public class EditTeachersFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new EditTeachers();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class EditClassesFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new EditClasses();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class EditRoomsFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new EditRooms();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class DeleteFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new DeleteCommand();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class DeleteStudentFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new DeleteStudent();
            command.Init(args, u, args2);
            return command;
        }

    }
    public class DeleteTeacherFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new DeleteTeacher();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class DeleteClassFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new DeleteClass();
            command.Init(args, u, args2);
            return command;
        }
    }
    public class DeleteRoomFactory : ICommandFactory
    {
        public ICommand Create(string[] args, University u, string[] args2 = null)
        {
            ICommand command = new DeleteRoom();
            command.Init(args, u, args2);
            return command;
        }
    }
}