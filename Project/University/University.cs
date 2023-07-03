using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Project
{
	public class University
	{
		public List<ITeacher> teachers;
		public List<IStudent> students;
		public List<IRoom> rooms;
		public List<IMyClass> classes;
		public University()
		{
			teachers = new List<ITeacher>();
			students = new List<IStudent>();
			rooms = new List<IRoom>();
			classes = new List<IMyClass>();
		}
		public void PrintAll()
		{
			Console.WriteLine("Rooms: ");
			foreach (var r in rooms)
			{
				Console.WriteLine(r);
				Console.WriteLine("\tClasses:");
				foreach (var c in r.GetClasses())
				{
					Console.WriteLine("\t" + c);
					Console.WriteLine("\t\tTeachers:");
					foreach (var v in c.GetTeachers())
						Console.WriteLine("\t\t" + v);
					Console.WriteLine("\t\tStudents:");
					foreach (var v in c.GetStudents())
						Console.WriteLine("\t\t" + v);
				}
			}
		}
		public void Print2()
        {
			foreach(var v in classes)
            {
				bool a = false;
				bool b = false;
				IStudent? st=null;
				ITeacher? te=null;
				foreach(var s in v.GetStudents())
                {
					if (s.GetNames().Count == 2)
                    {
						a = true;
						st = s;
						break;
					}
                }
				foreach(var t in v.GetTeachers())
                {
					if (t.GetNames().Count == 2)
                    {
						b = true;
						te = t;
						break;
					}
                }
				if(a==true&&b==true)
                {
					Console.WriteLine(v);
					//if (st != null)
						Console.WriteLine(st);
					//if(te!=null)
						Console.WriteLine(te);
                }
            }
		}
		//public void Fill1()
		//{
		//	int n;
		//	n = 107;
			
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 204;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 21;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 123;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 404;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 504;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);
		//	n = 73;
		//	rooms.Add((IRoom)HashMap.hashmapref[n.ToString()]);


		//	classes.Add((IMyClass)HashMap.hashmapref["MD2"]);
		//	classes.Add((IMyClass)HashMap.hashmapref["RD"]);
		//	classes.Add((IMyClass)HashMap.hashmapref["WDK"]);
		//	classes.Add((IMyClass)HashMap.hashmapref["AC3"]);

		//	rooms[0].AddClass(classes[0]);
		//	rooms[0].AddClass(classes[1]);
		//	rooms[0].AddClass(classes[2]);
		//	rooms[0].AddClass(classes[3]);
		//	rooms[1].AddClass(classes[2]);
		//	rooms[1].AddClass(classes[3]);
		//	rooms[2].AddClass(classes[1]);
		//	rooms[3].AddClass(classes[1]);
		//	rooms[3].AddClass(classes[2]);
		//	rooms[4].AddClass(classes[0]);
		//	rooms[4].AddClass(classes[2]);
		//	rooms[4].AddClass(classes[1]);
		//	rooms[5].AddClass(classes[0]);
		//	rooms[6].AddClass(classes[3]);

		//	students.Add((IStudent)HashMap.hashmapref["S1"]);
		//	students.Add((IStudent)HashMap.hashmapref["S2"]);
		//	students.Add((IStudent)HashMap.hashmapref["S3"]);
		//	students.Add((IStudent)HashMap.hashmapref["S4"]);
		//	students.Add((IStudent)HashMap.hashmapref["S5"]);


		//	students[0].AddClass(classes[0]);
		//	students[0].AddClass(classes[2]);
		//	students[1].AddClass(classes[0]);
		//	students[1].AddClass(classes[2]);
		//	students[1].AddClass(classes[3]);
		//	students[2].AddClass(classes[1]);
		//	students[2].AddClass(classes[2]);
		//	students[3].AddClass(classes[1]);
		//	students[3].AddClass(classes[2]);
		//	students[3].AddClass(classes[3]);
		//	students[4].AddClass(classes[0]);
		//	students[4].AddClass(classes[2]);
		//	students[4].AddClass(classes[3]);

		//	teachers.Add((ITeacher)HashMap.hashmapref["P1"]);
		//	teachers.Add((ITeacher)HashMap.hashmapref["P2"]);
		//	teachers.Add((ITeacher)HashMap.hashmapref["P3"]);
		//	teachers.Add((ITeacher)HashMap.hashmapref["P4"]);
		//	teachers.Add((ITeacher)HashMap.hashmapref["P5"]);

		//	teachers[0].AddClass(classes[3]);
		//	teachers[1].AddClass(classes[0]);
		//	teachers[2].AddClass(classes[1]);
		//	teachers[2].AddClass(classes[2]);
		//	teachers[3].AddClass(classes[2]);
		//	teachers[4].AddClass(classes[3]);

		//	classes[0].AddTeacher(teachers[1]);
		//	classes[1].AddTeacher(teachers[2]);
		//	classes[2].AddTeacher(teachers[2]);
		//	classes[2].AddTeacher(teachers[4]);
		//	classes[3].AddTeacher(teachers[0]);

		//	classes[0].AddStudent(students[0]);
		//	classes[0].AddStudent(students[1]);
		//	classes[0].AddStudent(students[4]);
		//	classes[1].AddStudent(students[2]);
		//	classes[1].AddStudent(students[3]);
		//	classes[2].AddStudent(students[0]);
		//	classes[2].AddStudent(students[1]);
		//	classes[2].AddStudent(students[2]);
		//	classes[2].AddStudent(students[3]);
		//	classes[2].AddStudent(students[4]);
		//	classes[3].AddStudent(students[1]);
		//	classes[3].AddStudent(students[3]);
		//	classes[3].AddStudent(students[4]);

		//}
		public void Fill2()
		{
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(107, "lecture", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(204, "training", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(21, "lecture", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(123, "laboratory", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(404, "lecture", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(504, "training", new List<IMyClass>())));
			rooms.Add(new Room_Adapter2(new Room_Adaptee2(73, "laboratory", new List<IMyClass>())));

			classes.Add((IMyClass)HashMap2.hashmap["MD2"]);
			classes.Add((IMyClass)HashMap2.hashmap["RD"]);
			classes.Add((IMyClass)HashMap2.hashmap["WDK"]);
			classes.Add((IMyClass)HashMap2.hashmap["AC3"]);

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

			students.Add((IStudent)HashMap2.hashmap["S1"]);
			students.Add((IStudent)HashMap2.hashmap["S2"]);
			students.Add((IStudent)HashMap2.hashmap["S3"]);
			students.Add((IStudent)HashMap2.hashmap["S4"]);
			students.Add((IStudent)HashMap2.hashmap["S5"]);


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

			teachers.Add((ITeacher)HashMap2.hashmap["P1"]);
			teachers.Add((ITeacher)HashMap2.hashmap["P2"]);
			teachers.Add((ITeacher)HashMap2.hashmap["P3"]);
			teachers.Add((ITeacher)HashMap2.hashmap["P4"]);
			teachers.Add((ITeacher)HashMap2.hashmap["P5"]);

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

		public void Fill()
		{
			rooms.Add(new Room(107, "lecture", new List<IMyClass>()));
			rooms.Add(new Room(204, "training", new List<IMyClass>()));
			rooms.Add(new Room(21, "lecture", new List<IMyClass>()));
			rooms.Add(new Room(123, "laboratory", new List<IMyClass>()));
			rooms.Add(new Room(404, "lecture", new List<IMyClass>()));
			rooms.Add(new Room(504, "training", new List<IMyClass>()));
			rooms.Add(new Room(73,"laboratory", new List<IMyClass>()));

			classes.Add(new MyClass("Diabolical Mathematics 2", "MD2", 2, new List<ITeacher>(), new List<IStudent>()));
			classes.Add(new MyClass("Routers Description", "RD", 1, new List<ITeacher>(), new List<IStudent>()));
			classes.Add(new MyClass("Introduction to cables", "WDK", 5, new List<ITeacher>(), new List<IStudent>()));
			classes.Add(new MyClass("Advanced Cooking 3", "AC3", 3, new List<ITeacher>(), new List<IStudent>()));


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

			students.Add(new Student(new List<string> { "Robert" }, "Kielbica", 3, "S1", new List<IMyClass>()));
			students.Add(new Student(new List<string> { "Archibald", "Agapios" }, "Linux", 7, "S2", new List<IMyClass>()));
			students.Add(new Student(new List<string> { "Angrboða" }, "Kára", 31, "S3", new List<IMyClass>()));
			students.Add(new Student(new List<string> { "Olympos" }, "Andronikos", 5, "S4", new List<IMyClass>()));
			students.Add(new Student(new List<string> { "Mac", "Rhymes" }, "Pickuppicker", 6, "S5", new List<IMyClass>()));


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

			teachers.Add(new Teacher(new List<string> { "Tomas" }, "Cherrmann", "MiB", "P1", new List<IMyClass>()));
			teachers.Add(new Teacher(new List<string> { "Jon" }, "Tron", "TiB", "P2", new List<IMyClass>()));
			teachers.Add(new Teacher(new List<string> { "William", "Joseph" }, "Blazkowicz", "GiB", "P3", new List<IMyClass>()));
			teachers.Add(new Teacher(new List<string> { "Arkadiusz", "Amadeusz" }, "Kamiński", "KiB", "P4", new List<IMyClass>()));
			teachers.Add(new Teacher(new List<string> { "Cooking" }, "Mama", "GiB", "P5", new List<IMyClass>()));

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

