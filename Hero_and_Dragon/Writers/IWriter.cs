namespace Hero_and_Dragon.Writers
{
    /// <summary>
    /// This interface declare how all writers should be written (implemented)
    /// </summary>
    public interface IWriter
    {
        public char LineFilling { get; set; }

        /// <summary>
        /// This method should define/create new line from col given
        /// </summary>
        /// <param name="col">one string</param>
        void NewLine(string col);

        /// <summary>
        /// This method should define/create new line fom cols given
        /// </summary>
        /// <param name="cols">multiple strings</param>
        void NewLine(params string[] cols);

        /// <summary>
        /// This method should define/create new line filled with characters
        /// </summary>
        void NewFilledLine();

        /// <summary>
        /// This method should define/create new line filled with blank characters
        /// </summary>
        void NewBlankLine();
    }
}