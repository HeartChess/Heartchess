using System;
using System.Collections;
// the collection one is for lists and dictionaries
using System.IO;
//that one is read and write files
using System.Xml;
//and this one is for xml
// importing those are called direc					

namespace justFirstChank {
	public class Program
	{
		public static void Main()
		{
			DateTime now = DateTime.Now;
			Console.WriteLine(now);
			
			int maxI = int.MaxValue;
			Console.WriteLine(maxI);
			
			int minI = int.MinValue;
			Console.WriteLine(minI);
			
			sbyte maxSb = sbyte.MaxValue;
			Console.WriteLine(maxSb);
			
			sbyte minSb = sbyte.MinValue;
			Console.WriteLine(minSb);
			
			const sbyte yoo = 56;
			Console.WriteLine(yoo);
			
			string staffUp = "Hello World".ToUpper();
			string staffLow = "SOME CRAZY ASS TEXT".ToLower();
			
			Console.WriteLine(staffUp);
			Console.WriteLine(staffLow);
			Console.WriteLine("Hello whats up");
			
			int value = 56;
			staff(value);
			Console.Write(value);
		}
		
		public static void staff(int value) {
			value = 567345345;
		}
		
		public static void NewMain() {
			Person person = new Person();
			person.Age = 45;
			Console.WriteLine("Persons Age --> ");
			Console.WriteLine(person.Age);
		}
		
		public static void Method1( Person person ) {
			person.Age = 31;
		}
	}
	
	public class Person {
		public int Age { get; set; }
	}
}

// namespace is just markup diff, to clarify the code
namespace itUsedToClaryfiCodeBlocks {
	public class Program {
		public static void MainSecond() {
			string name = "Matt";
			int age = 31;
			DateTime today = DateTime.Now;
			Console.WriteLine(name);
			Console.WriteLine(age);
			Console.ReadLine();
		}
	}
}