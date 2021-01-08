using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ConsoleApp29.MojaLista;

namespace ConsoleApp29
{

	class MojaLista : IEnumerable<string>
	{
		string[] NizNekakav;

		public MojaLista() { }
		public MojaLista(params string[] stvari)
		{
			NizNekakav = new string[stvari.Length];
			for (int i = 0; i < stvari.Length; i++)
			{
				NizNekakav[i] = stvari[i];
			}
		}

		public void Dodaj(string s)
		{
			Array.Resize<string>(ref NizNekakav, NizNekakav.Length + 1);
			NizNekakav[NizNekakav.Length - 1] = s;
		}

		public IEnumerator<string> GetEnumerator()
			=> new MojEnumerator(NizNekakav);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
		public string this[int index] => NizNekakav[index];

		public class MojEnumerator : IEnumerator<string>
		{
			public MojEnumerator(string[] k)
			{
				kol = k;
			}
			private string[] kol;
			private int indeks = -1;
			public string Current => indeks >= 0 && indeks < kol.Length ? kol[indeks]
				: throw new IndexOutOfRangeException("Auh :(");

			object IEnumerator.Current => Current;

			public void Dispose(){}
			public bool MoveNext()
				=> ++indeks < kol.Length;

			public bool MoveBack()
			{
				if (indeks < 0)
					indeks = kol.Length;
				if (--indeks < 0)
					return false;
				return true;
			}

			public void Reset()
				=> indeks = -1;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			MojaLista ml = new MojaLista("prvi", "drugi", "treci");
			ml.Dodaj("blabla");
			Console.WriteLine(ml[1]);
			foreach (var nesto in ml)
				Console.WriteLine(nesto);
			Console.WriteLine("----------------------");
			using (var en = ml.GetEnumerator() as MojEnumerator)
				while (en.MoveBack())
				{
					Console.WriteLine(en.Current);
				}

			/*foreach (var n in Foo(false))
				Console.WriteLine(n);

			Console.WriteLine("----------------------");

			using (var enu = Foo(false).GetEnumerator())
			{
				while (enu.MoveNext())
				{
					int n = enu.Current;
					Console.WriteLine(n);
				}
			}*/

			Console.ReadKey();
		}

		static IEnumerable<int> Foo(bool stop)
		{
			Console.WriteLine("1");
			yield return 1;
			if (stop)
				yield break;
			Console.WriteLine("2");
			yield return 2;
			Console.WriteLine("3");
			yield return 3;
			Console.WriteLine("4");
			yield return 4;
		}
	}
}
