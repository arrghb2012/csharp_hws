using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

public class Student
{
	public string FIO { get; set; }
	public int Kurs { get; set; }
	public string Specialnost { get; set; }
}

class MainClass
	{
	public static void Main(string[] args) 
	{ 
		List<Student> Students = new List<Student>();

		Student s1 = new Student();
		s1.FIO = "FIO1";
		s1.Kurs = 4;
		s1.Specialnost = "S1";

		Student s2 = new Student();
		s2.FIO = "FIO2";
		s2.Kurs = 1;
		s2.Specialnost = "S2";

		Student s3 = new Student();
		s3.FIO = "FIO3";
		s3.Kurs = 2;
		s3.Specialnost = "S3";

		Student s4 = new Student();
		s4.FIO = "FIO4";
		s4.Kurs = 1;
		s4.Specialnost = "S4";

		Student s5 = new Student();
		s5.FIO = "FIO5";
		s5.Kurs = 3;
		s5.Specialnost = "S5";

		Student s6 = new Student();
		s6.FIO = "FIO6";
		s6.Kurs = 4;
		s6.Specialnost = "S6";

		Student s7 = new Student();
		s7.FIO = "FIO7";
		s7.Kurs = 5;
		s7.Specialnost = "S7";

		Student s8 = new Student();
		s8.FIO = "FIO8";
		s8.Kurs = 2;
		s8.Specialnost = "S8";

		Student s9 = new Student();
		s9.FIO = "FIO9";
		s9.Kurs = 4;
		s9.Specialnost = "S9";

		Student s10 = new Student();
		s10.FIO = "FIO10";
		s10.Kurs = 2;
		s10.Specialnost = "S10";

		Students.Add(s1);  
		Students.Add(s2);  
		Students.Add(s3);  
		Students.Add(s4);  
		Students.Add(s5);  
		Students.Add(s6);  
		Students.Add(s7);  
		Students.Add(s8);  
		Students.Add(s9);  
		Students.Add(s10);  

		XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
		using ( TextWriter writer = new StreamWriter( @"Xml.xml"))
		{
			serializer.Serialize(writer, Students);
		}

	}
}