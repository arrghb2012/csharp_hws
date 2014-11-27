using System;
using System.Linq;
using WorkWithVocabulary;
using System.IO;

namespace Program
{
	class ProgramMain
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
			wwv.get_TextFile_lineCount ();
			var TextFile_lineCount = wwv.getTextFilelineCount();

			if (TextFile_lineCount == -1) {
				return ERROR_TEXTFILE_IO;
			}

			if (TextFile_lineCount >= MAX_TEXTFILE_LENGTH) {
				Console.WriteLine ("Too long TextFile");
				return ERROR_TOOLONG_TEXTFILE;
			}

			// определение количества строк в исходном
			// файле словаря
			wwv.get_VocabFile_lineCount ();
			var VocabFile_lineCount = wwv.getVocabFilelineCount();

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
	}}
