using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Project
{
	public static class UniversityStorer
	{
		public static MyVector<ITeacher> teachers;
		public static MyVector<IStudent> students;
		public static MyVector<IRoom> rooms;
		public static MyVector<IMyClass> classes;
		
		public static void FillV()
		{

			rooms = new MyVector<IRoom>();
			classes = new MyVector<IMyClass>();
			students = new MyVector<IStudent>();
			teachers = new MyVector<ITeacher>();

			rooms.AddObject(new Room(107, "lecture", new List<IMyClass>()));
			rooms.AddObject(new Room(204, "training", new List<IMyClass>()));
			rooms.AddObject(new Room(21, "lecture", new List<IMyClass>()));
			rooms.AddObject(new Room(123, "laboratory", new List<IMyClass>()));
			rooms.AddObject(new Room(404, "lecture", new List<IMyClass>()));
			rooms.AddObject(new Room(504, "training", new List<IMyClass>()));
			rooms.AddObject(new Room(73, "laboratory", new List<IMyClass>()));

			classes.AddObject(new MyClass("Diabolical Mathematics 2", "MD2", 2, new List<ITeacher>(), new List<IStudent>()));
			classes.AddObject(new MyClass("Routers Description", "RD", 1, new List<ITeacher>(), new List<IStudent>()));
			classes.AddObject(new MyClass("Introduction to cables", "WDK", 5, new List<ITeacher>(), new List<IStudent>()));
			classes.AddObject(new MyClass("Advanced Cooking 3", "AC3", 3, new List<ITeacher>(), new List<IStudent>()));


			rooms[0].AddClass(classes[0]);
			rooms[0].AddClass(classes[1]);
			rooms[0].AddClass(classes[2]);
			rooms[0].AddClass(classes[3]);
			rooms[1].AddClass(classes[2]);
			rooms[1].AddClass(classes[3]);
			rooms[2].AddClass(classes[1]);
			rooms[3].AddClass(classes[1]);
			rooms[3].AddClass(classes[2]);
			rooms[4].AddClass(classes[0]);
			rooms[4].AddClass(classes[2]);
			rooms[4].AddClass(classes[1]);
			rooms[5].AddClass(classes[0]);
			rooms[6].AddClass(classes[3]);

			students.AddObject(new Student(new List<string> { "Robert" }, "Kielbica", 3, "S1", new List<IMyClass>()));
			students.AddObject(new Student(new List<string> { "Archibald", "Agapios" }, "Linux", 7, "S2", new List<IMyClass>()));
			students.AddObject(new Student(new List<string> { "Angrboða" }, "Kára", 31, "S3", new List<IMyClass>()));
			students.AddObject(new Student(new List<string> { "Olympos" }, "Andronikos", 5, "S4", new List<IMyClass>()));
			students.AddObject(new Student(new List<string> { "Mac", "Rhymes" }, "Pickuppicker", 6, "S5", new List<IMyClass>()));


			students[0].AddClass(classes[0]);
			students[0].AddClass(classes[2]);
			students[1].AddClass(classes[0]);
			students[1].AddClass(classes[2]);
			students[1].AddClass(classes[3]);
			students[2].AddClass(classes[1]);
			students[2].AddClass(classes[2]);
			students[3].AddClass(classes[1]);
			students[3].AddClass(classes[2]);
			students[3].AddClass(classes[3]);
			students[4].AddClass(classes[0]);
			students[4].AddClass(classes[2]);
			students[4].AddClass(classes[3]);

			teachers.AddObject(new Teacher(new List<string> { "Tomas" }, "Cherrmann", "MiB", "P1", new List<IMyClass>()));
			teachers.AddObject(new Teacher(new List<string> { "Jon" }, "Tron", "TiB", "P2", new List<IMyClass>()));
			teachers.AddObject(new Teacher(new List<string> { "William", "Joseph" }, "Blazkowicz", "GiB", "P3", new List<IMyClass>()));
			teachers.AddObject(new Teacher(new List<string> { "Arkadiusz", "Amadeusz" }, "Kamiński", "KiB", "P4", new List<IMyClass>()));
			teachers.AddObject(new Teacher(new List<string> { "Cooking" }, "Mama", "GiB", "P5", new List<IMyClass>()));

			teachers[0].AddClass(classes[3]);
			teachers[1].AddClass(classes[0]);
			teachers[2].AddClass(classes[1]);
			teachers[2].AddClass(classes[2]);
			teachers[3].AddClass(classes[2]);
			teachers[4].AddClass(classes[3]);

			classes[0].AddTeacher(teachers[1]);
			classes[1].AddTeacher(teachers[2]);
			classes[2].AddTeacher(teachers[2]);
			classes[2].AddTeacher(teachers[4]);
			classes[3].AddTeacher(teachers[0]);

			classes[0].AddStudent(students[0]);
			classes[0].AddStudent(students[1]);
			classes[0].AddStudent(students[4]);
			classes[1].AddStudent(students[2]);
			classes[1].AddStudent(students[3]);
			classes[2].AddStudent(students[0]);
			classes[2].AddStudent(students[1]);
			classes[2].AddStudent(students[2]);
			classes[2].AddStudent(students[3]);
			classes[2].AddStudent(students[4]);
			classes[3].AddStudent(students[1]);
			classes[3].AddStudent(students[3]);
			classes[3].AddStudent(students[4]);
		}
	}
}

