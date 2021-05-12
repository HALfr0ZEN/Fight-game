using System;

namespace Hero_and_Dragon
{
    public interface IWriter
    {
        void NewLine(string col);
        void NewLine(params string[] cols);
        void NewFilledLine();
        void NewBlankLine();
    }
}