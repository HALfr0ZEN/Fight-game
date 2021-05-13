using System;

namespace Hero_and_Dragon.Writers
{
    internal class ConsoleWriter : IWriter
    {
        public char LineFilling { get; set; } = '-';
        public char LineEnds { get; set; } = '|';

        private const ConsoleColor DefaultColor = ConsoleColor.Gray;

        public ConsoleColor Color
        {
            set => Console.ForegroundColor = value;
        }

        private int _lineLenght = 50;

        public int LineLenght
        {
            get => _lineLenght;
            set => _lineLenght = value > 1 ? value : 2;
        }

        public static ConsoleWriter Instance { get; } = new ConsoleWriter();

        private ConsoleWriter()
        {
        }

        public void NewLine(params string[] cols)
        {
            int width = (_lineLenght - 2) / cols.Length;
            string row = LineEnds.ToString();

            foreach (var col in cols)
                row += Center(col, width).PadLeft(width);

            Console.WriteLine(row + LineEnds);
            Color = DefaultColor;
        }

        public void NewLine(string col)
        {
            Console.WriteLine(LineEnds + " " + col.PadRight(_lineLenght - 3) + LineEnds);
            Color = DefaultColor;
        }

        public void NewFilledLine()
        {
            Console.WriteLine("".PadRight(_lineLenght, LineFilling));
            Color = DefaultColor;
        }

        public void NewBlankLine()
        {
            Console.Write(Environment.NewLine);
        }

        private string Center(string text, int width)
        {
            if (text.Length > width)
                text = text[..(width - 3)] + "...";

            if (string.IsNullOrEmpty(text))
                return new string(' ', width);

            return text.PadRight(width - (width - text.Length) / 2);
        }
    }
}