using NUnit.Framework;
using System;
using System.Linq;
using WorkWithVocabulary;
using System.IO;

namespace WorkWithVocabularyTests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		// проверка правильности задания максимального количества строк в выходном html файле
		public void SettingMaxLinesInOutputFile ()
		{
			var wwv_ins = new wwv ();
			wwv_ins.set_N (10);
			Assert.AreEqual(10, wwv_ins.get_N());
		}

		[Test()]
		// проверка преобразования одной строки в соответствии с заданным вручную словарем
		public void CorrectlyTransformSingleLine ()
		{
			var wwv_ins = new wwv ();
			string[] input_line = {"текст,", "в"}; 
			string[] vocabulary = {"текст"};
			wwv_ins.set_vocabulary(vocabulary);
//			Assert.IsTrue(input_line.Count() == 2);
			Assert.AreEqual(2, input_line.Count());
			Assert.IsTrue(vocabulary.Contains("текст"));
			Assert.AreEqual("<b><i>текст</i></b>, в<br>", wwv_ins.get_transformed_line(input_line));

		}

		
		[Test()]
		// более сложный пример преобразования строк в соответствии с заданным вручную словарем
		public void MoreComplexExample()
		{
			var wwv_ins = new wwv ();
			string[] vocabulary = {"текст", "в", "знак"};

			wwv_ins.set_vocabulary(vocabulary);

			string[][] input_lines = new string[4][];
			input_lines[0] = new string[]{"текст,", "в", "текстовом", "файле"};
			input_lines[1] = new string[]{""}; // если задать просто new string[]{} то будет ошибка SystemIndexOutOfRangeException
			input_lines[2] = new string[]{"со", "словами", "(в", "том", "числе", "из", "словаря)"}; 
			input_lines[3] = new string[]{"и", "знаками", "препинания"};
			Assert.AreEqual(4, input_lines.Length);

			string[] expected_out_lines = new string[4];
			expected_out_lines[0] = "<b><i>текст</i></b>, <b><i>в</i></b> текстовом файле<br>";
			expected_out_lines[1] = "<br>";
			expected_out_lines[2] = "со словами (<b><i>в</i></b> том числе из словаря)<br>";
			expected_out_lines[3] = "и знаками препинания<br>";

			for (int i = 0; i < input_lines.Length; i++)
			{
				string new_words = wwv_ins.get_transformed_line(input_lines[i]);
				//				Console.WriteLine(i);
				//				Console.WriteLine(input_lines[i].Length);
				//				Console.WriteLine("Transformed line is {0}", new_words);
				Assert.AreEqual(new_words, expected_out_lines[i]);
			}

		}

		[Test()]
		public void WillThrowArgumentNullExceptionWhenDictionaryIsNotSet ()
		{
			var wwv_ins = new wwv ();
			string[] input_line = {"текст,", "в"}; 
			string[] vocabulary = {"текст"};
			// не вызван метод wwv_ins.set_vocabulary(); - выбрасывается исключение из-за пустого словаря
			Assert.Throws<ArgumentNullException>(() => wwv_ins.get_transformed_line(input_line));
		}

		[Test()]
		// подсчет количества строк в тестовом текстовом файле, который находится в той
		// же директории, что и Test.cs
		public void GetTextFileLineCount ()
		{
			var wwv_ins = new wwv ();
			wwv_ins.set_N (10);
			wwv_ins.get_TextFile_lineCount (@"../../TextFile.txt");
			Assert.AreEqual(1, wwv_ins.getTextFilelineCount());
		}

		[Test()]
		// подсчет количества строк в тестовом текстовом файле TextFile.txt, который не найден, выбрасывает исключение
		public void WillThrowFileNotFoundExceptionWhenPathToTextFileIsNotSet ()
		{
			var wwv_ins = new wwv ();
			Assert.Throws<FileNotFoundException>(() => wwv_ins.get_TextFile_lineCount ());
		}

		[Test()]
		// подсчет количества строк в тестовом текстовом файле TextFile.txt,
		// для которого неверно задан путь для, выбрасывает исключение
		public void WillThrowFileNotFoundExceptionWhenIncorrectPathToTextFileIsSet ()
		{
			var wwv_ins = new wwv ();
			Assert.Throws<FileNotFoundException>(() => wwv_ins.get_TextFile_lineCount (@"/home/rr4/TextFile.txt"));
		}

		[Test()]
		// подсчет количества строк в тестовом текстовом файле словаря VocabFile.txt, который не найден, выбрасывает исключение
		public void WillThrowFileNotFoundExceptionWhenPathToVocabFileIsNotSet ()
		{
			var wwv_ins = new wwv ();
			Assert.Throws<FileNotFoundException>(() => wwv_ins.get_VocabFile_lineCount ());
		}
			 


	}
}

