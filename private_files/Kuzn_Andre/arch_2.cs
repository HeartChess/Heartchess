using System;
using System.Collections.Concurrent;
using System.Ling;
using System.Threading;

public class newStaff {
	public static void newFunction() {
		sbyte items = 50;
		string fam = "Here is the number "
		Console.WriteLine(fum + items);


		//arrays
		int[] nums = {1,2,3,4,6};
		int[] sttf = new int[10];
		int[] namms = new int[20];

		sbyte[] staff = new sbyte[10];
		sbyte q = staff[0];
		sbyte r = staff[1];
		staff[2] = 10;

		Console.WriteLine(sttf.Length);
		Console.WriteLine(nums.Length);

		//lists
		List<int> numbers = new List<int>();
		numbers.Add(100);
		numbers.Add(44);
		numbers.Add(1);
		numbers.Add(98);


		int x = 34;
		float s = 17.000021;
		short z = 5;
		double w = 1.4E + 3;
		Console.WriteLine("The sum is {0}", x + s + z + w);

		const int staffs = 56;
		const string name = "Andre";
		const char letterStaff = "S";
		// you can not change this guy!))

		//const Person person = new Person();
		//top will not work cause const work only with literal vals
		separateFn();
		HereWeGo();

		      // write your code here
     	int[] fruits = new int[10];
      
	    int fn = fruits[0];
	    int sn = fruits[1];
	       
	    fruits[0] = 457;
	    fruits[1] = 1;
	    fruits[2] = 0001;
	       
	      // test code
	    Console.WriteLine(fruits[0]);
	    Console.WriteLine(fruits[1]);
	    Console.WriteLine(fruits[2]);
	}

	public static void separateFn() {
		ConcurrentDictionary<string, int> dict = new ConcurrentDictionary<string, int>;
		//var is basically tell the compiler to determine what tha type of value is assinged
		var dictin = new ConcurrentDictionary<string, int>;

		var personStaffs = new { Name = "John Smith" };
		Console.Write(personStaffs);
	}

	// Enums
	public enum FooBar 
	{
		One = 2,
		Two = 3,
		Stafffs = 4,
	}

	public static HereWeGo () 
	{
		FooBar smallFooBar = FooBar.Two;
		Console.WriteLine(smallFooBar);
	}

}

//============================================================//

class ProgramConcarDicts {
	static ConcurrentDictionary<string, int> _concarent = 
	new ConcurrentDictionary<string, int>();

	static void MainStaff() {
		Thread thread1 = new Thread(new ThreadStart(A));
		Thread thread2 = new Thread(new ThreadStart(A));
		thread1.Start();
		thread2.Start();
		thread1.Join();
		thread2.Join();

		Console.WriteLine("Avarage: {0}", _concarent.Values.Avarage());
	}

	static void A()
	{
		for(int i = 0; i < 1000; i++) 
		{
			_concarent.TryAdd(i.ToString(), i);
		}
	}
}

class tryUpdateConcurentDics 
{
	static void MainFunction() 
	{
		var con = new ConcurrentDictionary<string, int>();
		con.TryAdd("cat", 1);
		con.TryAdd("dog", 2);

		//Try to update if value is 4 (fails)
		con.TryUpdate("cat", 200, 4);

		//Try to update if value is 2 (works)
		con.TryUpdate("dog", 100, 2);
		con.TryUpdate("cat", 500, 1);

		Console.WriteLine(con["cat"]);
	}
}


class MultidynamArr 
{
	public static void Arrrs() 
	{
		int[,] matrix = new int[34, 56];

		matrix[0, 0] = 1;
		matrix[0, 1] = 2;
		matrix[1, 0] = 3;
		matrix[1, 1] = 4;

		int[,] predefinedMatrix = new int[2,2] { { 1, 2 }, { 3, 4 } };
	}

}












