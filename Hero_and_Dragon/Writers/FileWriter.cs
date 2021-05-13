using System;
using System.IO;

namespace Hero_and_Dragon.Writers
{
    internal class FileWriter : IWriter
    {
        public char LineFilling { get; set; } = '-';
        public char LineEnds { get; set; } = '|';

        private int _lineLenght = 50;

        public int LineLenght
        {
            get => _lineLenght;
            set => _lineLenght = value > 0 ? value : 1;
        }

        private static FileWriter _instance;

        private string _path;

        private FileWriter(string path)
        {
            string end = path.Substring(path.IndexOf('.'));
            path = path.Substring(0, path.IndexOf('.'));
            int i;
            for (i = 1; File.Exists(path + i + end); i++)
            {
            }

            _path = path + i + end;
        }

        public static FileWriter Instance(string path) => _instance ??= new FileWriter(path);

        public void NewLine(params string[] cols)
        {
            int width = (LineLenght - 2) / cols.Length;
            string row = LineEnds.ToString();

            foreach (var col in cols)
                row += TextToRow(col, width);

            File.AppendAllText(_path, row + LineEnds);
            NewBlankLine();
        }

        public void NewLine(string col)
        {
            File.AppendAllText(_path, LineEnds + " " + col.PadRight(_lineLenght - 3) + LineEnds);
            NewBlankLine();
        }

        public void NewFilledLine()
        {
            File.AppendAllText(_path, "".PadRight(_lineLenght, LineFilling));
            NewBlankLine();
        }

        public void NewBlankLine() => File.AppendAllText(_path, Environment.NewLine);

        private string TextToRow(string text, int width)
        {
            text = text.Length > width ? text[..(width - 3)] + "..." : text;

            if (text.Length > width)
                text = text[..(width - 3)] + "...";

            if (string.IsNullOrEmpty(text))
                return new string(' ', width);

            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}