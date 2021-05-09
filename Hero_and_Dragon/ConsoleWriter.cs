using System;

namespace Hero_and_Dragon
{
    static class ConsoleWriter
    {
        public static char LineFilling { get; set; } = '-';
        public static char LineEnds { get; set; } = '|';

        private static int _lineLenght = 50;

        public static int LineLenght
        {
            get => _lineLenght;
            set => _lineLenght = value > 0 ? value : 1;
        }

        public static void NewLine(ConsoleColor color, params string[] cols)
        {
            Console.ForegroundColor = color;
            int width = (LineLenght - 2)/ cols.Length;
            string row = LineEnds.ToString();
            
            foreach (var col in cols)
                row += TextToRow(col, width);
            
            Console.WriteLine(row + LineEnds);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void NewLine(params string[] cols)
        {
            int width = (LineLenght - 2)/ cols.Length;
            string row = LineEnds.ToString();
            
            foreach (var col in cols)
                row += TextToRow(col, width);
            
            Console.WriteLine(row + LineEnds);
        }

        public static void NewLine(ConsoleColor color, string col)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(LineEnds+ " " + col.PadRight(_lineLenght - 3) + LineEnds);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void NewLine(string col)
        {
            Console.WriteLine(LineEnds+ " " + col.PadRight(_lineLenght - 3) + LineEnds);
        }
        
        public static void NewFilledLine()
        {
            Console.WriteLine("".PadRight(_lineLenght, LineFilling));
        }

        public static void NewBlankLine()
        {
            Console.Write(Environment.NewLine);
        }

        private static string TextToRow(string text, int width)
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