using System;
using System.IO;
using System.Linq;

class wwv {
	// задается максимальное количество строк
	// в выходном html файле
	public void set_N(int N) {
		this.N = N;
	}

	// метод возращает количество строк
	// в исходном файле с текстом TextFile.txt
	public int get_TextFile_lineCount() {
		try
		{
			this.TextFile_lineCount = File.ReadLines(@"TextFile.txt").Count();
			Console.WriteLine ("TextFile size {0}", this.TextFile_lineCount);
			return this.TextFile_lineCount;
		}
		catch (FileNotFoundException)
		{
			Console.WriteLine("TextFile.txt not found");
			return this.TextFile_lineCount;

		}
		catch(IOException e)
		{
			Console.WriteLine(
				"{0}: The read operation could not " +
				"be performed because the specified " +
				"part of the file is locked.", 
				e.GetType().Name);
			return this.TextFile_lineCount;
		}
	}

	// метод возращает количество строк
	// в файле со словарем VocabFile.txt
	public int get_VocabFile_lineCount() {
		try
		{
			this.VocabFile_lineCount = File.ReadLines(@"VocabFile.txt").Count();
			Console.WriteLine ("VocabFile size {0}", this.TextFile_lineCount);
			return this.VocabFile_lineCount;
		}
		catch (FileNotFoundException)
		{
			Console.WriteLine("VocabFile.txt not found");
			return this.VocabFile_lineCount;

		}
		catch(IOException e)
		{
			Console.WriteLine(
				"{0}: The read operation could not " +
				"be performed because the specified " +
				"part of the file is locked.", 
				e.GetType().Name);
			return this.VocabFile_lineCount;
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
	public void set_vocabulary()
	{
		vocabwords = System.IO.File.ReadAllLines(@"VocabFile.txt");
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
	private string[] vocabwords;
}

class Program
{
	// допустимое количество строк в одном html файле
	private const int N = 10;
	// максимальное количество строк в исходном 
	// текстовом файле
	private const int MAX_TEXTFILE_LENGTH = 100000;
	// максимальное количество строк в исходном 
	// файле словаря
	private const int MAX_VOCABFILE_LENGTH = 100000;
	// код, соответствующий успешному завершению программы 
	private const int ERROR_SUCCESS = 0;
	// код, соответствующий проблемам с io с исходным
	// текстовым файлом
	private const int ERROR_TEXTFILE_IO = 1;
	// код, соответствующий превышению максимального количества
	// строк в исходном текстовым файле
	private const int ERROR_TOOLONG_TEXTFILE = 2;
	// код, соответствующий проблемам с io с исходным
	// файлом словаря
	private const int ERROR_VOCABFILE_IO = 3;
	// код, соответствующий превышению максимального количества
	// строк в исходном файле словаря
	private const int ERROR_TOOLONG_VOCABFILE = 4;

	public static int Main()
	{
		var wwv = new wwv ();

		// определение количества строк в исходном
		// текстовом файле
		var TextFile_lineCount = wwv.get_TextFile_lineCount ();

		if (TextFile_lineCount == -1) {
			return ERROR_TEXTFILE_IO;
		}

		if (TextFile_lineCount >= MAX_TEXTFILE_LENGTH) {
			Console.WriteLine ("Too long TextFile");
			return ERROR_TOOLONG_TEXTFILE;
		}

		// определение количества строк в исходном
		// файле словаря
		var VocabFile_lineCount = wwv.get_VocabFile_lineCount ();

		if (VocabFile_lineCount == -1) {
			return ERROR_VOCABFILE_IO;
		}

		if (VocabFile_lineCount >= MAX_VOCABFILE_LENGTH) {
			Console.WriteLine ("Too long VocabFile");
			return ERROR_TOOLONG_VOCABFILE;
		}


		// установка допустимого количества строк в одном html файле
		wwv.set_N(N);
		// загрузка словаря
		wwv.set_vocabulary();

		// обработка исходного текстового файла
		wwv.process_text_file ();

		return ERROR_SUCCESS;

	}
}


