using System;
using System.Collections.Generic;
using System.Collections;

namespace Project
{
	public static class HashMap2
	{
		public static Dictionary<string,object> hashmap;

		static HashMap2()
		{
			hashmap = new Dictionary<string,object>();
			{

				string s;
				// Classes
				s = "MD2";
				hashmap.Add("MD2", new MyClass_Adapter2(new MyClass_Adaptee2("Diabolical Mathematics 2", s, 2, new List<ITeacher>(), new List<IStudent>())));
				s = "RD";
				hashmap.Add("RD", new MyClass_Adapter2(new MyClass_Adaptee2("Routers descriptions", s, 1, new List<ITeacher>(), new List<IStudent>())));
				s = "WDK";
				hashmap.Add("WDK", new MyClass_Adapter2(new MyClass_Adaptee2("Introduction to cables", s, 5, new List<ITeacher>(), new List<IStudent>())));
				s = "AC3";
				hashmap.Add("AC3", new MyClass_Adapter2(new MyClass_Adaptee2("Advanced Cooking 3", s, 3, new List<ITeacher>(), new List<IStudent>())));



				// Teachers
				s = "P1";
				hashmap.Add("P1", new Teacher_Adapter2(new Teacher_Adaptee2(new List<string> { "Tomas" }, "Cherrmann", "MiB", "P1", new List<IMyClass>())));
				s = "P2";
				hashmap.Add("P2", new Teacher_Adapter2(new Teacher_Adaptee2(new List<string> { "Jon" }, "Tron", "TiB", s, new List<IMyClass>())));
				s = "P3";
				hashmap.Add("P3", new Teacher_Adapter2(new Teacher_Adaptee2(new List<string> { "William", "Joseph" }, "Blazkowicz", "GiB", s, new List<IMyClass>())));
				s = "P4";
				hashmap.Add("P4", new Teacher_Adapter2(new Teacher_Adaptee2(new List<string> { "Arkadiusz", "Amadeusz" }, "Kamiński", "KiB", s, new List<IMyClass>())));
				s = "P5";
				hashmap.Add("P5", new Teacher_Adapter2(new Teacher_Adaptee2(new List<string> { "Cooking" }, "Mama", "GiB", s, new List<IMyClass>())));

				// Students
				s = "S1";
				hashmap.Add("S1", new Student_Adapter2(new Student_Adaptee2(new List<string> { "Robert" }, "Kielbica", 3, s, new List<IMyClass>())));
				s = "S2";
				hashmap.Add("S2", new Student_Adapter2(new Student_Adaptee2(new List<string> { "Archibald", "Agapios" }, "Linux", 7, s, new List<IMyClass>())));
				s = "S3";
				hashmap.Add("S3", new Student_Adapter2(new Student_Adaptee2(new List<string> { "Angrboða" }, "Kára", 1, s, new List<IMyClass>())));
				s = "S4";
				hashmap.Add("S4", new Student_Adapter2(new Student_Adaptee2(new List<string> { "Olympos" }, "Andronikos", 5, s, new List<IMyClass>())));
				s = "S5";
				hashmap.Add("S5", new Student_Adapter2(new Student_Adaptee2(new List<string> { "Mac", "Rhymes" }, "Pickuppicker", 6, s, new List<IMyClass>())));
			}

		}
	}
}
