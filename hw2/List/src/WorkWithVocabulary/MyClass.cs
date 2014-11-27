namespace WorkWithVocabulary
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Collections.Generic;

	public class wwv {
		// задается максимальное количество строк
		// в выходном html файле
		public void set_N(int N) {
			this.N = N;
		}
		/// <summary>
		/// возвращает максимальное количество строк
		/// в выходном html файле 
		/// </summary>
		/// <returns>The n.</returns>
		public int get_N() {
			return N;
		}
		// TODO переименовать getTextFilelineCount и get_TextFile_lineCount (get_TextFile_lineCount должно быть set)
		public int getTextFilelineCount() {
			return this.TextFile_lineCount;
		}

		public int getVocabFilelineCount() {
			return this.VocabFile_lineCount;
		}

		// метод возращает количество строк
		// в исходном файле с текстом TextFile.txt
//		public int get_TextFile_lineCount(string path_to_TextFile = @"TextFile.txt") {
		// TODO возможно лучше заменить int на void
		// TODO переименовать переменные и имена методов только CamelCase или только с underscores
		public void get_TextFile_lineCount(string path_to_TextFile = @"TextFile.txt") {
//		public void get_TextFile_lineCount() {
			try
			{
//				this.TextFile_lineCount = File.ReadLines(@"TextFile.txt").Count();
				this.TextFile_lineCount = File.ReadLines(path_to_TextFile).Count();
				Console.WriteLine ("TextFile size {0}", this.TextFile_lineCount);
//				return this.TextFile_lineCount;
			}
			catch (FileNotFoundException)
			{
//				Console.WriteLine("TextFile.txt not found");
//				return this.TextFile_lineCount;
//				Console.WriteLine("TextFile.txt not found");
				throw new FileNotFoundException(@"TextFile.txt not found");

			}
			catch(IOException e)
			{
				Console.WriteLine(
					"{0}: The read operation could not " +
					"be performed because the specified " +
					"part of the file is locked.", 
					e.GetType().Name);
//				return this.TextFile_lineCount;
				throw new IOException(@"The read operation could not " + 
				                      "be performed");
			}
		}

		// метод возращает количество строк
		// в файле со словарем VocabFile.txt
		public void get_VocabFile_lineCount(string path_to_VocabFile = @"VocabFile.txt") {
			try
			{
				this.VocabFile_lineCount = File.ReadLines(path_to_VocabFile).Count();
//				this.VocabFile_lineCount = File.ReadLines(@"VocabFile.txt").Count();
				Console.WriteLine ("VocabFile size {0}", this.TextFile_lineCount);
//				return this.VocabFile_lineCount;
			}
			catch (FileNotFoundException)
			{
//				Console.WriteLine("VocabFile.txt not found");
//				return this.VocabFile_lineCount;
				throw new FileNotFoundException(@"VocabFile.txt not found");

			}
			catch(IOException e)
			{
				Console.WriteLine(
					"{0}: The read operation could not " +
					"be performed because the specified " +
					"part of the file is locked.", 
					e.GetType().Name);
//				return this.VocabFile_lineCount;
				throw new IOException(@"The read operation could not " + 
					"be performed");
			}
		}

		// метод осуществляет преобразование 
		// считанной строки из файла в соответствии с требованиями
		// задания
		public string get_transformed_line(string[] words){
			// строка, которая получается после обработки 
			string new_words = "";
			// обработка пустых строк 
			if (words[0] == "")
			{
				return new_words + "<br>";
			}
			// обработка пустых строк 
			for (var x = 0; x < words.Count(); x++) {

				// временное хранилище для вырезанных
				// предшествующих или последующих
				// символов типа , . ( и т.п.
				string delim_to_append = "";
				string delim_to_prepend = "";

				foreach (var delim in delimiterChars){

					if (delim == words[x][words[x].Length - 1]){
						delim_to_append = delim.ToString();
						// new_word - временное хранилище для сравнения
						// текущего слова со словарем
						string new_word = words[x].Remove(words[x].Length - 1);
						if (vocabwords.Contains(new_word)){
							this.found_in_vocab = true;

							// если достигнут конец строки, то добавить
							// перенос в html
							if (x == (words.Count () - 1)) {
								new_words = new_words + "<b><i>" + 
									new_word + "</i></b>" + delim_to_append + "<br>";
							} 
							else {
								new_words = new_words + "<b><i>" + 
									new_word + "</i></b>" + delim_to_append + " ";
							}	
						}					
						break;
					}

					if (delim == words[x][0]){
						delim_to_prepend = delim.ToString();
						// new_word - временное хранилище для сравнения
						// текущего слова со словарем
						string new_word = words[x].Remove(0, 1);
						if (vocabwords.Contains(new_word)){
							this.found_in_vocab = true;

							// если достигнут конец строки, то добавить
							// перенос в html
							if (x == (words.Count () - 1)) {
								new_words = new_words + delim_to_prepend + "<b><i>" + 
									new_word + "</i></b>" + "<br>";
							} 
							else {
								new_words = new_words + delim_to_prepend +"<b><i>" + 
									new_word + "</i></b>" + " ";
							}	
						}					
						break;
					}

				}

				// сравнение со словарем для
				// слов без символов на конце
				if (vocabwords.Contains(words[x])){
					this.found_in_vocab = true;
					if (x == (words.Count() - 1)){
						new_words = new_words + "<b><i>" + words[x] + "</i></b>" + "<br>";
					}
					else {
						new_words = new_words + "<b><i>" + words[x] + "</i></b>" + " ";
					}
				}


				// слова, не имеющиеся в словаре
				// переходят в html файл без изменений
				if (!this.found_in_vocab) {
					if (x == (words.Count () - 1)) {
						new_words = new_words + words [x] + "<br>";
					} 
					else {
						new_words = new_words + words[x] + " ";
					}
				}

				this.found_in_vocab = false;
			}

			return new_words;
		}

		// загрузка словаря в память целиком
		public void set_vocabulary( List<string> in_vocabwords = null )
		{
			if (in_vocabwords == null) {
//				vocabwords = System.IO.File.ReadAllLines (@"VocabFile.txt");
				var logFile = File.ReadAllLines(@"VocabFile.txt");
				vocabwords = new List<string>(logFile);
			} else {
				this.vocabwords = in_vocabwords;
			}

//			if (this.vocabwords == null) {
//				throw new ArgumentNullException("Vocabulary has not been set!");
//			}
//			vocabwords = System.IO.File.ReadAllLines(@"VocabFile.txt");
		}

		// построчное чтение исходного
		// файла и запись в html файл
		public void process_text_file ()
		{
			string lline;

			System.IO.StreamReader file = 
				new System.IO.StreamReader(@"TextFile.txt");

			// определение количества html
			// файлов на выходе в зависимости от 
			// допустимого числа строк в одном html файле
			int number_of_htmls = this.TextFile_lineCount / this.N;

			if (number_of_htmls == 0) {

				string output_html_file_name = "output" + number_of_htmls.ToString() + ".html";

				StreamWriter file2 = new StreamWriter(output_html_file_name, false);
				file2.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\" />");
				file2.WriteLine("");

				while((lline = file.ReadLine()) != null){
					// преобразование считанной строки в
					// соответствии с заданием
					string[] words = lline.Split(' ');
					string new_words = get_transformed_line(words);
					// вывод преобразованной строки в консоль
					Console.WriteLine ("Transformed line is {0}, written to {1}", new_words, output_html_file_name);
					file2.WriteLine(new_words);
				}
				file2.Close();

			}

			//split to separate files
			if (number_of_htmls > 0) {
				int out_file_id = 0;
				string output_html_file_name = "output" + out_file_id.ToString() + ".html";

				StreamWriter file2 = new StreamWriter(output_html_file_name, false);
				file2.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\" />");
				file2.WriteLine("");

				int counter = 0;
				while((lline = file.ReadLine()) != null){
					// преобразование считанной строки в
					// соответствии с заданием
					string[] words = lline.Split(' ');
					string new_words = get_transformed_line(words);
					if (counter == this.N) {
						//					Console.WriteLine ("new file creation, counter = {0}", counter);
						file2.Close();
						counter = 0;
						out_file_id += 1; 
						output_html_file_name = "output" + out_file_id.ToString() + ".html";
						file2 = new StreamWriter(output_html_file_name, false);
						file2.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html;charset=utf-8\" />");
						file2.WriteLine("");
						file2.WriteLine (new_words);
					}
					else {
						file2.WriteLine (new_words);
					}
					// вывод преобразованной строки в консоль
					Console.WriteLine ("Transformed line is {0}, written to {1}", new_words, output_html_file_name);
					counter += 1;
				}
				file2.Close();

			}

			file.Close();

		}

		// TODO добавить геттеры и сеттеры
		// допустимое количество строк в одном html файле
		private int N;
		// количество строк в исходном текстовом файле
		private int TextFile_lineCount = -1;
		// количество строк в исходном файле словаря
		private int VocabFile_lineCount = -1;
		private bool found_in_vocab = false;
		// возможные разделители, использующиеся
		// в исходном тексте
		private char[] delimiterChars = {',', '.', ':', '?' ,'!', '\t', '(', ')', '\'', '\"'};
		// словарь
		private List<string> vocabwords;
	}

}


